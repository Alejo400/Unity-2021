using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Photon.Pun;
using System.Diagnostics;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField]
    int damage;
    public bool requiredSound;
    public AudioSource attackSound;
    /********/
    PhotonView _photonviewOther;

    private void OnTriggerEnter(Collider other)
    {
        Health _health = other.GetComponent<Health>();
        /********/
        _photonviewOther = other.GetComponent<PhotonView>();
        /********/
        if (_health != null)
        {
            if(requiredSound)
                attackSound.Play();

            _photonviewOther.RPC("TakeDamage", RpcTarget.All,damage,_photonviewOther.ViewID);
            //_health.Amount -= damage;

            if (gameObject.tag == "Bullet")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
