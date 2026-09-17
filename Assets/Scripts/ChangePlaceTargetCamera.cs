using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlaceTargetCamera : MonoBehaviour
{
    [SerializeField] private Transform TargetCamera;
    [SerializeField] private KeyCode keyChange;
    private int selectedPlayer = 0;
    private Player _selectedPlayer;

    private void Start()
    {
        GameEvents.current.onKill += Kill;
        _selectedPlayer = PhotonNetwork.LocalPlayer;
        enabled = false;
    }

    private void OnDestroy()
    {
        GameEvents.current.onKill -= Kill;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyChange)) ChangePlayer();
    }

    private void Kill(Player killer, Player murdered)
    {
        if (murdered == _selectedPlayer) 
        { 
            ChangePlayer();
            enabled = true;
        }
    }

    private void ChangePlayer()
    {
        Debug.Log("ChangeCameraTarget");
        var teammates = SpawnPlayer.spawnPlayer.Teammates;
        
        if (teammates.Count > 0)
        {
            Transform player = (Transform)_selectedPlayer.TagObject;
            ChangeStateLight(player, false);

            selectedPlayer++;
            if (selectedPlayer >= teammates.Count)
            {
                selectedPlayer = 0;
            }

            _selectedPlayer = teammates[selectedPlayer];
            player = (Transform)_selectedPlayer.TagObject;
            ChangeStateLight(player, true);
            ChangeTarget(player);
        }
    }

    private void ChangeStateLight(Transform player, bool value)
    {
        var light = player.GetChild(5);
        light.gameObject.SetActive(value);
    }

    private void ChangeTarget(Transform target)
    {
        TargetCamera.SetParent(target);
        TargetCamera.localPosition = Vector3.zero;
    }
}
