using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Photon.Pun;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField]
    int damage, LayerPlayer, LayerEnemie;
    public bool requiredSound;
    public float timeToDestroyDamageParticle;
    public GameObject damageParticle;
    public AudioSource attackSound;
    /********/
    PhotonView _photonviewOther;
    private void Awake()
    {
        LayerPlayer = LayerMask.NameToLayer("Player");
        LayerEnemie = LayerMask.NameToLayer("Enemie");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerPlayer || other.gameObject.layer == LayerEnemie)
        {
            Health _health = other.GetComponent<Health>();
            /********/
            _photonviewOther = other.GetComponent<PhotonView>();
            /********/
            if (_health != null)
            {
                if (!_health.ObjectIsDie())
                {
                    if (requiredSound)
                        attackSound.Play();

                    if (PhotonNetwork.InRoom)
                        _photonviewOther.RPC("TakeDamage", RpcTarget.All, damage, _photonviewOther.ViewID);
                    else
                        _health.TakeDamage(damage, _photonviewOther.ViewID);

                    if (gameObject.tag == "Bullet")
                    {
                        if(other.gameObject.tag == "Enemie")
                        {
                            InstantiateTemporalObject._sharedIntance.ShowParticle(damageParticle, gameObject.transform.position,
                            timeToDestroyDamageParticle);
                        }
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
