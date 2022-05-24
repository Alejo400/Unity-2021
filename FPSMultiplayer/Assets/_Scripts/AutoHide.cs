using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    [SerializeField]
    public float timeTohideObject;
    private void OnEnable()
    {
        //Invoke("Hide", hideObject);
        StartCoroutine(HideTime());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Hide();
        }
    }
    IEnumerator HideTime()
    {
        yield return new WaitForSeconds(timeTohideObject);
        Hide();
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
