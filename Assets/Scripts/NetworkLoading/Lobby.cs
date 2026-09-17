using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UIElements;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomSettings roomSettings;
    [SerializeField] private GameObject startBotton;
    [SerializeField] private GameObject connectionText;
    [SerializeField] private GameObject roomsPanel;

    [Space]

    [SerializeField] private Text _roomName;
    //[SerializeField] private Dropdown dropdown;
    //[SerializeField] private RoomInformation roomInformation;

    [SerializeField] private Text _nickName;

    //SpawnPlayer spawnPlayer;
    public void StartGame()
    {
        PhotonNetwork.GameVersion = "1";
        //DontDestroyOnLoad(gameObject);
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        startBotton.SetActive(false);
        connectionText.SetActive(true);
        //PhotonNetwork.ConnectToBestCloudServer();
        //PhotonNetwork.ConnectToRegion("ru");
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ChangeNicname()
    {
        PhotonNetwork.LocalPlayer.NickName = _nickName.text;
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_roomName.text, roomSettings.Settings());
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("JoinRoom");

        PhotonNetwork.LoadLevel(1);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");

        startBotton.SetActive(false);
        connectionText.SetActive(false);
        roomsPanel.SetActive(true);

        //PhotonNetwork.ConnectToRegion("ru");
        PhotonNetwork.JoinLobby();

        //PhotonNetwork.JoinRandomRoom();
    }
}
