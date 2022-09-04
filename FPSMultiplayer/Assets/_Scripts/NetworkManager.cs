using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Button startMultiplayer;
    private void Start()
    {
        Debug.Log("Conectando a servidor");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Uniendonos al Lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Se ha unido al Lobby");
        startMultiplayer.interactable = true;
    }

    public void FindMatch()
    {
        //PhotonNetwork.JoinRandomRoom();
        JoinInRoom();
        //MakeRoom();
    }

    public void JoinInRoom()
    {
        PhotonNetwork.JoinRoom("Room500");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Ingreso a sala fallido. Creando sala");
        MakeRoom();
    }

    public void MakeRoom()
    {
        //int randomRoom = Random.Range(0,5000);
        int randomRoom = 500;
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            PublishUserId = true,
            MaxPlayers = 4,
        };
        //PhotonNetwork.JoinOrCreateRoom($"Room{randomRoom}", roomOptions,null);
        PhotonNetwork.CreateRoom($"Room{randomRoom}",roomOptions);
        Debug.Log($"Sala{randomRoom} creada");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("En Sala de Juego");
        Debug.Log($"Sala: {PhotonNetwork.CountOfRooms}, {PhotonNetwork.CurrentRoom.Name}, Players: {PhotonNetwork.CountOfPlayersInRooms}");
        //Ojo, esto fue agregado el 3 de septiembre, hasta entonces no había nada acá
        PhotonNetwork.LoadLevel(1);
    }
}
