using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class respawnEnemies : MonoBehaviour
{
    [SerializeField]
    float timeToInitRespawn, timeForEachInstantiate, leftRangeToRespawn, rightRangeToRespawn,
        reduce,timeToReduce;
    Vector3 spawnPosition;
    public PhotonView _photonView;
    bool respawnIsActive;
    [SerializeField]
    public bool leader;
    float playersInRoom;

    private void Awake()
    {
        playersInRoom = PhotonNetwork.CountOfPlayersInRooms + 1;
        respawnIsActive = false;
    }

    private void Start()
    {
        //Reducir tiempo de respawn de acuerdo al numero de jugadores en sala
        timeForEachInstantiate = (timeForEachInstantiate / playersInRoom);
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
        if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient || !PhotonNetwork.InRoom)
        {
            //InvokeRepeating("instantiateEnemie", timeToInitRespawn, timeForEachInstantiate);
            StartCoroutine(instantiateEnemie());
            if (leader) //objeto líder (dangerous)
            {
                StartCoroutine(ReduceTimeForEachInstantiate());
                //InvokeRepeating("ReduceTimeForEachInstantiate", timeToReduce, timeToReduce);
            }
        }
    }

    IEnumerator instantiateEnemie()
    {
        while (true)
        {
            spawnPosition = new Vector3(transform.position.x, transform.position.y,
            transform.position.z + Random.Range(leftRangeToRespawn, rightRangeToRespawn));
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.Instantiate("Alien", spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(Resources.Load("Alien"), spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(timeForEachInstantiate);
        }
    }

    IEnumerator ReduceTimeForEachInstantiate()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToReduce);
            timeForEachInstantiate = (timeForEachInstantiate / reduce);
        }
    }
}
