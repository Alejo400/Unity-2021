using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    public GameObject firePoint;

    Animator _animator;
    // Update is called once per frame
    float lastShootTime;
    public float TimeBulletBlast;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        //AimAndShoot();
        BulletBlast(TimeBulletBlast);
    }
    void AimAndShoot()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _animator.SetBool("aim", true);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OpenFire();
            }
        }
        else
        {
            _animator.SetBool("aim", false);
        }
    }
    /// <summary>
    /// FireBlast with the player and desactive movement of player
    /// </summary>
    /// <param name="timeToEndBlast">Time to finish bullet blast and prevent the multiple shoot</param>
    void BulletBlast(float timeToEndBlast)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FindObjectOfType<PlayerMove>().isMoving = false;
            _animator.SetBool("shoot", true);
            InvokeRepeating("OpenFire", 0.4f, 0.4f);
            Invoke("CancelBulletBlast", timeToEndBlast);
        }
    }
    /// <summary>
    /// Cancel multiple fire and active movement of player
    /// </summary>
    void CancelBulletBlast()
    {
        CancelInvoke();
        _animator.SetBool("shoot",false);
        FindObjectOfType<PlayerMove>().isMoving = true;
    }
    /// <summary>
    /// Fire to forward
    /// </summary>
    void OpenFire()
    {
        GameObject prefab = ObjectsPooling.sharedIntance.GetFirstObjectOfPool();
        prefab.transform.position = firePoint.transform.position;
        prefab.transform.rotation = firePoint.transform.rotation;
    }
}
