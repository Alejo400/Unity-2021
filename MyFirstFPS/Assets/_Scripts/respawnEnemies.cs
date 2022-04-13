using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class respawnEnemies : MonoBehaviour
{
    [SerializeField]
    float timeToInitRespawn, timeForEachInstantiate;
    Vector3 spawnPosition;
    public PhotonView _photonView;
    bool respawnIsActive;

    private void Awake()
    {
        respawnIsActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !respawnIsActive)
        {
            //if (!PhotonNetwork.InRoom || (PhotonNetwork.IsMasterClient && _photonView.IsMine))
            generateRespawn();
            respawnIsActive = true;
        }
    }

    void generateRespawn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            InvokeRepeating("instantiateEnemie", timeToInitRespawn, timeForEachInstantiate);
        }
    }

    void instantiateEnemie()
    {
        spawnPosition = new Vector3(transform.position.x, transform.position.y, Random.Range(10, -2));
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate("Alien", spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(Resources.Load("Alien"), spawnPosition, Quaternion.identity);
        }
    }

        /*Instantiate(prefab);
        prefab.transform.position = transform.position;*/
}
