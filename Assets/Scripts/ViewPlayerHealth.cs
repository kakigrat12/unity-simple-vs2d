using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPlayerHealth : MonoBehaviour
{
    public Player Player;
    [SerializeField] private Scrollbar healthBar;
    [SerializeField] private Text niñkName;
    [SerializeField] private Image Background;

    private void Start()
    {
        GameEvents.current.onHealthChanged += OnView;

        niñkName.text = Player.NickName;
        Vector3 vector3 = (Vector3)Player.CustomProperties["teamsÑolors"];
        Background.color = new Color(vector3.x, vector3.y, vector3.z);
    }

    private void OnDisable()
    {
        GameEvents.current.onHealthChanged -= OnView;
    }

    private void OnView(Health health)
    {
        if (health.transform == (Transform)Player.TagObject) 
        { 
            healthBar.size = (float)health.Value / health.MaxValue;
        }
    }
}
