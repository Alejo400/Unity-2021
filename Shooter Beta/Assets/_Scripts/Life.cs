using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField]
    int amount;

    public int AmountLife
    {
        get => amount;
        set
        {
            amount = value;
            if(amount <= 0)
            {

                MoveForward move = GetComponent<MoveForward>();
                move.enabled = false;

                ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
                explosion.Play();

                Animator die = GetComponentInChildren<Animator>();
                die.SetTrigger("Die");

                Destroy(gameObject, 2);
            }
        }
    }

}
