using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RoomsMenu : MonoBehaviourPunCallbacks
{
    //[SerializeField] private Dropdown scrollRect;
    [SerializeField] private Transform _content;
    //[SerializeField] private ScrollView scrollView;
    [SerializeField] private RoomListing _roomListing;

    private List<RoomListing> _listings = new List<RoomListing>();


    [SerializeField] private float space;

    private Vector2 lastPosition;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //if (roomLists != null)
        //{
        //    foreach (var room in roomLists)
        //    {
        //        Destroy(room);
        //    }
        //}
        

        //scrollRect.AddOptions(roomList);
        foreach (RoomInfo info in roomList)
        {
            Debug.Log(info);

            int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
            if (index != -1)
            {
                if (info.RemovedFromList)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
                else
                {
                    _listings[index].SetRoomInfo(info);
                }
            }
            else
            {
                RoomListing listing = Instantiate(_roomListing, _content);

                //RectTransform rectTransform = listing.GetComponent<RectTransform>();
                //rectTransform.localPosition += Vector3.up * (-space + lastPosition.y);
                //lastPosition = rectTransform.localPosition;

                listing.SetRoomInfo(info);
                _listings.Add(listing);
            }
        }
    }
}
