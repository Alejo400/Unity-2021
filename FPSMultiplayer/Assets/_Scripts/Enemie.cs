using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Enemie : MonoBehaviour
{
    [SerializeField]
    float timeToDestroy;
    NavMeshAgent agent;
    GameObject[] players;
    Animator _animator;
    Health _health;
    Rigidbody _rigibody;
    float velocity;
    bool onAttack;
    GameManager gameManager;
    Vector3 target;

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameManager._sharedIntance;
        agent = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _rigibody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (gameManager.currentStateGame != StateGame.gameOver && !_health.ObjectIsDie())
        {
            Movement();
            transform.forward = LookAt();
            onAttack = agent.remainingDistance >= 2 ? false : true;
            Attack(onAttack);
        }
    }

    void Movement()
    {
        //ojo modificar
        target = players[0].transform.position;
        agent.destination = target;
        //
        velocity = agent.velocity.magnitude;
        _animator.SetFloat("velocity", velocity);
        _animator.SetFloat("moveX", agent.velocity.x);
        _animator.SetFloat("moveY", agent.velocity.z);
    }
    /// <summary>
    /// Attack depending IA RemainingDistance
    /// </summary>
    /// <param name="attack">if attack or not</param>
    void Attack(bool attack) {
        _animator.SetBool("Attack", attack);
        agent.isStopped = attack;
    }
    public void Die(){
        agent.isStopped = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        _animator.SetTrigger("Die");
        Destroy(gameObject,timeToDestroy);
    }
    /// <summary>
    /// Look target
    /// </summary>
    Vector3 LookAt()
    {
        Vector3 directionToLook = Vector3.Normalize(target - transform.position);
        directionToLook.y = 0;
        return directionToLook;
    }
}
