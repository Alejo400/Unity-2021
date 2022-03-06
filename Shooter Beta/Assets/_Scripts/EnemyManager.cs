using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager sharedIntance;
    public List<Enemie> enemies;

    private void Awake()
    {
        if(sharedIntance == null) 
        {
            sharedIntance = this;
        }
        else
        {
            Destroy(gameObject);
        }   
    }
}
