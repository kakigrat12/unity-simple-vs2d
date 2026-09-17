using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class IItem: ScriptableObject
{
    public int Order;
    public float Population;
    //[HideInInspector] public int Container = 0;

    //public InventoryCell InventoryCellTemplate;// => _inventoryCellTemplate;
    public string Name;// => _name;
    public Sprite Icon;// => _icon;
    public Sprite InventoryIcon;
    public ThingPanelInfo PanelPref;
    public ThingPanelInfo InventoryPanelPref;
    public int MaxCollectionCount;// => _maxCollectionCount;
    //public abstract Sprite IconOnPlayer();
    public abstract void StartGame(Thing thing);
    //public abstract void CellsRender(InventorySimplePanel inventorySimplePanel);
    public abstract void IsPutting(Thing thing);
    public abstract void IsThrowing(Thing thing, PointerEventData eventData, int numberInCell); //Transform weapon, Vector2 pos, Quaternion rot

    public abstract object[] ThingPanelData(int number, object[] instantiationData);

    private void Awake()
    {
        if (InventoryIcon == null) InventoryIcon = Icon;
    }

    //int Order { set; get; }

    //InventoryCell InventoryCellTemplate { get; }
    ////float Population { set; get; }
    //string Name { get; }
    //Sprite Icon { get; }
    //int MaxCollectionCount { get; }

    ////Weapon Settings
    ////int Shop { get; }
    //float TimeShoot { get; }
    //string NamesCartridges { get; }
    //float TimeRecharge { get; }
    ////int CollectionCount { get; }
}
