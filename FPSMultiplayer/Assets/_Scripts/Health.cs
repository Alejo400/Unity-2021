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
    public void TakeDamage(int value, int viewID)
    {
        if(_photonview.ViewID == viewID)
        {
            amount -= value;
            HP.value = amount;
            
            if(gameObject.CompareTag("Player"))
                UIManager._sharedIntance.showScreenDamagePlayer();
            if (ObjectIsDie())
                ObjectDie();
        }
    }
    public bool ObjectIsDie()
    {
        if (amount <= 0)
        {
            return true;
        }
        return false;
    }
    public void ObjectDie()
    {
        if (gameObject.CompareTag("Pilar"))
        {
            GameManager._sharedIntance.PilarsEnemiesInScene--;
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("Player"))
        {
            GameManager._sharedIntance.PlayersInScene--;
        }
        if (gameObject.CompareTag("Enemie"))
        {
            UIManager._sharedIntance.PlayerKills++;
            gameObject.GetComponent<Enemie>().Die();
        }
    }
    public int Amount
    {
        get => amount;
        set { 
            amount = value;
            HP.value = amount;
            if (amount > HP.maxValue)
            {
                amount = ((int)HP.maxValue);
            }
        }
    }
}
