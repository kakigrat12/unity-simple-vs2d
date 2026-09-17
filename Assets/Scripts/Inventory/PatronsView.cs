using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatronsView : MonoBehaviour
{
    [HideInInspector] public Thing Thing;
    [SerializeField] private Text textCountInShop;

    private void Start()
    {
        GameEvents.current.onPatronsUpdate += Render;
        Render();
    }

    //private void OnBecameVisible()
    //{
    //    Render();
    //}

    private void OnDestroy()
    {
        GameEvents.current.onPatronsUpdate -= Render;
    }

    public void Render()
    {
        if (Thing == null)
        {
            textCountInShop.text = "0";
        }
        else
        {
            textCountInShop.text = Thing.PhotonView.InstantiationData[2].ToString();
        }
    }
}
