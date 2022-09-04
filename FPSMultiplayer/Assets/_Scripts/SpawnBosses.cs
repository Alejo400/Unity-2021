using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnBosses : MonoBehaviour
{
    [SerializeField]
    string nameOfObjetc;
    public GameObject _object;
    [SerializeField]
    float timeToInitRespawn, timeForEachInstantiate, increaseRandomLeftPosition, increaseRandomRightPosition,
        increaseRandomDownPosition, increaseRandomUpPosition;
    [SerializeField]
    int amountOfObjectsToInstantiate;
    SpawnObjects _spawnObjects;
    Animator _animator;
    public GameObject InvokeEffect;

    private void Awake()
    {
        _spawnObjects = GetComponent<SpawnObjects>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(_spawnObjects.instantiateObject(timeToInitRespawn, amountOfObjectsToInstantiate, _object,
        increaseRandomDownPosition, increaseRandomUpPosition, increaseRandomLeftPosition,
        increaseRandomRightPosition, nameOfObjetc, timeForEachInstantiate, InvokeEffect));
        StartCoroutine(AnimationCallBosses());
    }
    IEnumerator AnimationCallBosses()
    {
        yield return new WaitForSeconds(timeToInitRespawn / 1.3f);
        while (true)
        {
            GameObject.Find("Dark Magic").GetComponent<AudioSource>().Play();
            _animator.SetBool("Invoke",true);
            yield return new WaitForSeconds(_animator.GetAnimatorTransitionInfo(0).duration);
            _animator.SetBool("Invoke", false);
            yield return new WaitForSeconds(timeForEachInstantiate);
        }
    }
    
}
