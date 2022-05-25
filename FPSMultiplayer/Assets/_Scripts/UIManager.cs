using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI playerKillsText;
    int playerKills;
    public static UIManager _sharedIntance;
    public GameObject screenDamagePlayer;

    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
        else
            Debug.Log("Ya existe un UIManager");
    }
    // Start is called before the first frame update
    public int PlayerKills
    {
        get => playerKills;
        set
        {
            playerKills = value;
            playerKillsText.text = playerKills.ToString();
        }
    }
    public void showScreenDamagePlayer()
    {
       screenDamagePlayer.SetActive(true);
       Invoke("HideScreenDamagePlayer",0.2f);
    }
    public void HideScreenDamagePlayer() => screenDamagePlayer.SetActive(false);
}
