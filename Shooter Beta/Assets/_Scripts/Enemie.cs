using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [SerializeField]
    int amountLife = 50;
    private void OnDestroy()
    {
        ScoreManager.sharedIntance.Amount += amountLife;
    }
}
