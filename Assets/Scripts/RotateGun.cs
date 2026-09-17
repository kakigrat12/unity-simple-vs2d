using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RotateGun : MonoBehaviour
{
    [SerializeField] private CharacterController2D characterController;
    private Camera camera;
    private bool isActive = true;
    
    //private bool m_FacingRight = true;

    public PhotonView photonView;

    private void Start()
    {
        camera = Camera.main;
        GameEvents.current.onIsAnyPanelOpened += ChangeIsActive;
    }

    private void OnDisable()
    {
        GameEvents.current.onIsAnyPanelOpened -= ChangeIsActive;
    }

    private void ChangeIsActive(bool newValue)
    {
        isActive = !newValue;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetMouseButton(1) && isActive)
        {
            var difference = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angel = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //if((angel > 90 | angel < -90) & m_FacingRight)
            //{
            //    //GameEvents.current.Flip(photonView.ViewID);

            //    FlipEvent();
            //}
            //else if (!(angel > 90 | angel < -90) & !m_FacingRight)
            //{
            //    //GameEvents.current.Flip(photonView.ViewID);

            //    FlipEvent();
            //}
            transform.rotation = Quaternion.Euler(0f, 0f, angel);
        }
        else
        {
            if (characterController.m_FacingRight)
            {
                transform.rotation = Quaternion.identity;
            }
            else 
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward); 
            }
        }
    }

    

    //private void FlipEvent()
    //{
    //    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
    //    PhotonNetwork.RaiseEvent(0, photonView.ViewID, raiseEventOptions, SendOptions.SendUnreliable);

    //    //FacingState();
    //}

    //private void FacingState()
    //{
    //    m_FacingRight = !m_FacingRight;
    //}

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    if (photonView.ViewID == (int)obj.CustomData)
    //    {
    //        FacingState();
    //    }
    //}

    //private void Flip()
    //{
    //    m_FacingRight = !m_FacingRight;
    //    spriteRenderer.flipY = !spriteRenderer.flipY;
    //}
}
