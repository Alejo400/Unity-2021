using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPrefabs : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Enemigo a respawmear")]
    public GameObject prefab;

    [SerializeField]
    [Range(0,10)]
    public float timeToInitRespawn, timeBetweenRespawn, endTime;

    private void Start()
    {
        WaveManager.sharedIntance.listWaves.Add(this);
        InvokeRepeating("respawnPrefab",timeToInitRespawn,timeBetweenRespawn);
        Invoke("EndWave",endTime);
    }

    void respawnPrefab()
    {
       Instantiate(prefab, transform.position, transform.rotation);
    }

    void EndWave()
    {
        WaveManager.sharedIntance.listWaves.Remove(this);
        CancelInvoke();
    }
}
