using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuGameOver, panelMenuGameOver, instructions, menuPause;
    public static MenuManager _sharedIntance;
    public TextMeshProUGUI titleFinalGame, titleButtonInstructions;
    public List<AudioSource> audioSources = new List<AudioSource>();
    Color screenRed, screenBlue;
    public bool sceneRestarted;
    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
        screenRed = new Color32(243, 61, 61, 100); screenBlue = new Color32(58, 92, 144, 100);
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
        MenuFinishGame(false, null, 0);
        RestartScene();
        sceneRestarted = true;
    }
    /// <summary>
    /// this function tell if the player "win" or "lost"
    /// </summary>
    /// <param name="title">message in screen</param>
    /// <param name="status">win or lost</param>
    public void FinisGame(string title, int status)
    {
        MenuFinishGame(true, title, status);
        Time.timeScale = 0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enable">show menu finish game</param>
    /// <param name="title">message in screen</param>
    /// <param name="condition">to change the color of canvas. 0 to lose (red), 1 to win (blue)</param>
    public void MenuFinishGame(bool enable, string title, int condition) {
        menuGameOver.SetActive(enable);
        titleFinalGame.text = title;
        panelMenuGameOver.GetComponent<Image>().color = condition == 0 ? screenRed : screenBlue;
        audioSources[0].Play();
    }

    public void ShowInstructions(){
        instructions.SetActive(!instructions.activeInHierarchy);
        titleButtonInstructions.text = instructions.activeInHierarchy ? "QUIT" : "GUIDE";
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
            Debug.Log("Cargada escena local");
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        menuPause.SetActive(true);

    }
    public void ReturnGame()
    {
        Time.timeScale = 1;
        menuPause.SetActive(false);
    }
}
