using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealthBars : MonoBehaviour
{
    [SerializeField] private ViewPlayerHealth viewHealth;
    [SerializeField] private Transform container;

    public void StartSpawn()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        //while (SpawnPlayer.spawnPlayer.Teammates.Count < SpawnPlayer.spawnPlayer.roomInformation.CountPlayersInTeam)
        //{
        //    yield return null;
        //}

        while (SpawnPlayer.spawnPlayer.Teammates[SpawnPlayer.spawnPlayer.Teammates.Count - 1].CustomProperties["teamsÑolors"] == null)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(1f);

        for (int i = 0; i < SpawnPlayer.spawnPlayer.Teammates.Count; i++)
        {
            Player player = SpawnPlayer.spawnPlayer.Teammates[i];
            var viewH = Instantiate(viewHealth, container);
            viewH.Player = player;
            if (player == PhotonNetwork.LocalPlayer) viewH.transform.SetSiblingIndex(0);

            //Vector3 vector3 = (Vector3)player.CustomProperties["teamsÑolors"];
            //viewH.Background.color = new Color(vector3.x, vector3.y, vector3.z);
        }
    }
}
