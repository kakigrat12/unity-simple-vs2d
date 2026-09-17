using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemContainer", menuName = "Data/ItemContainers/Container", order = 51)]
public class AssetsItemContainer : ScriptableObject
{
    public IItem[] assetItems;

    private void Awake()
    {
        for (int i = 0; i < assetItems.Length; i++)
        {
            assetItems[i].Order = i;
        }
    }
}
