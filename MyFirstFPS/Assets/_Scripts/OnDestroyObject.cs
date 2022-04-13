using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyObject : MonoBehaviour
{
    public GameObject particleDeath;
    Vector3 position;

    private void Awake()
    {
        position = gameObject.transform.position;
    }

    private void OnDestroy()
    {
        GameObject objectInstante = Instantiate(particleDeath);
        objectInstante.transform.position = new Vector3(position.x,position.y + 1.5f,position.z);
    }
}
