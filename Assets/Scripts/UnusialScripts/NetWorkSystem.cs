using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetWorkSystem : MonoBehaviour
{
    public GameObject PlayerPrefab;

    [SerializeField] private Transform targetCamera;

    //[SerializeField] private GameObject pistol;

    private void Start()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    PhotonNetwork.Instantiate(pistol.name, new Vector3(5f, -6f, 0f), Quaternion.identity);
        //    PhotonNetwork.Instantiate(pistol.name, new Vector3(10f, -6f, 0f), Quaternion.identity);
        //    PhotonNetwork.Instantiate(pistol.name, new Vector3(15f, -6f, 0f), Quaternion.identity);
        //}
        PlayerPrefab = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(0f, 0f, 1f), Quaternion.identity);
        targetCamera.SetParent(PlayerPrefab.transform);
    }
}
