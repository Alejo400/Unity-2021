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

    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            playersInScene = PhotonNetwork.CountOfPlayersInRooms;
        }
        pilarsEnemiesInScene = GameObject.FindGameObjectsWithTag("Pilar").Length;
    }

    public float PlayersInScene{
        get => playersInScene;
        set {
            playersInScene = value;
            if(playersInScene < 0)
            {
                currentStateGame = StateGame.gameOver;
                MenuManager._sharedIntance.GameOver("GAME OVER");
            }
        }
    }

    public int PilarsEnemiesInScene
    {
        get => pilarsEnemiesInScene;
        set
        {
            pilarsEnemiesInScene = value;
            if(pilarsEnemiesInScene <= 0)
            {
                MenuManager._sharedIntance.GameOver("¡¡VICTORY!!");
            }
        }
    }

}
