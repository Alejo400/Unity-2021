using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInstantiate : MonoBehaviour
{
    [SerializeField]
    float timeToInstantiate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BossInvoke());
    }

    IEnumerator BossInvoke()
    {
        yield return new WaitForSeconds(timeToInstantiate);
        Instantiate(Resources.Load("Boss"),transform.position,Quaternion.identity);
    }
}
