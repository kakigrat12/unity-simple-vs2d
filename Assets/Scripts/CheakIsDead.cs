using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheakIsDead : MonoBehaviour
{
    public UnityEvent HitIsDead;

    private void Start()
    {
        GameEvents.current.onKill += Death;
    }

    private void OnDisable()
    {
        GameEvents.current.onKill -= Death;
    }

    private void Death(Player killer, Player murdered)
    {
        if (murdered == PhotonNetwork.LocalPlayer) HitIsDead?.Invoke();
    }
}
