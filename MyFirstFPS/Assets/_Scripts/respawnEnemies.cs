using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnEnemies : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField]
    float timeToInitRespawn, timeForEachInstantiate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            generateRespawn();
            /*Ray para verificar que no hay una pared delante del player. Vector3.up es para no chocar con el 
             collider de este objeto. ToDo: investigar como manejar esto de otra forma con LayerMask
             */
            //ray, debug ray, hit
        }
    }

    void generateRespawn()
    {
        InvokeRepeating("instantiateEnemie", timeToInitRespawn, timeForEachInstantiate);
    }

    void instantiateEnemie()
    {
        Instantiate(prefab);
        prefab.transform.position = transform.position;
    }
}
