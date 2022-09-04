using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPowerUp : MonoBehaviour
{
    [SerializeField]
    string nameOfObjetc;
    public GameObject _object;
    [SerializeField]
    float timeToInitRespawn, timeForEachInstantiate, increaseRandomLeftPosition, increaseRandomRightPosition, increaseRandomDownPosition,
        increaseRandomUpPosition;
    [SerializeField]
    int amountOfObjectsToInstantiate;
    bool respawnIsActive;
    SpawnObjects _spawnObjects;
    public GameObject InvokeEffect;

    private void Awake()
    {
      _spawnObjects = GetComponent<SpawnObjects>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !respawnIsActive)
        {
            generateRespawn();
            respawnIsActive = true;
        }
    }

    void generateRespawn()
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient || !PhotonNetwork.InRoom)
        {
            StartCoroutine(_spawnObjects.instantiateObject(timeToInitRespawn,amountOfObjectsToInstantiate,_object,
            increaseRandomDownPosition,increaseRandomUpPosition,increaseRandomLeftPosition,
            increaseRandomRightPosition,nameOfObjetc,timeForEachInstantiate, InvokeEffect));
        }
    }
}

