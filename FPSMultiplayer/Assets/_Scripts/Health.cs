using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Health : MonoBehaviour
{
    [SerializeField]
    int amount;
    public Slider HP;
    PhotonView _photonview;

    private void Start()
    {
        _photonview = PhotonView.Get(this);
        HP.maxValue = amount;
        HP.value = amount;
    }
    //*************
    [PunRPC]
    void TakeDamage(int value, int viewID)
    {
        if(_photonview.ViewID == viewID)
        {
            amount -= value;
            HP.value = amount;
            if (amount <= 0)
            {
                if (gameObject.CompareTag("Pilar"))
                {
                    GameManager._sharedIntance.PilarsEnemiesInScene--;
                }
                if (gameObject.CompareTag("Player"))
                {
                    GameManager._sharedIntance.PlayersInScene--;
                }
                else {
                    Destroy(gameObject);
                }
            }
        }
        else{
            Debug.Log("Falso. Script Health");
        }
    }
    //*************
    /*
    public int Amount
    {
        get => amount;
        set {
            amount = value;
            HP.value = amount;
            if(amount <= 0)
            {
               Destroy(gameObject);
            }
        }
    }
    */
}
