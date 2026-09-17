using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThingNumberView : MonoBehaviour
{
    public IItem Item;
    [SerializeField] private Text number;

    private void Start()
    {
        GameEvents.current.onThingListUpdate += Render;
        Render();
    }

    private void OnDestroy()
    {
        GameEvents.current.onThingListUpdate -= Render;
    }

    private void Render()
    {
        number.text = InventorySimplePanel.current.countOneTypeItems[Item.Order].ToString();
    }
}
