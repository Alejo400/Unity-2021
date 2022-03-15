using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    int amount;
    public Slider HP;

    private void Start()
    {
        HP.maxValue = amount;
        HP.value = amount;
    }

    public int Amount
    {
        get => amount;
        set {
            amount = value;
            HP.value = amount;
            if(amount <= 0)
            {
                if (gameObject.CompareTag("Player"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
