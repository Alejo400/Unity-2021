using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField]
    int damage;
    public AudioSource attackSound;

    private void OnTriggerEnter(Collider other)
    {
        Health _health = other.GetComponent<Health>();
        if(_health != null)
        {
            attackSound.Play();
            _health.Amount -= damage;
            if (this.gameObject.tag == "Bullet")
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
