using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{

    public float velocity, increaseToRun, timeToFinishPowerUpV;
    public int healthPowerUP, speedPowerUp, runPowerUp;

    float moveX, moveZ, space, defaultVelocity, run;
    Vector3 direction;
    bool isRun, isMoving, isPointing, isActivePowerUpVelocity;

    public bool IsRun { get => isRun; set => isRun = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public bool IsPoiting { get => isPointing; set => isPointing = value; }
    public bool IsActivePowerUpVelocity { get => isActivePowerUpVelocity; set => isActivePowerUpVelocity = value; }

    Rigidbody _rigibody;
    Animator  _animator;
    Health _health;
    PlayerShoot _playerShoot;
    public AudioSource walkSound;
    [SerializeField]
    float timeBetweenWalk = 0.5f, timeToWalk = 0, timeBetweenRun, defaultTimeBetweenWalk;
    public PhotonView photonView;
    public GameObject[] PUVelocityEffect;
    private void Awake()
    {
        run = velocity * increaseToRun;
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
        _health = GetComponent<Health>();
        _playerShoot = GetComponent<PlayerShoot>();
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
        if (PhotonNetwork.InRoom)
        {
            photonView.RPC("PlaySFX",RpcTarget.All,photonView.ViewID);
        }
        else
        {
            PlaySFX(photonView.ViewID);
        }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUpH"))
        {
            //Power up to give more health points player
            GameObject.Find("heal").GetComponent<AudioSource>().Play();
            _health.Amount += healthPowerUP;
            Destroy(other.gameObject);

        }else if (other.gameObject.CompareTag("PowerUpV") && !isActivePowerUpVelocity)
        {
            //Show and active power up of velocity with pickup sound
            appearPowerUPV();
            GameObject.Find("electricSound").GetComponent<AudioSource>().Play();
            IsActivePowerUpVelocity = true;
            //Effect of power up: the player can shoot more fast
            _playerShoot.TimeBetweenFire = (_playerShoot.TimeBetweenFire / 2);
            velocity += speedPowerUp; defaultVelocity += speedPowerUp; run += runPowerUp;
            //Destroy the power up after pickup
            Destroy(other.gameObject);
            //Init function to dissapear PU by parts
            StartCoroutine(disappearPowerUpV());
            //Function to desactive and remove effects of power up. Remove the last part of power up also
            Invoke("removePowerUpV",timeToFinishPowerUpV);
        }
    }
    /// <summary>
    /// remove fire and movement speed increase
    /// when the time defined for timeToFinishPowerUpV has finished
    /// </summary>
    void removePowerUpV()
    {
        IsActivePowerUpVelocity = false;
        _playerShoot.TimeBetweenFire = _playerShoot.DefaultTimeBetweenFire;
        velocity -= speedPowerUp; defaultVelocity -= speedPowerUp; run -= runPowerUp;
        //last part of power up velocity
        PUVelocityEffect[PUVelocityEffect.Length-1].SetActive(false);
    }
    void appearPowerUPV()
    {
        foreach (var indicators in PUVelocityEffect)
        {
            indicators.SetActive(true);
        }
    }
    IEnumerator disappearPowerUpV()
    {
        int ReducePUVelocityEffect = PUVelocityEffect.Length - 1;
        for (int i = 0; i < ReducePUVelocityEffect; i++)
        {
            yield return new WaitForSeconds(timeToFinishPowerUpV / ReducePUVelocityEffect);
            PUVelocityEffect[i].SetActive(false);
        }
    }
    /// <summary>
    /// Play walking and running sounds
    /// </summary>
    [PunRPC]
    void PlaySFX(int viewID)
    {
            if (isMoving)
            {
                if (Time.time >= timeToWalk)
                {
                    timeToWalk = Time.time + timeBetweenWalk;

                    if(photonView.ViewID == viewID)
                    {
                        walkSound.Play();
                    }
                    
                }
            }
            if (isRun)
                timeBetweenWalk = timeBetweenRun;
            else
                timeBetweenWalk = defaultTimeBetweenWalk;
    }
}
