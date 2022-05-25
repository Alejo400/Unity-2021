using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuGameOver, panelMenuGameOver;
    public static MenuManager _sharedIntance;
    public TextMeshProUGUI titleFinalGame;
    Color screenRed, screenBlue;
    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
        screenRed = new Color32(243,61,61,100); screenBlue = new Color32(58, 92, 144, 100);
    }
    public void StartGame()
    {
        RestartScene();
    }
    public void ExitGame()
    {
        Debug.Log("HEMOS SALIDO DEL JUEGO!");
        Application.Quit();
    }

    public void RestarGame()
    {
        Time.timeScale = 1;
        MenuFinishGame(false,null,0);
        RestartScene();
    }

    public void FinisGame(string title, int status)
    {
        MenuFinishGame(true,title,status);
        Time.timeScale = 0;
    }

    public void MenuFinishGame(bool enable, string title, int condition) { 
        menuGameOver.SetActive(enable);
        titleFinalGame.text = title;
        panelMenuGameOver.GetComponent<Image>().color = condition == 0 ? screenRed : screenBlue;
    }

    void RestartScene()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LoadLevel(1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
