using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbyStartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    private void StartGame()
    {
        PhotonNetwork.LoadLevel(2);
    }
    //public override void OnLobbyStatisticsUpdate()
    //{
    //    string countPlayersOnline;
    //    countPlayersOnline = PhotonNetwork.countOfPlayers.ToString() + " Players Online";
    //}
}
