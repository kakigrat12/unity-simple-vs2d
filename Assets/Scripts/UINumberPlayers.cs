using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINumberPlayers : MonoBehaviour
{
    [SerializeField] private Text PlayersOnScece;
    [SerializeField] private WaiterLoadingScenes waiterLoadingScenes;

    private void Awake()
    {
        waiterLoadingScenes.OnNewPlayer += UpdatePlayers;
    }

    private void OnDisable()
    {
        waiterLoadingScenes.OnNewPlayer -= UpdatePlayers;
    }

    private void UpdatePlayers()
    {
        PlayersOnScece.text = waiterLoadingScenes.playersOnScene.ToString();
    }
}
