using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("HideObject",2);
    }

    void HideObject()
    {
        gameObject.SetActive(false);
    }
}
