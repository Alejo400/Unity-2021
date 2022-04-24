using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuGameOver;
    public static MenuManager _sharedIntance;
    public TextMeshProUGUI titleFinalGame;
    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
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
        MenuGameOver(false,null);
        RestartScene();
    }

    public void GameOver(string title)
    {
        MenuGameOver(true,title);
        Time.timeScale = 0;
    }

    public void MenuGameOver(bool enable, string title) { 
        menuGameOver.SetActive(enable);
        titleFinalGame.text = title;
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
