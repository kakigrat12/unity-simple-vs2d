using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSettings : MonoBehaviour
{
    //[SerializeField] private Text _roomName;
    [SerializeField] private Dropdown dropdown;

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            dropdown.value = (int)PhotonNetwork.CurrentRoom.CustomProperties["pIT"] - 1;
        }
    }

    public RoomOptions Settings()
    {
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 12,
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        };

        roomOptions.CustomRoomProperties["pIT"] = dropdown.value + 1;

        return roomOptions;

        //roomInformation.CountPlayersInTeam = dropdown.value + 1;
        //PhotonNetwork.CreateRoom(_roomName.text, roomOptions);
    }
}
