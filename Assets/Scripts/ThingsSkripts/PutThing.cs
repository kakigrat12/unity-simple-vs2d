using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class PutThing : MonoBehaviour//, IPunObservable
{
    [SerializeField]
    private bool isMouseEnter = false;
    //public bool Put = false;
    [SerializeField]
    private Collider2D inventoryCollider;

    private PhotonView photonView;
    //private Inventory inventory;


    //private void Start()
    //{
    //    GameEvents.current.onThrow += StartScript;
    //    PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    //    photonView = GetComponent<PhotonView>();
    //    //if (!photonView.IsMine) enabled = false;
    //}

    //private void OnMouseEnter()
    //{
    //    isMouseEnter = true;
    //}
    //private void OnMouseExit()
    //{
    //    isMouseEnter = false;
    //}


    //private void Update()
    //{
    //    if(isMouseEnter && inventoryCollider != null)
    //    {
    //        if (Input.GetKeyDown("e") && inventoryCollider.GetComponentInParent<PhotonView>().IsMine)
    //        {
    //            //Put = true;
    //            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
    //            PhotonNetwork.RaiseEvent(1, photonView.ViewID, raiseEventOptions, SendOptions.SendUnreliable);
    //        }
            
            
    //    }

    //    //if (Put)
    //    //{
    //    //    Putting();
    //    //}
    //}

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    if (obj.Code == 1 && photonView.ViewID == (int)obj.CustomData)
    //    {
    //        Putting();
    //    }
    //}

    ////public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    ////{
    ////    if (stream.IsWriting)
    ////    {
    ////        stream.SendNext(Put);
    ////    }
    ////    else
    ////    {
    ////        Put = (bool)stream.ReceiveNext();
    ////    }
    ////}

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.tag == "Inventory")
    //    {
    //        inventoryCollider = collider;
    //        //State(collision);
    //        //PhotonView photonView = collider.GetComponentInParent<PhotonView>();
    //        //if (photonView.IsMine)
    //        //{
    //            //canTake = true;
    //            //inventory = collider.GetComponentInChildren<Inventory>();
    //        //}
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collider)
    //{
    //    if (collider.tag == "Inventory")
    //    {
    //        inventoryCollider = null;
    //        //State(collision);
    //        //PhotonView photonView = collision.GetComponentInParent<PhotonView>();
    //        //if (photonView.IsMine)
    //        //{
    //        //    canTake = false;
    //        //    inventory = null;
    //        //}
    //    }
    //}

    ////private void State(Collider2D collision)
    ////{
    ////    //Inventory inventoryForCheck = collision.GetComponent<Inventory>();
    ////    PhotonView photonView = collision.GetComponentInParent<PhotonView>();
    ////    if (photonView.IsMine)
    ////    {
    ////        canTake = !canTake;
    ////        inventory = collision.GetComponent<Inventory>();
    ////    }
    ////}

    //private void Putting()
    //{
    //    Debug.Log("isPut");
    //    //PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    //    inventoryCollider.GetComponent<Inventory>().ThingsHandler(gameObject);
    //    //enabled = false;
    //    //Destroy(photonView);
    //    //gameObject.SetActive(false);
    //}

    //private void StartScript(int id)
    //{
    //    if(id == photonView.ViewID)
    //    {
    //        enabled = true;
    //    }
    //}
}
