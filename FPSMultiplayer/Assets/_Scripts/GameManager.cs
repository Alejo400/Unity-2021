using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateGame
{
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager _sharedIntance;
    float playersInScene;
    public StateGame currentStateGame = StateGame.inGame;
    int pilarsEnemiesInScene;
    string mission1 = "Destruye todos los pilares para romper la entrada de los Aliens a la tierra y acaba con el jefe final" +
        "para acabar con la infección";

    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        Time.timeScale = 0;
        if (PhotonNetwork.InRoom)
        {
            playersInScene = PhotonNetwork.CountOfPlayersInRooms;
        }
        pilarsEnemiesInScene = GameObject.FindGameObjectsWithTag("Pilar").Length;

        UIManager._sharedIntance.ShowDialog(mission1);
    }
    private void Update()
    {
        PauseGame();
    }
    public float PlayersInScene{
        get => playersInScene;
        set {
            playersInScene = value;
            if(playersInScene < 0)
            {
                currentStateGame = StateGame.gameOver;
                MenuManager._sharedIntance.FinisGame("GAME OVER",0);
            }
        }
    }

    public int PilarsEnemiesInScene
    {
        get => pilarsEnemiesInScene;
        set
        {
            pilarsEnemiesInScene = value;
            if (pilarsEnemiesInScene <= 0)
            {
                currentStateGame = StateGame.gameOver;
                MenuManager._sharedIntance.FinisGame("¡¡VICTORY!!",1);
            }
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentStateGame != StateGame.gameOver)
        {
            if (PhotonNetwork.InRoom)
            {
                //TODO
            }
            else
            {
                MenuManager._sharedIntance.PauseGame();
            } 
        }
    }
}
