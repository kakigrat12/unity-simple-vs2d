using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;

public class SwitchRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] objToDisable;
    //public static LeaveRoom current;
    //public int DisabledMasterClients;

    //private void Awake()
    //{
    //    current = this;
    //}

    private void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;

        var player = PhotonNetwork.LocalPlayer;
        var playerObject = (Transform)player.TagObject;
        if (playerObject != null) playerObject.GetComponent<PhotonView>().RPC("ChangeHealth", RpcTarget.AllBuffered, 999, null, null, player);
    }

    private void OnDestroy()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    public void Leave()
    {
        //PhotonNetwork.LeaveLobby();
        //PhotonNetwork.InternalCleanPhotonMonoFromSceneIfStuck();
        //SceneManager.LoadScene(1);
        PhotonNetwork.CurrentRoom.CustomProperties.Clear();
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        PhotonNetwork.LeaveRoom();
        //Application.Quit();
        //PhotonNetwork.LoadLevel(0);
    }

    public void SynChangeScene(int number)
    {
        Debug.Log("JJJJ");
        if (!PhotonNetwork.IsMasterClient) return;
        //StartCoroutine(MoveToGameScene());
        //PhotonNetwork.LoadLevel(2);

        //object[] data = new object[1] { number };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(0, number, raiseEventOptions, SendOptions.SendReliable);
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == 0)
        {
            int number = (int)obj.CustomData;
            if(number != gameObject.scene.buildIndex)
            {
                PhotonNetwork.LoadLevel((int)obj.CustomData);
            }
        }
    }

    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        otherPlayer.LeaveCurrentTeam();
    //    }
    //}

    private void ChangeScene(int number, bool isAsync)
    {
        PhotonNetwork.LocalPlayer.LeaveCurrentTeam();
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "teams—olors", null } });

        //foreach (var s in scenesToLoad) s.allowSceneActivation = false;
        //var s = SceneManager.LoadSceneAsync(number, LoadSceneMode.Additive);
        //scenesToLoad.Add(s);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        if (objToDisable != null)
            foreach (var s in objToDisable)
                s.SetActive(false);

        if (isAsync) 
        {
            SceneManager.LoadSceneAsync(number, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(number);
        }

        //scenesToLoad[scenesToLoad.Count - 1].allowSceneActivation = true;
        //SceneManager.LoadScene(number);
        //PhotonNetwork.LoadLevel(number);
    }

    public void ChangeSceneAsync(int number)
    {
        ChangeScene(number, true);
    }

    public override void OnLeftRoom()
    {
        ChangeScene(0, false);
    }

    //public override void OnLeftRoom()
    //{
    //    SceneManager.LoadScene(0); 
    //    //PhotonNetwork.LocalPlayer.LeaveCurrentTeam();
    //    //SceneManager.LoadScene(1);
    //    //PhotonNetwork.CurrentRoom.CustomProperties.Clear();
    //    //PhotonNetwork.LocalPlayer.CustomProperties.Clear();
    //}

    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    if (otherPlayer.ActorNumber == 1 + DisabledMasterClients) DisabledMasterClients++;
    //}
}
