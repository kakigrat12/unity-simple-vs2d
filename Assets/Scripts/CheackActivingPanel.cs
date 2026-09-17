using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheackActivingPanel : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.current.AnyPanelOpened(gameObject);
    }

    private void OnDisable()
    {
        GameEvents.current.AnyPanelClosed(gameObject);
    }
}
