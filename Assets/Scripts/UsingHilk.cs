using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UsingHilk : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private HandsPlayer handsPlayer;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Animator animator;
    [SerializeField] private HilkItemData[] hilkItemData;

    private HilkItemData usedHilk;

    public UnityEvent OnUsingHilk;

    private void Start()
    {
        //GameEvents.current.onDeceleration += ChangeState;
        GameEvents.current.onThingListUpdate += IsThrown;
    }

    private void Update()
    {
        if (Input.GetKeyDown("h") && photonView.IsMine)
        {
            TryToUseHilk(0);
        }
    }

    private void OnDisable()
    {
        //GameEvents.current.onDeceleration -= ChangeState;
        GameEvents.current.onThingListUpdate -= IsThrown;
    }

    private void TryToUseHilk(int numberHilk)
    {
        HilkItemData hilk = hilkItemData[numberHilk];
        if (InventorySimplePanel.current.countOneTypeItems[hilk.Order] > 0 && health.Value < health.MaxValue && usedHilk != hilk)
        {
            Debug.Log("TryToUseHilk");
            //GameEvents.current.Deceleration(true);
            usedHilk = hilk;
            handsPlayer.NewItem(hilk, hilk.TimeToUseing, UseingHilk, 0, true);
            OnUsingHilk?.Invoke();
            //UseingHilk(hilk);
            //StartCoroutine(UseingHilk(hilk));
        }
    }

    private void IsThrown()
    {
        if (usedHilk != null && InventorySimplePanel.current.countOneTypeItems[usedHilk.Order] <= 0)
        {
            Debug.Log("IsThrown");
            GameEvents.current.ChoseWeapon();
            //handsPlayer.NewItem(null, 0f, null, 0);
        }
    }

    private void UseingHilk(bool passed)
    {
        if (passed && usedHilk != null)
        {
            int value = usedHilk.Value;
            photonView.RPC("ChangeHealth", RpcTarget.AllBuffered, -value, null, null, null);
            InventorySimplePanel.current.Grouper(usedHilk.Order, -1);

            GameEvents.current.ChoseWeapon(); //Чтоб взял оружие в конце
            
        }
        //GameEvents.current.Deceleration(false);
        usedHilk = null;
    }

    //private IEnumerator UseingHilk(HilkItemData hilk)
    //{
    //    isUseing = true;
    //    //animator.Play("Death Animation");
    //    animator.runtimeAnimatorController = hilk.AnimatorController;

    //    yield return new WaitForSeconds(hilk.TimeToUseing);

    //    InventorySimplePanel.current.Grouper(hilk.Order, -1);
    //    photonView.RPC("ChangeHealth", RpcTarget.All, -hilk.Value, null);
    //    isUseing = false;
    //}

    //private void ChangeState(float isHandsBusy)
    //{
    //    StopAllCoroutines();
    //    isUseing = false;
    //}
}
