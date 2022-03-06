using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Hide", 4);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

}
