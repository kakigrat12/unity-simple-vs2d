using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "newItem", menuName = "Data/Items/Simple", order = 51)]
public class AssetItem : IItem
{
    //private InventorySimplePanel inventorySimplePanel;
    public override void IsPutting(Thing thing)
    {
        InventorySimplePanel.current.AddNewItem(thing);
        //thing.photonView.RPC("ChangeInstantiationData", RpcTarget.All, (int)thing.photonView.InstantiationData[1] - number, 1);
        //if ((int)thing.photonView.InstantiationData[1] <= 0) thing.photonView.RPC("DestroyObject", RpcTarget.All);
    }
    
    public override void IsThrowing(Thing thing, PointerEventData eventData, int numberInCell) //Transform weapon, Vector2 pos, Quaternion rot
    {
        int number = 0;
        if (eventData == null || eventData.button == PointerEventData.InputButton.Left)
        {
            number = numberInCell;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            number = Mathf.Max(numberInCell / 2, 1);
        }

        if (number > 0) InventorySimplePanel.current.ThrowItem(Order, number, null);

        //object[] data = new object[2] { Order, number };
        //Transform player = (Transform)PhotonNetwork.LocalPlayer.TagObject;
        //PhotonNetwork.InstantiateRoomObject("Thing", player.position, player.rotation, 0, data);
    }

    public override void StartGame(Thing thing)
    {
        //inventorySimplePanel = InventorySimplePanel.current;
        thing.ChangeInstantiationData(MaxCollectionCount, 1);
        //Debug.Log(InventorySimplePanel.current);
    }

    public override object[] ThingPanelData(int number, object[] instantiationData)
    {
        object[] data = new object[] { Name, InventoryIcon, number };
        return data;
    }

    //public void Destruction(Thing thing)
    //{
    //    //Destroy(thing.gameObject);
    //    PhotonNetwork.Destroy(thing.PhotonView);
    //}

    //public override Sprite IconOnPlayer()
    //{
    //    return null;
    //}

    //private void Grouper(PhotonView photonView)
    //{
    //    Debug.Log(photonView);
    //    Debug.Log(photonView.InstantiationData[1]);
    //    Debug.Log((int)photonView.InstantiationData[1]);

    //    InventorySimplePanel.current.countOneTypeItems[Order] += (int)photonView.InstantiationData[1];
    //    InventorySimplePanel.current.CellsCount = 0;

    //    for (int i = 0; i < InventorySimplePanel.current.countOneTypeItems.Length || InventorySimplePanel.current.CellsCount < InventorySimplePanel.current.MaxCellsCount; i++)
    //    {
    //        int total = InventorySimplePanel.current.countOneTypeItems[Order];
    //        if (total > 0)
    //        {
    //            var item = InventorySimplePanel.current.assetsItemContainer.assetItems[i];
    //            item.CellsRender(InventorySimplePanel.current);
    //            Debug.Log(i);
    //        }
    //        else
    //        {
    //            CellRender(InventorySimplePanel.current, 0);
    //        }
    //    }

    //    //inventory.CellsCount = conteiner.childCount;
    //    //int difference = inventory.MaxCellsCount - inventory.CellsCount;
    //    //for (int i = 0; i < difference; i++)
    //    //{
    //    //    CellRender(emptyItem, 0, false);
    //    //}
    //}

    //public override void CellsRender(InventorySimplePanel inventorySimplePanel)
    //{
    //    int total = inventorySimplePanel.countOneTypeItems[Order];
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

    //[SerializeField] private InventoryCell _inventoryCellTemplate;
    //[SerializeField] private string _name;
    //[SerializeField] private Sprite _icon;
    //[SerializeField] private int _maxCollectionCount;

    //[SerializeField] private int _collectionCount;

    //public abstract void SetParent(Transform child, Transform parent);
}
