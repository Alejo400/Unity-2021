using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    string nameOfObjetc;
    public GameObject _object;
    [SerializeField]
    float timeToInitRespawn, timeForEachInstantiate, increaseRandomLeftPosition, increaseRandomRightPosition, 
        increaseRandomDownPosition, increaseRandomUpPosition,timeToReduce;
    [SerializeField]
    int amountOfObjectsToInstantiate;
    [SerializeField, Range(1.2f, 5f)]
    float reduce;
    bool respawnIsActive;
    [SerializeField]
    bool leader;
    float playersInRoom;
    SpawnObjects _spawnObjects;
    public GameObject InvokeEffect;

    private void Awake()
    {
        playersInRoom = PhotonNetwork.CountOfPlayersInRooms + 1;
        respawnIsActive = false;
        _spawnObjects = GetComponent<SpawnObjects>();
    }

    private void Start()
    {
        timeForEachInstantiate = (timeForEachInstantiate / playersInRoom);
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
            if (leader) //líder de los pilares que spawnmean enemigos
            {
                StartCoroutine(ReduceTimeForEachInstantiate());
            }
        }
    }

    /// <summary>
    /// Reducir el tiempo que tardan los pilares en instanciar un enemigo
    /// </summary>
    /// <returns>tiempo de reducción</returns>
    IEnumerator ReduceTimeForEachInstantiate()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToReduce);
            timeForEachInstantiate = (timeForEachInstantiate / reduce);
            _spawnObjects.TimeForEachInstantiateTo = timeForEachInstantiate;
        }
    }
}
