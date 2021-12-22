using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPrefabs : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Enemigo a respawmear")]
    public GameObject prefab;

    [SerializeField]
    [Range(0,5)]
    public float timeToInitRespawn, timeBetweenRespawn;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("respawnPrefab",timeToInitRespawn,timeBetweenRespawn);
    }

    void respawnPrefab()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
    
}
