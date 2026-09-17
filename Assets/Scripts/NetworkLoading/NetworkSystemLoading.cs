using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkSystemLoading : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject bottonStartGame;
    [SerializeField] private Text playersInRoomText;

    [Space]

    [SerializeField] int minCountPlayers;
    [SerializeField] private RoomInformation roomInformation;

    private bool newSceneDidNotFinishLoading = true;

    private void Start()
    {
        //PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        OnPlayerListUpdate();
        //StartGame();
    }

    private void OnDestroy()
    {
        //PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        OnPlayerListUpdate();
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        OnPlayerListUpdate();
    }

    private void OnPlayerListUpdate()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        playersInRoomText.text = playerCount.ToString();
        if (playerCount >= minCountPlayers && playerCount % roomInformation.CountPlayersInTeam == 0 && PhotonNetwork.IsMasterClient)
        {
            bottonStartGame.SetActive(true);
        }
        else
        {
            bottonStartGame.SetActive(false);
        }
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if(obj.Code == 4)
        {
            //PhotonNetwork.LoadLevel(2);
            //SceneManager.LoadScene(2);
        }
    }

    //public void StartGame()
    //{
    //    if (!PhotonNetwork.IsMasterClient) return;
    //    //StartCoroutine(MoveToGameScene());
    //    //PhotonNetwork.LoadLevel(2);

    //    //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
    //    //PhotonNetwork.RaiseEvent(4, "StatrGame", raiseEventOptions, SendOptions.SendReliable);
    //}

    private void OnLevelWasLoaded(int level)
    {
        if(level == 2)
        {
            newSceneDidNotFinishLoading = false;
        }
    }

    private IEnumerator MoveToGameScene()
    {
        // Temporary disable processing of futher network messages
        PhotonNetwork.IsMessageQueueRunning = false;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        //PhotonNetwork.LoadLevel(2);

        while (newSceneDidNotFinishLoading)
        {
            Debug.Log("kkk");
            yield return null;
        }
        //yield return new WaitForSeconds(1f);
        PhotonNetwork.IsMessageQueueRunning = true;
    }
}
