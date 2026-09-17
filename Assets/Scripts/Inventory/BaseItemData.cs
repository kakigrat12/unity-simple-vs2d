using System;
using UnityEngine;
public abstract class BaseItemData : ScriptableObject
{
    public Sprite Icon;
    public string Title;
    public string Description;

    public virtual void PutToInventory(Inventory inventory, int count, Func<int, ItemState> putNewItem)
    {
        for (int i = 0; i < count; i++)
        {
            var state = putNewItem(1);

            if (state == null)
            {
                return;
            }
        }
    }
}

