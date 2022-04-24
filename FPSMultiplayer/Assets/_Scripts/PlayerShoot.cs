using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShoot : MonoBehaviour
{
    GameObject objectOfPool;
    public GameObject point;
    PlayerMove Player;
    public AudioSource soundShoot;

    [SerializeField]
    float timeBetweenFire = 5, timeToFire = 0;
    public PhotonView photonView;

    private void Awake()
    {
        Player = gameObject.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom && !photonView.IsMine)
        {
            return;
        }
        Shoot();
    }
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1") & Player.IsRun == false)
        {
            if (Player.IsMoving == true || Player.IsPoiting == true)
            {
                if (Time.time >= timeToFire)
                {

                    timeToFire = Time.time + timeBetweenFire;

                    objectOfPool = ObjectsPooling._sharedIntance.GetFirstPrefabOfPool();
                    objectOfPool.transform.position = point.transform.position;
                    objectOfPool.transform.rotation = transform.rotation;
                    objectOfPool.SetActive(true);

                    if (PhotonNetwork.InRoom)
                    {
                        photonView.RPC("MultiplayerSFXandVFX", RpcTarget.All, photonView.ViewID);
                    }
                    else
                    {
                        MultiplayerSFXandVFX(photonView.ViewID);
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
    [PunRPC]
    void MultiplayerSFXandVFX(int viewID)
    {
        if(photonView.ViewID == viewID)
        {
            soundShoot.Play();
        }
    }
}
