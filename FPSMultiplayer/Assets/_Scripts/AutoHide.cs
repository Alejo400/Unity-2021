using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    [SerializeField]
    public float hideObject;
    private void OnEnable()
    {
        Invoke("Hide", hideObject);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Hide();
        }
    }
}
