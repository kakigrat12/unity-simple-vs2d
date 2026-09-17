using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThingPanelInfo : MonoBehaviour
{
    public RectTransform RectTransform;
    [SerializeField] private Image icon;
    [SerializeField] private Image iconSecond;
    [SerializeField] private Text nameItem;
    [SerializeField] private Text number;

    [HideInInspector] public object[] Data;

    private void Start()
    {
        GameEvents.current.onThingListUpdate += ThingUpdate;
        //data = GetComponentInParent<ThingPanelRender>().Data;
        ThingUpdate();
    }

    public void ThingUpdate()
    {
        nameItem.text = (string)Data[0];
        if (icon != null) icon.sprite = (Sprite)Data[1];
        if (number != null) number.text = ((int)Data[2]).ToString();
        if (Data.Length >= 4) iconSecond.sprite = (Sprite)Data[3];
    }
}
