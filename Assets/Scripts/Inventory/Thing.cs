using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class Thing : MonoBehaviour//, IPunInstantiateMagicCallback
{
    public SpriteRenderer SpriteRenderer;

    public AssetsItemContainer assetsItemContainer;
    [HideInInspector] public IItem assetItem;
    private int Order;
    //[SerializeField] private int count;

    //public Animator Animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D trigger;

    [SerializeField] private LayerMask toTrigger;

    //[SerializeField] private Inventory inventory;
    //public ThingPanelRender ThingPanel;

    public PhotonView PhotonView;

    public UnityEvent OnPlayerCollisChange;

    private bool playerNear;
    //public UnityEvent CanTakeUpdate;

    private void Start()
    {
        //PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;

        PhotonView = GetComponent<PhotonView>();

        object[] instantiationData = PhotonView.InstantiationData;

        if (instantiationData == null) return;

        Order = (int)instantiationData[0];
        assetItem = assetsItemContainer.assetItems[Order];


        //if (instantiationData.Length > 1)
        //{
        //    int childOrder = (int)instantiationData[1];
        //    if (childOrder != 0) SetParent(childOrder - 1);
        //}

        //WeaponItemData weaponItem = null;
        //weaponItem.

        if ((int)instantiationData[1] == 0) { assetItem.StartGame(this); }
        //else
        //{
        //    Throw((int)instantiationData[3]);
        //}
        //if (photonView.Owner != null) Throw(photonView.OwnerActorNr);

        //Debug.Log(spriteRenderer.sprite.rect);
        //trigger.size = spriteRenderer.sprite.textureRect.size / 1000f;
        ThingView(assetItem.Icon);
    }

    //private void SetParent(int childOrder)
    //{
    //    transform.SetParent(((Transform)PhotonNetwork.LocalPlayer.TagObject).Find("Weapon Place"));
    //    //transform.SetSiblingIndex(childOrder);
    //    rigidbody2D.isKinematic = true;
    //    //rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    //    transform.localPosition = Vector2.zero;
    //}

    //public void OnPhotonInstantiate(PhotonMessageInfo info)
    //{
    //    object[] instantiationData = info.photonView.InstantiationData;
    //    Order = (int)instantiationData[0];

    //    //PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    //    photonView = GetComponent<PhotonView>();
    //    assetItem = assetsItemContainer.assetItems[Order];

    //    Debug.Log(Order);

    //    ThingView();
    //}

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    if (obj.Code == 7)
    //    {
    //        object[] datas = (object[])obj.CustomData;
    //        if ((int)datas[0] == photonView.ViewID)
    //        {
    //            gameObject.SetActive(false);
    //        }
    //    }
    //}

    //public void Throw(int playerId)
    //{
    //    rigidbody2D.isKinematic = false;
    //    rigidbody2D.constraints = RigidbodyConstraints2D.None;

    //    var weaponPoint = ((Transform)PhotonNetwork.PlayerList[playerId - LeaveRoom.current.DisabledMasterClients - 1].TagObject).GetComponentInChildren<RotateGun>().transform;
    //    rigidbody2D.position = new Vector3(weaponPoint.position.x, weaponPoint.position.y, 0f);
    //    rigidbody2D.AddForce(weaponPoint.rotation * Vector2.right * 100f);
    //}

    //private void OnEnable()
    //{
    //    Debug.Log("OnEnableLoot");
    //    transform.position += new Vector3(0f, 0f, -0.1f);
    //}

    public void ThingView(Sprite sprite)
    {
        SpriteRenderer.sprite = sprite;
        trigger.size = SpriteRenderer.sprite.bounds.size;
        gameObject.name = assetItem.Name;

        rigidbody2D.freezeRotation = true;
        //count = assetItem.MaxCollectionCount;
    }

    [PunRPC]
    public void ChangeInstantiationData(object data, int number)
    {
        PhotonView.InstantiationData[number] = data;
        if ((int)PhotonView.InstantiationData[1] <= 0) Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        var inv = collider.gameObject.GetComponentInChildren<Inventory>();
        if (inv == null && collider.gameObject.layer == toTrigger.value - 1 && rigidbody2D.constraints != RigidbodyConstraints2D.FreezeAll)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheackPosPlayer(true, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheackPosPlayer(false, collision);
    }

    private void OnMouseEnter()
    {
        if (playerNear)
        {
            StartCoroutine(WaitPressing());
            UpdateThingPanel(true);
        }
        //var objectsNear = Physics2D.OverlapCircleAll(transform.position, 0.05f);
        //for (int i = 0; i < objectsNear.Length; i++)
        //{
        //    if (objectsNear[i] == ((Transform)PhotonNetwork.LocalPlayer.TagObject).GetChild(0).GetComponent<Collider2D>())
        //    {
        //        StartCoroutine(WaitPressing());
        //        UpdateThingPanel(true);
        //        //photonView.RPC("Put", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        //        break;
        //    }
        //}
    }

    private void OnMouseExit()
    {
        StopAllCoroutines();
        UpdateThingPanel(false);
    }

    private void OnDisable()
    {
        UpdateThingPanel(false);
    }

    private void CheackPosPlayer(bool value, Collider2D collider)
    {
        if(IsPlayerCollider(collider) && value != playerNear)
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

        Put();

        UpdateThingPanel(true);
    }

    private void UpdateThingPanel(bool activeValue)
    {
        int number = (int)PhotonView.InstantiationData[1];
        if (number <= 0 || !gameObject.activeSelf) activeValue = false;
        if (activeValue) 
        { 
            //if(PhotonView.InstantiationData.Length >= 2) number = 
            ThingPanelRender.current.Render(assetItem.PanelPref, assetItem.ThingPanelData(number, PhotonView.InstantiationData)); //new object[3] { assetItem.InventoryIcon, assetItem.Name, number }
        } 
        else
        {
            ThingPanelRender.current.Stop();
        }
        //ThingPanelRender.current.Render(transform.position, activeValue, assetItem.InventoryIcon, assetItem.Name, number);
    }

    //private void OnMouseDown()
    //{
    //    //ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
    //    //rigidbody2D.OverlapCollider(contactFilter, results);
    //    //Collider2D[] b = Physics2D.OverlapCircleAll(transform.position, 1f);
    //    //Debug.Log(b[0]);
        

    //    //if (inventory != null)
    //    //{
    //    //    //object[] data = new object[3] { GetComponent<PhotonView>().ViewID, Order, count };
    //    //    //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
    //    //    //PhotonNetwork.RaiseEvent(7, data, raiseEventOptions, SendOptions.SendUnreliable);
    //    //    inventory.GetComponent<PhotonView>().RPC("AddNewItem", RpcTarget.All, Order, count);
    //    //    photonView.RPC("Put", RpcTarget.All);
    //    //}
    //}

    

    //[PunRPC]
    //private void ChangeParent(object[] data, int playerId, int childIndex)
    //{
    //    gameObject.SetActive(true);
    //    if (childIndex >= 0)
    //    {
    //        var place = ((Transform)PhotonNetwork.PlayerList[playerId - LeaveRoom.current.DisabledMasterClients - 1].TagObject).GetChild(childIndex);
    //        transform.SetParent(place);
    //        transform.localPosition = Vector2.zero;
    //        transform.localRotation = Quaternion.identity;

    //        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    //        rigidbody2D.isKinematic = true;
    //        trigger.enabled = false;
    //        //ThingView(null);
    //    }
    //    else
    //    {
    //        photonView.InstantiationData = data;
    //        transform.SetParent(default);
    //        transform.position = new Vector2(transform.position.x, transform.position.y);
    //        rigidbody2D.constraints = RigidbodyConstraints2D.None;
    //        rigidbody2D.isKinematic = false;
    //        trigger.enabled = true;
    //        //Animator.runtimeAnimatorController = null;
    //        ThingView(assetItem.Icon);
    //        Throw(playerId);
    //    }
    //}

    //[PunRPC]
    private void Put()
    {
        //Transform player = (Transform)PhotonNetwork.PlayerList[userId].TagObject;
        //player.GetComponentInChildren<Inventory>().AddNewItem(assetItem, transform, count);
        //Debug.Log(player.Find("Weapon Place"));
        assetItem.IsPutting(this);

        rigidbody2D.isKinematic = true;
    }

    //[PunRPC]
    //public void ThrowWeapon()
    //{
    //    transform.SetParent(default);
    //    transform.GetComponent<Thing>().ThingView();
    //}

    

    //private void OnTriggerExit2D(Collider2D collider)
    //{
    //    var inv = collider.GetComponent<Inventory>();
    //    if (inv != null)
    //    {
    //        inventory = null;
    //    }
    //}
}
