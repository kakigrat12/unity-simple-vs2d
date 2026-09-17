using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPanel : MonoBehaviour
{
    [SerializeField] private Transform containerInInventory;
    [SerializeField] private Transform containerOnScreen;
    [SerializeField] private WeaponCellPaths cellPrefabInInv;
    [SerializeField] private WeaponCellPaths cellPrefabOnScr;
    [SerializeField] private OrderGrouper orderGrouper;
    [SerializeField] private SpawnCells spawnCells;

    [SerializeField] private WeaponCellPaths[] weaponCellPaths;

    private void Start()
    {
        int maxCount = Weapons.current.MaxCount;
        weaponCellPaths = new WeaponCellPaths[maxCount];
        for (int i = 0; i < maxCount; i++)
        {
            weaponCellPaths[i] = Instantiate(cellPrefabInInv, containerInInventory);
            weaponCellPaths[i].WeaponBind.text = (i + 1).ToString();
        }

        //spawnCells.SpawnNewCells(cellPrefabInInv, containerInInventory, Weapons.current.MaxCount);
        GameEvents.current.onChoseWeapon += Render;
    }

    private void OnDisable()
    {
        GameEvents.current.onChoseWeapon -= Render;
    }

    //private void UpdateWeapons(float value)
    //{
    //    photonView.RPC("ThingHandler", RpcTarget.All, value);
    //    photonView.RPC("ChangeAnimation", RpcTarget.All, 0);
    //    Invoke("RenderCells", 0.0001f); //Без задержки нормально не отрисовывается
    //    //RenderCells();
    //}

    private void Render()
    {
        int selected = Weapons.current.Selected;
        Thing[] things = Weapons.current.Things.ToArray();
        Thing[] thingsInOrder = new Thing[things.Length];
        string[] binds = new string[things.Length];

        for (int i = things.Length - 1; i >= 0; i--)
        {
            if (selected >= things.Length)
            {
                selected = 0;
            }
            thingsInOrder[i] = things[selected];
            binds[i] = (selected + 1).ToString();
            selected++;
        }

        for (int i = 0; i < Weapons.current.MaxCount; i++)
        {
            var cell = weaponCellPaths[i];
            if (i < things.Length)
            {
                cell.InventoryCell.Render(things[i], things[i].assetItem, 1);
                cell.PatronsView.Thing = things[i];
            }
            else
            {
                cell.InventoryCell.Render(null, null, 0);
                cell.PatronsView.Thing = null;
            }
        }

        foreach (Transform child in containerOnScreen)
            Destroy(child.gameObject);

        for (int i = 0; i < thingsInOrder.Length; i++)
        {
            var thing = thingsInOrder[i];
            var weapon = (WeaponItemData)thing.assetItem;
            var cell = Instantiate(cellPrefabOnScr, containerOnScreen);

            cell.WeaponCellView.Render(weapon.IconOnScreen, weapon.IconBackground);
            cell.PatronsView.Thing = thing;
            cell.ThingNumberView.Item = ((WeaponItemData)thing.assetItem).Patrons;
            cell.WeaponBind.text = binds[i];
        }
    }

    //private IEnumerator RenderCells()
    //{
    //    int selected = Weapons.current.Selected;
    //    Thing[] things = Weapons.current.Things.ToArray();
    //    Thing[] thingsInOrder = new Thing[things.Length];

    //    IItem[] items = new IItem[things.Length];
    //    IItem[] itemsInOrder = new IItem[things.Length];

    //    if (containerOnScreen.childCount < things.Length)
    //    {
    //        Instantiate(cellPrefabOnScr, containerOnScreen);
    //    }
    //    else if(containerOnScreen.childCount > things.Length)
    //    {
    //        if(things.Length > 0)
    //        {
    //            Destroy(containerOnScreen.GetChild(0).gameObject);
    //        }
    //        else
    //        {
    //            foreach (Transform child in containerOnScreen)
    //            {
    //                Destroy(child.gameObject);
    //            }
    //        }
    //    }

    //    for (int i = things.Length - 1; i >= 0; i--)
    //    {
    //        if(selected >= things.Length)
    //        {
    //            selected = 0;
    //        }
    //        //Debug.Log(weaponPlace.GetChild(i).GetComponent<Thing>());
    //        //thingsInOrder[i] = weaponPlace.GetChild(i).GetComponent<Thing>();
    //        //int number = things.Length - 1 - i; // Чтоб на нулевом месте было выбранное оружие
    //        thingsInOrder[i] = things[selected];
    //        itemsInOrder[i] = thingsInOrder[i].assetItem;

    //        items[i] = things[i].assetItem;
    //        selected++;
    //        //containerInInventory.GetChild(i).GetComponent<InventoryCell>().Render(thing, thing.assetItem, 1);
    //        //containerOnScreen.GetChild(i).GetComponent<InventoryCell>().Render(thing, thing.assetItem, 1);
    //    }

    //    //spawnCells.SpawnNewCells(cellPrefab, containerOnScreen, things.Length);

    //    yield return new WaitForEndOfFrame();

    //    orderGrouper.Grouper(things, items, null, containerInInventory, Weapons.current.MaxCount);
    //    orderGrouper.Grouper(thingsInOrder, itemsInOrder, null, containerOnScreen, things.Length); //тупо рендериться. Приходится использовать столько массивов. По другому пока не получиться
    //}
}
