using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheakIsMaster : MonoBehaviour
{
    public UnityEvent IsMaster;
    public UnityEvent NotIsMaster;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            IsMaster?.Invoke();
        }
        else
        {
            NotIsMaster?.Invoke();
        }
    }
}
