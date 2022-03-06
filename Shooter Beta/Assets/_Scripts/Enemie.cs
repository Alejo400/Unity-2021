using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [SerializeField]
    int amountLife = 50;

    private void Start()
    {
        EnemyManager.sharedIntance.enemies.Add(this);
    }

    private void OnDestroy()
    {
        EnemyManager.sharedIntance.enemies.Remove(this);
        ScoreManager.sharedIntance.Amount += amountLife;
    }
}
