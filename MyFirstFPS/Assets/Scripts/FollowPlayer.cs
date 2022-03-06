using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject target;
    Vector3 lookDirection;
    Rigidbody _rigibody;
    [SerializeField]
    float velocity;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _rigibody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Vector3 lookDirection = target.transform.position - transform.position;
        lookDirection.Normalize();
        _rigibody.AddForce(velocity * lookDirection);
    }

}
