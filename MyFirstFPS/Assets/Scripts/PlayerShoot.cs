using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    GameObject objectOfPool;
    public GameObject point;

    [SerializeField]
    float timeBetweenFire = 5, timeToFire = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") & PlayerMove.sharedIntance.IsRun == false)
        {
            if(PlayerMove.sharedIntance.IsMoving == true || PlayerMove.sharedIntance.IsPoiting == true)
            {
                if(Time.time >= timeToFire){

                    timeToFire = Time.time + timeBetweenFire;

                    objectOfPool = ObjectsPooling._sharedIntance.GetFirstPrefabOfPool();
                    objectOfPool.transform.position = point.transform.position;
                    objectOfPool.transform.rotation = transform.rotation;
                    objectOfPool.SetActive(true);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
