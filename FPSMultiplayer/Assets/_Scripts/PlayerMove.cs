using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float velocity;

    float moveX, moveZ, space, run, defaultVelocity;
    Vector3 direction;
    bool isRun, isMoving, isPointing;

    public bool IsRun { get => isRun; set => isRun = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public bool IsPoiting { get => isPointing; set => isPointing = value; }

    Rigidbody _rigibody;
    Animator  _animator;
    public AudioSource walkSound;
    [SerializeField]
    float timeBetweenWalk = 0.5f, timeToWalk = 0, timeBetweenRun, defaultTimeBetweenWalk;
    public PhotonView photonView;
    private void Awake()
    {
        run = velocity * 2.2f;
        defaultVelocity = velocity;

        isRun = false;
        isMoving = false;
        isPointing = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        defaultTimeBetweenWalk = timeBetweenWalk;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom && !photonView.IsMine)
        {
            return;
        }
        MovePosition();
        Animations();
        AimShoot();
        PlaySFX();
    }
    /// <summary>
    /// Move in X and Z using physics forces
    /// </summary>
    void MovePosition()
    {
        space = velocity * Time.deltaTime;
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        direction = new Vector3(moveX, 0, moveZ).normalized;

        if (isPointing == false)
            _rigibody.AddRelativeForce(direction * space);

        if (Mathf.Abs(moveX) > 0 || Mathf.Abs(moveZ) > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving == false || isPointing == true)
            _rigibody.velocity = Vector3.zero;

    }
    /// <summary>
    /// management of animations
    /// </summary>
    void Animations()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isMoving == true)
        {
            isRun = true;
            _animator.SetBool("run", true);
            velocity = run;
        }
        else
        {
            isRun = false;
            velocity = defaultVelocity;
            _animator.SetBool("run", false);
        }
        
        _animator.SetFloat("velocity", _rigibody.velocity.magnitude);
        _animator.SetFloat("moveX", moveX);
        _animator.SetFloat("moveZ", moveZ);
    }
    /// <summary>
    /// Aim with the Player of any animation state
    /// </summary>
    void AimShoot()
    {
        if (Input.GetButton("Fire2"))
        {
            isMoving = false;
            isPointing = true;
            isRun = false;
            _animator.SetBool("aim", true);
        }
        else
        {
            isPointing = false;
            _animator.SetBool("aim", false);
        }
    }
    /// <summary>
    /// Ejecutar bajo tiempo los sonidos de caminar, correr
    /// </summary>
    void PlaySFX()
    {
        if (isMoving)
        {
            if (Time.time >= timeToWalk)
            {
                timeToWalk = Time.time + timeBetweenWalk;
                walkSound.Play();
            }
        }
        if (isRun)
            timeBetweenWalk = timeBetweenRun;
        else
            timeBetweenWalk = defaultTimeBetweenWalk;
    }
}
