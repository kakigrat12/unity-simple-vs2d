using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheackAllActivingPanels : MonoBehaviour
{
    private List<GameObject> panels = new List<GameObject>();
    private bool wasAllClosed;

    private void Start()
    {
        GameEvents.current.onAnyPanelOpened += AddPanel;
        GameEvents.current.onAnyPanelClosed += RemovePanel;
    }

    private void OnDisable()
    {
        GameEvents.current.onAnyPanelOpened -= AddPanel;
        GameEvents.current.onAnyPanelClosed -= RemovePanel;
    }

    private void AddPanel(GameObject panel)
    {
        panels.Add(panel);
        ChangeState();
    }

    private void RemovePanel(GameObject panel)
    {
        panels.Remove(panel);
        ChangeState();
    }

    private void ChangeState()
    {
        if (panels.Count == 0)
        {
            wasAllClosed = true;
        }
        else if (wasAllClosed)
        {
            wasAllClosed = false;
        }
        GameEvents.current.IsAnyPanelOpened(!wasAllClosed);
    }
}
