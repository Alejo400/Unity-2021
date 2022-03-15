using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager sharedIntance;

    private void Start()
    {
        if(sharedIntance == null)
        {
            sharedIntance = this;
            //por si cambiamos de escena, preservar valores
            //DontDestroyOnLoad(sharedIntance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        //Suscribiendonos al OnSceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        //Dejar de suscribirse
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Vector3 spawnPosition = new Vector3(-32f,0,Random.Range(1,8));
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate("HPCharacter", spawnPosition, Quaternion.identity);
            Debug.Log($"¡Hola! Bienvenido a la sala: {PhotonNetwork.CurrentRoom.Name}");
        }
        else
        {
            Instantiate(Resources.Load("HPCharacter"),spawnPosition,Quaternion.identity);
        }
    }
}
