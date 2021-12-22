using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField]
    int damage;

    private void OnTriggerEnter(Collider other)
    {
        Life life = other.GetComponent<Life>();

        if (life != null)
            life.AmountLife -= damage;

    }

}
