using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Material standart;
    [SerializeField] private Material excretion;

    private bool isExcretion = false;

    private void Start()
    {
        Change();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision == ((Transform)PhotonNetwork.LocalPlayer.TagObject).GetChild(0).GetComponent<Collider2D>())
    //    {
    //        Debug.Log("OnTriggerEnterThing");
    //        spriteRenderer.material = excretion;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision == ((Transform)PhotonNetwork.LocalPlayer.TagObject).GetChild(0).GetComponent<Collider2D>())
    //    {
    //        Debug.Log("OnTriggerExitThing");
    //        spriteRenderer.material = standart;
    //    }
    //}

    public void ChangeToStandart()
    {
        spriteRenderer.material = standart;
    }

    public void Change()
    {
        if (!enabled) return;

        if (isExcretion)
        {
            spriteRenderer.material = excretion;
        }
        else
        {
            spriteRenderer.material = standart;
        }

        isExcretion = !isExcretion;
    }
}
