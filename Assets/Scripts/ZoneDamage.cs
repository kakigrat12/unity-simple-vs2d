using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDamage : MonoBehaviour
{
    private int damage;
    private PhotonView playerPhotonView;
    private Coroutine coroutine;

    private void Start()
    {
        StartCoroutine(PlayerTagObjectWaiter());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ObjectCheck(collision)) coroutine = StartCoroutine(Damaging());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ObjectCheck(collision)) StopCoroutine(coroutine);
    }

    private bool ObjectCheck(Collider2D collision)
    {
        PhotonView photonViewAther = collision.GetComponent<PhotonView>();
        if (photonViewAther == playerPhotonView)
        {
            return true;
        }
        return false;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private IEnumerator PlayerTagObjectWaiter()
    {
        while ((Transform)PhotonNetwork.LocalPlayer.TagObject == null)
        {
            yield return null;
        }
        playerPhotonView = ((Transform)PhotonNetwork.LocalPlayer.TagObject).GetComponent<PhotonView>();
    }

    private IEnumerator Damaging()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            playerPhotonView.RPC("ChangeHealth", RpcTarget.AllBuffered, damage, null, null, playerPhotonView.Owner);
        }
    }
}
