using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooling : MonoBehaviour
{
    public static ObjectsPooling _sharedIntance;
    public GameObject prefab;

    [SerializeField]
    public int amountPool;
    public List<GameObject> objetcsOfPool;

    private void Awake()
    {
        if (_sharedIntance == null)
        {
            _sharedIntance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Ya hay un pool de balas");
        }
    }

    private void Start()
    {
        GameObject tmp;
        for (int i = 0; i < amountPool; i++)
        {
            tmp = Instantiate(prefab);
            tmp.SetActive(false);
            objetcsOfPool.Add(tmp);
        }
    }
    /// <summary>
    /// Return the first objects in the pool to use in the player shoot
    /// </summary>
    /// <returns></returns>
    public GameObject GetFirstPrefabOfPool()
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (!objetcsOfPool[i].activeInHierarchy)
            {
                return objetcsOfPool[i];
            }
        }
        return null;
    }

}
