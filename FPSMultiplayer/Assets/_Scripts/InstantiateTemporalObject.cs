using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTemporalObject : MonoBehaviour
{
    public static InstantiateTemporalObject _sharedIntance;
    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
    }
    /// <summary>
    /// Instantiate a particle in a specific position
    /// </summary>
    /// <param name="particle">GameObject with particle</param>
    /// <param name="position">Vector3 with the position to instantiate object</param>
    /// <param name="timeToDestroy">Destroy intantiated object</param>
    public void ShowParticle(GameObject particle, Vector3 position, float timeToDestroy)
    {
        Destroy(Instantiate(particle,position,Quaternion.identity),timeToDestroy);
    }
}
