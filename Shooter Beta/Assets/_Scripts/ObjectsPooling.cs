using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooling : MonoBehaviour
{

    public GameObject prefab;
    public List<GameObject> poolOfObjects;
    public int amountPoolOfObjects;

    public static ObjectsPooling sharedIntance;

    private void Awake()
    {
        if (sharedIntance == null)
            sharedIntance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        createPoolOfObjects();
    }

    void createPoolOfObjects()
    {
        poolOfObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amountPoolOfObjects; i++)
        {
            tmp = Instantiate(prefab);
            tmp.SetActive(false);
            poolOfObjects.Add(tmp);
        }
    }

    public GameObject GetFirstObjectOfPool()
    {
        for (int i = 0; i < amountPoolOfObjects; i++)
        {
            if (!poolOfObjects[i].activeInHierarchy)
            {
                poolOfObjects[i].SetActive(true);
                return poolOfObjects[i];
            }
        }
        return null;
    }

}
