using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Realtime;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    //public event Action<int, bool> onThingListUpdate;
    //public void ThingListUpdate(int order, bool isPut)
    //{
    //    if (onThingListUpdate != null)
    //    {
    //        onThingListUpdate(order, isPut);
    //    }
    //}

    public event Action onThingListUpdate;
    public void ThingListUpdate()
    {
        if (onThingListUpdate != null)
        {
            Debug.Log("ThingListUpdate");
            onThingListUpdate();
        }
    }

    public event Action<Health> onHealthChanged;
    public void HealthChanged(Health health)
    {
        if (onHealthChanged != null)
        {
            onHealthChanged(health);
        }
    }
    
    //public event Action onKill;
    //public void Kill()
    //{
    //    if (onKill != null)
    //    {
    //        onKill();
    //    }
    //}

    public event Action<Player, Player> onKill;
    public void Kill(Player killer, Player murdered)
    {
        if (onKill != null)
        {
            onKill(killer, murdered);
        }
    }

    public event Action<bool> onDeceleration;
    public void Deceleration(bool isStarted)
    {
        if (onDeceleration != null)
        {
            onDeceleration(isStarted);
        }
    }

    public event Action onPatronsUpdate;
    public void PatronsUpdate()
    {
        if (onPatronsUpdate != null)
        {
            onPatronsUpdate();
        }
    }

    public event Action onChoseWeapon;
    public void ChoseWeapon()
    {
        if (onChoseWeapon != null)
        {
            onChoseWeapon();
        }
    }

    public event Action<GameObject> onAnyPanelOpened;
    public void AnyPanelOpened(GameObject panel)
    {
        if (onAnyPanelOpened != null)
        {
            onAnyPanelOpened(panel);
        }
    }

    public event Action<GameObject> onAnyPanelClosed;
    public void AnyPanelClosed(GameObject panel)
    {
        if (onAnyPanelClosed != null)
        {
            onAnyPanelClosed(panel);
        }
    }

    public event Action<bool> onIsAnyPanelOpened;
    public void IsAnyPanelOpened(bool value)
    {
        if (onIsAnyPanelOpened != null)
        {
            onIsAnyPanelOpened(value);
        }
    }


    //public event Action onInvIsFull;
    //public void InvIsFull()
    //{
    //    if (onInvIsFull != null)
    //    {
    //        onInvIsFull();
    //    }
    //}

    //public event Action<int> onFlip;
    //public void Flip(int id)
    //{
    //    if (onFlip != null)
    //    {
    //        onFlip(id);
    //    }
    //}
}
