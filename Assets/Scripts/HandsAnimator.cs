using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnimator : MonoBehaviour
{
    //[SerializeField] private PhotonView photonView;

    [SerializeField] private Animator animator;
    
    public void ChangeAnimator(RuntimeAnimatorController animatorController)
    {
        //if (animator != null) animator.runtimeAnimatorController = null;

        //animator = thingInHands.Animator;
        animator.runtimeAnimatorController = animatorController;
    }
    
    [PunRPC]
    private IEnumerator ChangeAnimation(int number)
    {
        animator.SetInteger("State", -1);
        yield return new WaitForEndOfFrame();
        animator.SetInteger("State", number);
    }
}
