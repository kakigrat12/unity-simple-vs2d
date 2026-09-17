using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderGrouper
{
    private int cellsCount;
    private Thing[] things;

    public int Grouper(Thing[] thingsWeapons, IItem[] items, int[] countOneTypeItems, Transform container, int maxCellsCount)
    {
        //countOneTypeItems[order] += count;
        things = thingsWeapons;
        cellsCount = 0;

        //Debug.Log("grouber");

        for (int i = 0; i < items.Length && cellsCount < maxCellsCount; i++)
        {
            int number = 1;
            if (countOneTypeItems != null) number = countOneTypeItems[i];
            CellsRender(items[i], number, container);
        }

        for (int i = cellsCount; i < maxCellsCount; i++)
        {
            container.GetChild(i).GetComponent<InventoryCell>().Render(null, null, 0);
        }

        return cellsCount;

        //inventory.CellsCount = conteiner.childCount;
        //int difference = inventory.MaxCellsCount - inventory.CellsCount;
        //for (int i = 0; i < difference; i++)
        //{
        //    CellRender(emptyItem, 0, false);
        //}
    }

    private void CellsRender(IItem item, int total, Transform container)
    {
        //int total = inventorySimplePanel.countOneTypeItems[item.Order];
        Debug.Log(total);

        if (total > 0)
        {
            //int totalCellsNumber = total / item.MaxCollectionCount;
            //int residue = total % item.MaxCollectionCount;

            Debug.Log(item.MaxCollectionCount);
            int cellsCount = total / item.MaxCollectionCount;
            int residue = total % item.MaxCollectionCount;

            for (int l = 0; l < cellsCount; l++)
            {
                CellRender(item, item.MaxCollectionCount, container);
            }

            if (residue > 0)
            {
                CellRender(item, residue, container);
            }
        }
        else
        {
            CellRender(item, 0, container);
        }
        //while (total > 0)
        //{
        //    int totalCellsNumber = total / item.MaxCollectionCount;
        //    int countInCell = total / totalCellsNumber + total % totalCellsNumber;
        //    InventorySimplePanel.current.CellRender(item, InventorySimplePanel.current.CellsCount, countInCell);
        //    total =- countInCell;
        //}
    }

    private void CellRender(IItem item, int count, Transform container)
    {
        Debug.Log(this);
        Thing thing = null;
        if (things != null) thing = things[cellsCount];
        container.GetChild(cellsCount).GetComponent<InventoryCell>().Render(thing, item, count);
        if (count > 0) cellsCount++;
    }
}
