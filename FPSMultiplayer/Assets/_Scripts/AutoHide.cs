using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    [SerializeField]
    public float timeTohideObject;
    public string obstacle;
    private void OnEnable()
    {
        StartCoroutine(HideTime());
    }
    private void OnTriggerEnter(Collider other)
    {
        //Hide bullet when hit with any wall
        if (other.CompareTag(obstacle))
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
