using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AfterWait(bool passed);
public class HandsPlayer : MonoBehaviour
{
    [SerializeField] private HandsAnimator handsAnimator;
    //[SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundsSyn soundsSyn;
    [SerializeField] private PhotonView photonView;

    [SerializeField] private RuntimeAnimatorController standartAnimator;

    [SerializeField] private int itemPriority = -1;

    //private IHandsItem handsItem;
    //private WeaponItemData item;
    //private HilkItemData jitem;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log(handsItem._animatorController().name);
    //    handsItem = (IHandsItem)item;
    //    //((IHandsItem)item)._animatorController()
    //    //((IHandsItem)jitem)._animatorController()
    //}

    private Coroutine _coroutine;
    private AfterWait _afterWait;

    //GameEvents.current.Deceleration(false);

    private void Start()
    {
        NewEffects(-1);

    }

    public void NewItem(IHandsItem handsItem, float time, AfterWait afterWait, int priority, bool deceleration)
    {
        int order = -1;
        if (CanNewAction(time, afterWait, priority, deceleration))
        {
            if (handsItem != null)
            {
                order = ((IItem)handsItem).Order;
            }
            photonView.RPC(nameof(NewEffects), RpcTarget.AllBuffered, (order));
        }
    }

    public bool CanNewAction(float time, AfterWait afterWait, int priority, bool deceleration)
    {
        GameEvents.current.Deceleration(deceleration);

        if (itemPriority >= 0 && priority > itemPriority)
        {
            if (afterWait != null) afterWait(false);
            Debug.Log("CanNewAction false");
            return false;
        }
        else
        {
            itemPriority = priority;

            if (_afterWait != null) _afterWait(false);
            if (_coroutine != null) StopCoroutine(_coroutine);

            _afterWait = afterWait;
            if (_afterWait != null) _coroutine = StartCoroutine(WaitToEnd(time));

            Debug.Log("CanNewAction true");
            return true;
        }
    }

    [PunRPC]
    private void NewEffects(int order)
    {
        Debug.Log("NewEffects");

        var animator = standartAnimator;
        AudioClip[] clips = null;
        if (order >= 0)
        {
            IItem item = InventorySimplePanel.current.assetsItemContainer.assetItems[order];
            animator = ((IHandsItem)item)._animatorController();
            clips = ((IHandsItem)item).Audio();
        }
        handsAnimator.ChangeAnimator(animator);
        soundsSyn.ChangeClips(clips);
    }

    private IEnumerator WaitToEnd(float time)
    {
        yield return new WaitForSeconds(time);
        itemPriority = -1;
        //if (_afterWait != null)
            GameEvents.current.Deceleration(false);
            _afterWait(true);
    }
}
