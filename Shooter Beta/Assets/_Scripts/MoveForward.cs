using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField]
    public float velocity;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,velocity * Time.deltaTime);
    }
}
