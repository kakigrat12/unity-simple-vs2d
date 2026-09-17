using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCurrentRoomSettings : MonoBehaviour
{
    [SerializeField] private RoomSettings roomSettings;

    public void Change()
    {
        var settings = roomSettings.Settings();
        PhotonNetwork.CurrentRoom.MaxPlayers = settings.MaxPlayers;
        PhotonNetwork.CurrentRoom.SetCustomProperties(settings.CustomRoomProperties);
    }
}
