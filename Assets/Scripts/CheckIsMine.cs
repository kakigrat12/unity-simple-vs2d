using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class CheckIsMine : MonoBehaviour
{
    public UnityEvent HitIsMine;
    private void Awake()
    {
        if (!GetComponent<PhotonView>().IsMine) HitIsMine.Invoke();
    }
}
