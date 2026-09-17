using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //[SerializeField] private Transform weaponPlace;
    //[SerializeField] private GameObject[] weapons;
    //private int numberWeapon;
    //private int hilks;
    //private List<Transform> things = new List<Transform>();
    //[SerializeField] Dropdown dropdown;

    //[SerializeField] private PlayerWeapons playerWeapons;
    //[SerializeField] private Health health;
    //[SerializeField] private Collider2D collider2D;

    ////private void Start()
    ////{
    ////    if (!GetComponentInParent<PhotonView>().IsMine) collider2D.enabled = false;
    ////}
    //public void ThingsHandler(GameObject newThing)
    //{
    //    switch (newThing.tag)
    //    {
    //        case "Weapon":
    //            Debug.Log("Weapon");
    //            playerWeapons.NewWeapon(newThing);
    //            break;

    //        case "Cartridge":
    //            Debug.Log("Cartridge");
    //            playerWeapons.NewCatridge(newThing);
    //            break;

    //        case "Hilk":
    //            Debug.Log("Hilk");
    //            health.NewHilk(newThing);
    //            break;
    //    }
    //}

    public AssetsItemContainer assetsItemContainer;

    //[SerializeField] private int maxCountItems;
    public int MaxCellsCount = 10;
    public int CellsCount;

    public int[] countOneTypeItems;

    [Space]

    [SerializeField] private Collider2D trigger;
    [SerializeField] private Transform weaponPlace;
    private PhotonView photonView;

    private void Start()
    {
        //PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;

        photonView = GetComponent<PhotonView>();
        countOneTypeItems = new int[assetsItemContainer.assetItems.Length];

        if (!photonView.IsMine) trigger.enabled = false;
        Debug.Log(photonView.IsMine);
    }

    

    //public void OnEnable()
    //{
    //    //Render(new List<AssetItem>(items));
    //    //Render(SorteredItems(items));
    //    Grouper();
    //}
    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    if (obj.Code == 7)
    //    {
    //        object[] datas = (object[])obj.CustomData;
    //        AddNewItem((int)datas[1], (int)datas[2]);
    //    }
    //}

    //[PunRPC]
    public void AddNewItem(IItem item, Transform itemTransform, int count)
    {
        if (CellsCount < MaxCellsCount) // || item.Container == 1
        {
            int order = item.Order;
            Debug.Log(order);
            countOneTypeItems[order] += count;
            //item.IsPutting(itemTransform, weaponPlace);
            //if (photonView.IsMine)
                //GameEvents.current.ThingListUpdate(order, true);
        }
        else
        {
            //GameEvents.current.InvIsFull();
        }
    }
    
    [PunRPC]
    private void ThrowItemSin(int order, int count, int playerId, int siblingIndex)
    {
        Transform weapon = null;
        var WeaponPlace = ((Transform)PhotonNetwork.PlayerList[playerId - 1].TagObject).Find("Weapon Place");
        if (siblingIndex < WeaponPlace.childCount)
        {
            weapon = WeaponPlace.GetChild(siblingIndex);
        }
        ThrowItem(order, count, weapon);
    }

    public void ThrowItem(int order, int count, Transform weapon)
    {
        RemoveItem(order, count);
        //photonView.RPC("RemoveItem", RpcTarget.All, order, count);
        //assetsItemContainer.assetItems[order].IsThrowing(weapon, transform.position - new Vector3(0f, 0f, 1f), Quaternion.identity);
        //CreateThing(order, transform.position - new Vector3(0f, 0f, 1f), Quaternion.identity);
    }

    [PunRPC]
    private void RemoveItem(int order, int count)
    {
        countOneTypeItems[order] -= count;
        //if (photonView.IsMine)
            //GameEvents.current.ThingListUpdate(order, false);
    }

    //private void CreateThing(int order, Vector3 pos, Quaternion rot)
    //{
    //    object[] data = new object[1] { order };
    //    PhotonNetwork.InstantiateRoomObject("Thing", pos, rot, 0, data);
    //}

    //private List<AssetItem> SorteredItems(List<AssetItem> assetItems)
    //{
    //    List<AssetItem>[] assetItems2D = new List<AssetItem>[maxCountItems];

    //    assetItems.ForEach(item => 
    //    {
    //        if (assetItems2D[item.Order] == null)
    //            assetItems2D[item.Order] = new List<AssetItem>();

    //        assetItems2D[item.Order].Add(item);
    //    });

    //    assetItems.Clear();

    //    foreach (var itemsList in assetItems2D)
    //    {
    //        if(itemsList != null)
    //            assetItems.AddRange(itemsList);
    //    }

    //    //for (int i = 0; i < maxCountItems; i++)
    //    //{
    //    //    var item = items[i];
    //    //    if(item == null)
    //    //    {
    //    //        items.Add(deafultItem);
    //    //    }
    //    //    else
    //    //    {

    //    //    }
    //    //}
    //    return assetItems;
    //}

    

    //public const int Width = 10;
    //public const int Height = 10;
    //public ItemState[] Items;

    //public void AddItem(BaseItemData item, int count)
    //{
    //    item.PutToInventory(this, count, (countInCell) => PutNewItem(item, countInCell));
    //}

    //private ItemState PutNewItem(BaseItemData item, int count)
    //{
    //    var state = FindEmptyState();
    //    if (state == null)
    //    {
    //        Debug.Log("Inventory is full");
    //        return null;
    //    }

    //    state.Data = item;
    //    state.Count = count;

    //    return state;
    //}

    //// Например нашёл, не только 0!
    //private ItemState FindEmptyState()
    //{
    //    return Items[0];
    //}
}
