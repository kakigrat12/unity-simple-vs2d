using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //private PatronsView patronsView;

    [SerializeField] private Text _naimeFeild;
    [SerializeField] private Image _iconFeild;
    [SerializeField] private Text _count;

    [SerializeField] private Sprite defaultIcon;

    //[HideInInspector] public PhotonView inventoryPhotonView;
    private IItem _item;
    [HideInInspector] public Thing Thing;

    //private void Awake()
    //{
    //    //GameEvents.current.onKill += Killh;
    //    patronsView = GetComponent<PatronsView>();
    //    //if (patronsView != null) patronsView.thing = Thing;
    //}

    //private void OnDestroy()
    //{
    //    GameEvents.current.onKill -= Killh;
    //}

    //private void Killh(Player g, Player murdered)
    //{

    //    Debug.Log("JJJJJJJJJJJJJJJJJJJJJJJJJJ");
    //    //if (murdered == PhotonNetwork.LocalPlayer)
    //    //{
    //        Debug.Log("gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
    //        OnPointerClick(null);
    //    //}
    //}

    public void Render(Thing thingComponent, IItem item, int count)
    {
        if (count <= 0)
        {
            _naimeFeild.enabled = false;
            if(defaultIcon == null)
            {
                _iconFeild.enabled = false;
            }
            else
            {
                _iconFeild.sprite = defaultIcon;
            }
            _count.enabled = false;

            _item = null;
            Thing = null;
        }
        else
        {
            _naimeFeild.enabled = true;
            _iconFeild.enabled = true;
            _count.enabled = true;


            //inventoryPhotonView = photonView;
            _item = item;
            Thing = thingComponent;

            _naimeFeild.text = item.Name;
            _iconFeild.sprite = item.InventoryIcon;
            //_iconFeild.SetNativeSize();

            _count.text = count.ToString();
        }

        //if (patronsView != null)
        //{
        //    patronsView.thing = Thing;
        //    patronsView.Render();
        //}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (assetItem != null) inventoryPhotonView.RPC("ThrowItemSin", RpcTarget.All, assetItem.Order, int.Parse(_count.text),PhotonNetwork.LocalPlayer.ActorNumber, transform.GetSiblingIndex());
        //inventoryPhotonView.GetComponent<Inventory>().ThrowItem(assetItem.Order, int.Parse(_count.text), transform.GetSiblingIndex());

        if (_item != null) _item.IsThrowing(Thing, eventData, int.Parse(_count.text));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ThingPanelRender.current.Render(_item.InventoryPanelPref, new object[1] { _item.Name });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ThingPanelRender.current.Stop();
    }

    //private void OnEnable()
    //{
    //    if (patronsView != null)
    //    {
    //        //patronsView.thing = Thing;
    //        patronsView.Render();
    //    }
    //}

    private void OnDisable()
    {
        ThingPanelRender.current.Stop();
    }
}
