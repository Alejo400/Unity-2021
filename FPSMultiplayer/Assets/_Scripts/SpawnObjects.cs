using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnObjects : MonoBehaviour
{
    float timeForEachInstantiateTo;
    public float timeToAppearEnemie;
    GameObject[] ParticlesEffects;
    public float TimeForEachInstantiateTo { get => timeForEachInstantiateTo; set => timeForEachInstantiateTo = value; }
    /// <summary>
    /// Instanciar un objeto para modo offline u online
    /// </summary>
    /// <param name="timeToInitRespawn">Segundos para iniciar respawn</param>
    /// <param name="amountOfObjectsToInstantiate"></param>
    /// <param name="_object"></param>
    /// <param name="increaseRandomDownPosition">valor mínimo de Random.Range para la posición en X del objeto</param>
    /// <param name="increaseRandomUpPosition">valor máximo de Random.Range para la posición en X del objeto</param>
    /// <param name="increaseRandomLeftPosition">valor mínimo de Random.Range para la posición en Z del objeto</param>
    /// <param name="increaseRandomRightPosition">valor máximo de Random.Range para la posición en Z del objeto</param>
    /// <param name="nameOfObjetc"></param>
    /// <param name="timeForEachInstantiate">Segundos por cada instancia</param>
    /// <returns></returns>
    public IEnumerator instantiateObject(float timeToInitRespawn,int amountOfObjectsToInstantiate,GameObject _object,
        float increaseRandomDownPosition, float increaseRandomUpPosition, float increaseRandomLeftPosition, 
        float increaseRandomRightPosition, string nameOfObjetc, float timeForEachInstantiate, GameObject invokeEffect)
    {
        timeForEachInstantiateTo = timeForEachInstantiate;
        yield return new WaitForSeconds(timeToInitRespawn);
        while (true)
        {
            for (int i = 0; i < amountOfObjectsToInstantiate; i++)
            {
                Vector3 spawnPosition = new Vector3(
                _object.transform.position.x * Random.Range(increaseRandomDownPosition, increaseRandomUpPosition),
                _object.transform.position.y + 1, 
                _object.transform.position.z * Random.Range(increaseRandomLeftPosition, increaseRandomRightPosition));

                if (PhotonNetwork.InRoom)
                {
                    PhotonNetwork.Instantiate(invokeEffect.name, new Vector3(spawnPosition.x,
                    invokeEffect.transform.position.y,spawnPosition.z), invokeEffect.transform.rotation);
                    yield return new WaitForSeconds(timeToAppearEnemie);

                    ParticlesEffects = GameObject.FindGameObjectsWithTag("TemporalObject");
                    DestroyTemporalParticlesEffects(ParticlesEffects);

                    PhotonNetwork.Instantiate(nameOfObjetc, spawnPosition, Quaternion.identity);
                }
                else
                {

                    Instantiate(Resources.Load(invokeEffect.name), new Vector3(spawnPosition.x,
                    invokeEffect.transform.position.y, spawnPosition.z), invokeEffect.transform.rotation);
                    yield return new WaitForSeconds(timeToAppearEnemie);

                    ParticlesEffects = GameObject.FindGameObjectsWithTag("TemporalObject");
                    DestroyTemporalParticlesEffects(ParticlesEffects);

                    Instantiate(Resources.Load(nameOfObjetc), spawnPosition, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(timeForEachInstantiateTo);
        }
    }
    void DestroyTemporalParticlesEffects(GameObject[] ParticlesEffects)
    {
        foreach (GameObject particles in ParticlesEffects)
        {
            Destroy(particles);
        }
    }
}
