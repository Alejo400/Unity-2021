using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    public GameObject firePoint;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            GameObject prefab = ObjectsPooling.sharedIntance.GetFirstObjectOfPool();
            prefab.transform.position = firePoint.transform.position;
            prefab.transform.rotation = firePoint.transform.rotation;
        }
    }
}
