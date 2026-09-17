using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public PhotonView Pv;
    [SerializeField] private Rigidbody2D rb;
    [Space]
    [SerializeField] private float distance = 10f;
    [SerializeField] private float speed = 1f;

    private float startDot;
    private float endDot;
    private bool isMovingForward;

    private void Start()
    {
        startDot = transform.position.y;
        endDot = startDot + distance;
        //if (!PhotonNetwork.IsMasterClient) enabled = false;
    }

    private void FixedUpdate()
    {
        //if (!PhotonNetwork.IsMasterClient) return;

        if (isMovingForward)
        {
            if (transform.position.y < endDot)
            {
                Move(speed);
            }
            else
            {
                isMovingForward = false;
            }
        }
        else
        {
            if (transform.position.y > startDot)
            {
                Move(-speed);
            }
            else
            {
                isMovingForward = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var photonViewOther = collision.GetComponent<PhotonView>();
        if (photonViewOther != null && photonViewOther.IsMine)
        {
            int viewID = photonViewOther.ViewID;
            collision.transform.SetParent(transform);
            Pv.RPC(nameof(SetChilde), RpcTarget.OthersBuffered, viewID, collision.transform.localPosition);
            //SetChilde(viewID, photonViewOther.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var photonViewOther = collision.GetComponentInParent<PhotonView>();
        if (photonViewOther != null && photonViewOther.IsMine)
        {
            int viewID = photonViewOther.ViewID;
            Pv.RPC(nameof(TakeAway), RpcTarget.OthersBuffered, viewID);
            TakeAway(viewID);
        }
    }

    [PunRPC]
    private void SetChilde(int viewID, Vector3 localPosition)
    {
        var target = PhotonNetwork.GetPhotonView(viewID).transform;
        target.SetParent(transform);
        target.localPosition = localPosition;
    }

    [PunRPC]
    private void TakeAway(int viewID)
    {
        PhotonNetwork.GetPhotonView(viewID).transform.SetParent(default);
    }


    private void Move(float y)
    {
        //rb.velocity = new Vector2(0, 0);
        transform.position += new Vector3(0f, y) * Time.fixedDeltaTime;
    }
}
