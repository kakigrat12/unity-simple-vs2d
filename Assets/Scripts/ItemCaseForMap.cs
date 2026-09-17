using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCaseForMap : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private PhotonView photonView;
    //private int id;
    [SerializeField] private AssetsItemContainer assetsItemContainer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite closedCase;
    [SerializeField] private Sprite openedCase;
    //[SerializeField] private GameObject effector;
    private bool isOpened;
    private bool playerNear;

    //private Vector2 pos;
    //private Quaternion rot;

    public UnityEvent OnPlayerCollisChange;
    public UnityEvent OnOpened;

    private void Start()
    {
        //PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;

        //id = photonView.ViewID;
        //pos = transform.position;
        //rot = transform.rotation;

        spriteRenderer.sprite = closedCase;
        Invoke("AtivingEffector", 0f);
    }

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    if(obj.Code == 12 && (int)obj.CustomData == id) Opening();
    //}

    private void OnMouseEnter()
    {

        //Debug.Log("sssssssssssssssssssssss");
        if (isOpened) return;

        //Debug.Log("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
        if (playerNear)
        {
            //Debug.Log("ggggggggggggggggggggggggggggggggggggggg");
            StartCoroutine(WaitPressing());
        }

        //var objectsNear = Physics2D.OverlapCircleAll(transform.position, 1f);
        //for (int i = 0; i < objectsNear.Length; i++)
        //{
        //    if (objectsNear[i] == ((Transform)PhotonNetwork.LocalPlayer.TagObject).GetChild(0).GetComponent<Collider2D>())
        //    {
        //        StartCoroutine(WaitPressing());
        //        break;
        //    }
        //}
    }

    private void OnMouseExit()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheackPosPlayer(true, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheackPosPlayer(false, collision);
    }  


    private void CheackPosPlayer(bool value, Collider2D collider)
    {
        if (IsPlayerCollider(collider) && value != playerNear)
        {
            OnPlayerCollisChange?.Invoke();
            playerNear = value;
        }
    }

    private bool IsPlayerCollider(Collider2D collider)
    {
        if (collider == ((Transform)PhotonNetwork.LocalPlayer.TagObject).GetChild(0).GetComponent<Collider2D>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator WaitPressing()
    {
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }
        Debug.Log("нажал");

        //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        //PhotonNetwork.RaiseEvent(12, id, raiseEventOptions, SendOptions.SendReliable);

        photonView.RPC(nameof(Opening), RpcTarget.All);
    }

    //private void AtivingEffector()
    //{
    //    effector.SetActive(effector.activeSelf!);
    //}

    [PunRPC]
    private void Opening()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < number; i++)
            {
                int spawnObjectNumber = assetsItemContainer.assetItems[Random.Range(0, assetsItemContainer.assetItems.Length)].Order;
                object[] data = new object[3] { spawnObjectNumber, 0, 0 };
                PhotonNetwork.InstantiateRoomObject("Thing", new Vector3(transform.position.x + 0.1f / number * i - 0.05f, transform.position.y, -0.1f), transform.rotation, 0, data);
            }
        }

        Invoke("AtivingEffector", 0f);

        spriteRenderer.sprite = openedCase;
        isOpened = true;


        Invoke("AtivingEffector", 0.01f);

        OnOpened?.Invoke();
    }
}
