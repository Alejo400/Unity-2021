using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sharedIntance;

    [SerializeField]
    [Tooltip("Puntos de score")]
    int amount;

    /*
     * Nivel extra de seguridad para prevenir que el valor sea 0, exceda de 9999, etc 
     */
    public int Amount{
        get => amount;
        set => amount = value;
    }

    private void Awake()
    {
        if (sharedIntance == null)
            sharedIntance = this;
        else
            Destroy(gameObject);
    }
}
