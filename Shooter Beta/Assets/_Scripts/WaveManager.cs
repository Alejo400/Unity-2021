using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager sharedIntance;
    public List<respawnPrefabs> listWaves;

    private void Awake()
    {
        if(sharedIntance == null)
        {
            sharedIntance = this;
        }
        else
        {
            Destroy(this);
        }
    }

}
