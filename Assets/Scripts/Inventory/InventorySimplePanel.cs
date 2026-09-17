using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AddThrowForce))]
public class InventorySimplePanel : MonoBehaviour
{
    public static InventorySimplePanel current;

    public AssetsItemContainer assetsItemContainer;
    private AddThrowForce addThrowForce;

    public int[] countOneTypeItems;
    public Transform container;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private SpawnCells spawnCells;
    [SerializeField] private OrderGrouper orderGrouper;
    public int MaxCellsCount = 10;
    public int CellsCount;

    public Thing LastThing;

    private PhotonView photonView;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        GameEvents.current.onKill += ThrowAllItems;
        //GameEvents.current.onThingListUpdate += Grouper;
        photonView = GetComponent<PhotonView>();
        addThrowForce = GetComponent<AddThrowForce>();
        StartCoroutine(WaitTagObject());
        countOneTypeItems = new int[assetsItemContainer.assetItems.Length];
    }

    private void OnDisable()
    {
        GameEvents.current.onKill -= ThrowAllItems;
        current = null;
    }

    private IEnumerator WaitTagObject()
    {
        while (PhotonNetwork.LocalPlayer.TagObject == null)
        {
            yield return null;
        }

        //inventory = ((Transform)PhotonNetwork.LocalPlayer.TagObject).GetComponentInChildren<Inventory>();

        spawnCells.SpawnNewCells(cellPrefab, container, MaxCellsCount);
    }

    //private void SpawnEmptyCells()
    //{
    //    DestroyAllCells(container);

    //    for (int i = 0; i < MaxCellsCount; i++)
    //    {
    //        SpawnCell(cellPrefab, container);
    //        //CellRender(emptyItem, container, 0, false);
    //    }
    //}

    public void Grouper(int order, int count)
    {
        countOneTypeItems[order] += count;
        CellsCount = orderGrouper.Grouper(null, assetsItemContainer.assetItems, countOneTypeItems, container, MaxCellsCount);
        GameEvents.current.ThingListUpdate();
    }
    //{
    //    countOneTypeItems[order] += count;

    //    CellsCount = 0;

    //    Debug.Log("grouber");

    //    for (int i = 0; i < countOneTypeItems.Length && CellsCount < MaxCellsCount; i++)
    //    {
    //        var item = assetsItemContainer.assetItems[i];
    //        CellsRender(i);
    //    }

    //    for (int i = CellsCount; i < MaxCellsCount; i++)
    //    {
    //        container.GetChild(i).GetComponent<InventoryCell>().Render(null, null, 0);
    //    }

    //    //inventory.CellsCount = conteiner.childCount;
    //    //int difference = inventory.MaxCellsCount - inventory.CellsCount;
    //    //for (int i = 0; i < difference; i++)
    //    //{
    //    //    CellRender(emptyItem, 0, false);
    //    //}
    //}

    //private void CellsRender(int order)
    //{
    //    int total = countOneTypeItems[order];
    //    Debug.Log(total);

    //    if (total > 0)
    //    {
    //        //int totalCellsNumber = total / item.MaxCollectionCount;
    //        //int residue = total % item.MaxCollectionCount;

    //        int cellsCount = total / MaxCollectionCount;
    //        int residue = total % MaxCollectionCount;

    //        for (int l = 0; l < cellsCount; l++)
    //        {
    //            CellRender(inventorySimplePanel, MaxCollectionCount);
    //        }

    //        if (residue > 0)
    //        {
    //            CellRender(inventorySimplePanel, residue);
    //        }
    //    }
    //    else
    //    {
    //        CellRender(inventorySimplePanel, 0);
    //    }
    //    //while (total > 0)
    //    //{
    //    //    int totalCellsNumber = total / item.MaxCollectionCount;
    //    //    int countInCell = total / totalCellsNumber + total % totalCellsNumber;
    //    //    InventorySimplePanel.current.CellRender(item, InventorySimplePanel.current.CellsCount, countInCell);
    //    //    total =- countInCell;
    //    //}
    //}

    //private void CellRender(InventorySimplePanel inventorySimplePanel, int count)
    //{
    //    Debug.Log(this);
    //    inventorySimplePanel.container.GetChild(inventorySimplePanel.CellsCount).GetComponent<InventoryCell>().Render(null, this, count);
    //    if (count > 0) inventorySimplePanel.CellsCount++;
    //}

    //private void DestroyAllCells(Transform container)
    //{
    //    foreach (Transform child in container)
    //        Destroy(child.gameObject);
    //}

    //public void CellRender(IItem item, int nuberCell, int count)
    //{
    //    if (count > 0) CellsCount++;
    //    Debug.Log(nuberCell);
    //    container.transform.GetChild(nuberCell).GetComponent<InventoryCell>().Render(item, count);  //inventory.GetComponent<PhotonView>(), 
    //}

    //private void SpawnCell(GameObject prefab, Transform container)
    //{
    //    Instantiate(prefab, container);
    //}


    public void AddNewItem(Thing thing)
    {
        LastThing = thing;
        int number = (int)thing.PhotonView.InstantiationData[1];

        if (CellsCount >= MaxCellsCount)
        {
            int residue = countOneTypeItems[thing.assetItem.Order] % thing.assetItem.MaxCollectionCount;
            if (residue > 0)
            {
                number = Mathf.Clamp(number, 0, thing.assetItem.MaxCollectionCount - residue);
            }
            else
            {
                number = 0;
            }
        }
        //countOneTypeItems[thing.assetItem.Order] += number;

        Grouper(thing.assetItem.Order, number);
        PutItem(thing, number);
    }

    private void PutItem(Thing thing, int number)
    {
        thing.PhotonView.RPC("ChangeInstantiationData", RpcTarget.All, (int)thing.PhotonView.InstantiationData[1] - number, 1);
    }

    public void ThrowItem(int order, int count, object[] data)
    {
        Debug.Log(addThrowForce);

        Vector3 pos = addThrowForce.WeaponPlace.position;
        Quaternion rot = addThrowForce.WeaponPlace.rotation;

        if (data != null)
        {
            pos = (Vector3)data[0];
            rot = (Quaternion)data[1];
        }
        //PhotonNetwork.LocalPlayer.UserId
        //PhotonNetwork.LocalPlayer.ActorNumber

        photonView.RPC(nameof(InstantiateItem), RpcTarget.MasterClient, order, count, pos, rot);
        //countOneTypeItems[order] -= count;
        Grouper(order, -count);
    }

    [PunRPC]
    private void InstantiateItem(int order, int count, Vector3 pos, Quaternion rot)
    {
        object[] data = new object[3] { order, count, 0};
        //Transform player = (Transform)PhotonNetwork.LocalPlayer.TagObject;
        //Transform player = (Transform)PhotonNetwork.PlayerList[playerId - LeaveRoom.current.DisabledMasterClients - 1].TagObject;
        var item = PhotonNetwork.InstantiateRoomObject("Thing", new Vector3(pos.x, pos.y, -0.1f), Quaternion.identity, 0, data); //new Vector3(player.position.x, player.position.y, 0f)

        if (item != null) photonView.RPC(nameof(ThrowSin), RpcTarget.All, item.GetComponent<PhotonView>().ViewID, rot);
        //addThrowForce.Throw(item, rot);
        //thing.GetComponent<Thing>().Throw(playerId);
    }

    [PunRPC]
    private void ThrowSin(int viewID, Quaternion rot)
    {
        var potonViewItem = PhotonNetwork.GetPhotonView(viewID);
        addThrowForce.Throw(potonViewItem.gameObject, rot);
    }


    private void ThrowAllItems(Player f, Player murder)
    {
        if(murder == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("ThrowAllItems");
            for (int i = 0; i < countOneTypeItems.Length; i++)
            {
                int count = countOneTypeItems[i];
                if (count > 0) 
                {
                    IItem item = assetsItemContainer.assetItems[i];
                    int itemCount = count / item.MaxCollectionCount;
                    int residue = count % item.MaxCollectionCount;

                    for(int l = 0; l < itemCount; l++)
                    {
                        object[] data = new object[2] { addThrowForce.WeaponPlace.position, Quaternion.AngleAxis(Random.Range(0f, 180f), Vector3.forward) };
                        ThrowItem(i, item.MaxCollectionCount, data);
                        //photonView.RPC(nameof(InstantiateItem), RpcTarget.All, i, item.MaxCollectionCount, addThrowForce.WeaponPlace.position, Quaternion.AngleAxis(Random.Range(0f, 180f), Vector3.forward));
                        //float random = Random.Range(0f, 180f);
                        //Debug.Log(random);
                        //Debug.Log(CellsCount);
                    }

                    if (residue > 0)
                    {
                        object[] data = new object[2] { addThrowForce.WeaponPlace.position, Quaternion.AngleAxis(Random.Range(0f, 180f), Vector3.forward) };
                        ThrowItem(i, item.MaxCollectionCount, data);
                    }
                }
            }
        }
        //container.parent.gameObject.SetActive(false);
        //enabled = false;
    }
}
