using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class LookForward : MonoBehaviour
{
    public Transform player;
    Vector3 direction;
    // Update is called once per frame
    public LineRenderer laserLineRenderer;
    [SerializeField]
    public float laserWidth = 0.05f, laserMaxLength = 5f;
    Vector3[] initLaserPositions;
    private void Start()
    {
        initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.startWidth = laserWidth;
    }
    void Update()
    {
        //Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        //Direccion local de este objeto apuntando hacia donde la mira apunte en direccion frontal
        Vector3 direction = transform.TransformDirection(Vector3.up);

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            //Debug.DrawRay(transform.position, direction * hit.distance, Color.red);

            if (hit.collider.CompareTag("Enemie")) //si detectamos un enemigo durante nuestro hit con el ray cast
            {
                laserLineRenderer.enabled = true;
                laserLineRenderer.SetPosition(0, transform.position); //posicion del objeto
                laserLineRenderer.SetPosition(1, hit.point); //punto de impacto local del objeto con tag enemie
            }
            else
            {
                laserLineRenderer.enabled = false;
                laserLineRenderer.SetPositions(initLaserPositions);
            }
        }

    }
}
