using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{

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
        Vector3 spawnPosition = new Vector3(0, 0, Random.Range(1, 5));
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate("HPCharacter", spawnPosition, Quaternion.identity);
            Debug.Log($"¡Hola! Bienvenido a la sala: {PhotonNetwork.CurrentRoom.Name}");
        }
        else
        {
            Instantiate(Resources.Load("HPCharacter"), spawnPosition, Quaternion.identity);
        }
    }
}
