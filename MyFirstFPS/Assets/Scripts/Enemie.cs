using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemie : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Animator _animator;
    float velocity;
    bool onAttack;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        onAttack = agent.remainingDistance >= 4 ? false : true;
        Attack(onAttack);
    }

    void Movement()
    {
        agent.destination = player.transform.position;
        velocity = agent.velocity.magnitude;
        _animator.SetFloat("velocity", velocity);
        _animator.SetFloat("moveX", agent.velocity.x);
        _animator.SetFloat("moveY", agent.velocity.z);
    }
    /// <summary>
    /// Atacar dependiendo de la distancia entre el enemigo y su objetivo
    /// </summary>
    /// <param name="attack">booleano que determina si atacar o no al player</param>
    void Attack(bool attack) {

        _animator.SetBool("Attack", attack);
        agent.isStopped = attack;
    }
}
