using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int amount;

    public int Amount
    {
        get => amount;
        set {
            amount = value;
            if(amount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
