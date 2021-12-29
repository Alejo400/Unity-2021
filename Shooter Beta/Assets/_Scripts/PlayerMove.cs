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
    Vector3 direction;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal,0,moveVertical).normalized;
        
        //space = velocity * Time.deltaTime;
        //transform.Translate(direction * space);

        //angle = rotationVelocity * Time.deltaTime;
        mouseX = Input.GetAxis("Mouse X");
        //transform.Rotate(0,mouseX * rotationVelocity,0);


    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(direction * velocity * Time.deltaTime);
        rb.AddRelativeTorque(0, mouseX * rotationVelocity * Time.deltaTime, 0);
    }
}
