using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float timeToDestroyObject;

    private void Awake()
    {
        Destroy(gameObject,timeToDestroyObject);
    }
}
