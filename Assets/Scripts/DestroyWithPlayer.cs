using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithPlayer : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public Player ThisPlayer;

    private void Start()
    {
        //GameEvents.current.onKill += Kill;
    }
    private void Kill(Player killer, Player murdered)
    {
        PlayerLeft(murdered);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerLeft(otherPlayer);
    }
    private void PlayerLeft(Player player)
    {
        if (player == ThisPlayer)
        {
            Destroy(gameObject);
        }
    }
}
