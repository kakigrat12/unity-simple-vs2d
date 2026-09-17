using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class FlipSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private enum type { X, Y };
    [SerializeField] private type _type;

    //const byte l = 0;

    public PhotonView photonView;

    private void Start()
    {
        //PhotonNetwork.RaiseEvent(l, photonView.ViewID, RaiseEventOptions.Default, SendOptions.SendUnreliable);

        //GameEvents.current.onThrow += End;
        //GameEvents.current.onFlip += Flip;
        //PhotonNetwork.NetworkingClient.current.onFlip += Flip;
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;

        //photonView = PhotonView.;
        photonView = GetComponentInParent<PhotonView>();

        //PhotonNetwork.RaiseEvent(l, photonView.ViewID, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        //Flip((int)obj.CustomData);
        if (obj.Code == 0 && photonView.ViewID == (int)obj.CustomData)
        {
            switch (_type)
            {
                case
                type.X:
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                    break;

                case
                type.Y:
                    spriteRenderer.flipY = !spriteRenderer.flipY;
                    break;
            }
        }
        //Debug.Log((int)obj.CustomData);
    }

    private void End(int id)
    {
        if (id == GetComponent<PhotonView>().ViewID)
        {
            PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
        }
    }

    //private void Flip(int id)
    //{
    //    if (photonView.ViewID == id)
    //    {
    //        switch (_type)
    //        {
    //            case
    //            type.X:
    //                spriteRenderer.flipX = !spriteRenderer.flipX;
    //                break;

    //            case
    //            type.Y:
    //                spriteRenderer.flipY = !spriteRenderer.flipY;
    //                break;
    //        }
    //    }
    //    ///////PhotonNetwork.RaiseEvent(1, spriteRenderer);
    //    //photonView.GetComponent<Health>().TakeDamage(Damage);
    //}

    //public void OnPhotonSerializeView(PhotonStream steram, PhotonMessageInfo info)
    //{
    //    if (steram.IsWriting)
    //    {
    //        switch (_type)
    //        {
    //            case
    //        type.X:
    //                spriteRenderer.flipX = !spriteRenderer.flipX;
    //                break;

    //            case
    //        type.Y:
    //                spriteRenderer.flipY = !spriteRenderer.flipY;
    //                break;
    //        }

    //        //steram.SendNext(_crouch);
    //    }
    //    else
    //    {
    //        //_crouch = (bool)steram.ReceiveNext();
    //        Flip();
    //    }
    //}
}
