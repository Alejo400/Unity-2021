using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField]
    int damage;

    private void OnTriggerEnter(Collider other)
    {
        Health _health = other.GetComponent<Health>();
        if(_health != null)
        {
            _health.Amount -= damage;
            gameObject.SetActive(false);
        }
    }
}
