using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WaiterLoadingScenes : MonoBehaviourPunCallbacks
{
    //[SerializeField] private PhotonView photonView;
    [HideInInspector] public int playersOnScene;

    public event Action OnNewPlayer;
    public UnityEvent Loaded;


    private void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;

        SceneManager.SetActiveScene(gameObject.scene);
        LevelWasLoaded(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void LevelWasLoaded(int level)
    {
        if(!PhotonNetwork.LocalPlayer.IsMasterClient) Loaded?.Invoke();
        PhotonNetwork.LocalPlayer.SetCustomProperties( new ExitGames.Client.Photon.Hashtable() { { "numberScene", level } });
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(1, level, raiseEventOptions, SendOptions.SendReliable);
        //photonView.RPC(nameof(NewPlayerOnScene), RpcTarget.AllBufferedViaServer, level);
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == 1)
        {
            //(int)obj.CustomData
            //Invoke(nameof(NewPlayerOnScene), 1f);
            NewPlayerOnScene();
        }
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        NewPlayerOnScene();
    }

    //[PunRPC]
    private void NewPlayerOnScene() //int level
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        int numberScene = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if ((int)player.CustomProperties["numberScene"] == SceneManager.GetActiveScene().buildIndex) numberScene++;
        }
        playersOnScene = numberScene;

        //if (SceneManager.GetActiveScene().buildIndex == level)
        //{
        //    playersOnScene++;
        //}
        //else
        //{
        //    playersOnScene--;
        //}
        
        OnNewPlayer?.Invoke();
        if (PhotonNetwork.CurrentRoom.PlayerCount == playersOnScene && PhotonNetwork.LocalPlayer.IsMasterClient) Loaded?.Invoke();
    }
}
