using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Fuerza de movimiento en newtons / seg")]
    [Range(0, 2000)]
    public float velocity;

    [SerializeField]
    [Tooltip("Fuerza de rotacion en N / seg")]
    public float rotationVelocity;

    float moveHorizontal, moveVertical, mouseX, space, angle;
    public bool isMoving;
    Vector3 direction;

    Rigidbody _rb;
    Animator _animator;

    private void Awake()
    {
        isMoving = true;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        //isMoving has participation in PlayerShoot.cs
        if (isMoving)
            direction = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        else
            direction = Vector3.zero;

        _animator.SetFloat("velocity", _rb.velocity.magnitude);
        _animator.SetFloat("moveX", moveHorizontal);
        _animator.SetFloat("moveY", moveVertical);

        /* Otra forma de ejecutar las animaciones si la velocidad es mayor que 0 con bool
         * if (Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0)
        {   _animator.SetBool("velocity", true);
            _animator.SetFloat("moveX", moveHorizontal);
            _animator.SetFloat("moveY", moveVertical);
        }
        else
          _animator.SetBool("velocity",false);*/

        //space = velocity * Time.deltaTime;
        //transform.Translate(direction * space);

        //angle = rotationVelocity * Time.deltaTime;
        mouseX = Input.GetAxis("Mouse X");
        //transform.Rotate(0,mouseX * rotationVelocity,0);


    }

    private void FixedUpdate()
    {
        _rb.AddRelativeForce(direction * velocity * Time.deltaTime);
        _rb.AddRelativeTorque(0, mouseX * rotationVelocity * Time.deltaTime, 0);
    }
}
