using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyObject : MonoBehaviour
{
    public GameObject particleDeath;
    Vector3 position;
    
    bool isQuit; //nos ayuda a evitar una carga extra de los objetos al instanciar

    private void Awake()
    {
        position = gameObject.transform.position;
    }
    private void OnDestroy()
    {
        if (isQuit || MenuManager._sharedIntance.sceneRestarted)
        {
            return;
        }
        GameObject objectInstante = Instantiate(particleDeath);
        objectInstante.transform.position = new Vector3(position.x,position.y + 1.5f,position.z);
    }

    private void OnApplicationQuit()
    {
        isQuit = true; 
    }
}
