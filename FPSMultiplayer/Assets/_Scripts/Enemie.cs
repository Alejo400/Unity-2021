using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Enemie : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject[] players;
    Animator _animator;
    float velocity;
    bool onAttack;

    GameManager gameManager = GameManager._sharedIntance;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if(gameManager.currentStateGame != StateGame.gameOver)
        {
            Movement();
            onAttack = agent.remainingDistance >= 2 ? false : true;
            Attack(onAttack);
        }
    }

    void Movement()
    {
        agent.destination = players[Random.Range(0,PhotonNetwork.CountOfPlayersInRooms)].transform.position;
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
