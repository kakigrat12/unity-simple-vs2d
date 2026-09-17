using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    private Animator animator;
    
    //public void ChangeAnimator(Thing thingInHands)
    //{
    //    if (animator != null) animator.runtimeAnimatorController = null;

    //    animator = thingInHands.Animator;
    //    animator.runtimeAnimatorController = ((WeaponItemData)thingInHands.assetItem).animatorController;
    //}
    
    //[PunRPC]
    //private IEnumerator ChangeAnimation(int number)
    //{
    //    animator.SetInteger("State", 0);
    //    yield return new WaitForEndOfFrame();
    //    animator.SetInteger("State", number);
    //}
}
