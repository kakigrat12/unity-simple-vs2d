using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillsView : MonoBehaviourPunCallbacks
{
    private Player _killer;
    private Player _murdered;

    [SerializeField] private PhotonTeamsManager photonTeamsManager;
    [SerializeField] private PhotonView pv;

    [SerializeField] private GameObject killsPanel;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject ourKillsPanel;

    [Space]

    [SerializeField] private KillMassegePaths killMessagePrefab;
    [SerializeField] private KillMassegePaths myKillPrefab;
    [SerializeField] private KillMassegePaths myTeammateKillMessagePrefab;
    [SerializeField] private KillMassegePaths myTeammateDeathMessagePrefab;

    [Space]

    [SerializeField] private Text playersCountText;
    private int playersCount;
    [SerializeField] private Text teamsCountText;
    [SerializeField] private Text myKillsText;
    private int myKillsCount;
    private Player myKiller;

    private static List<Player> deadPlayers = new List<Player>();

    //private PhotonView pv;

    //[Space]

    //[SerializeField] private GameObject targetCamera;

    private void Start()
    {
        GameEvents.current.onKill += PlayerKill;

        playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
        playersCountText.text = playersCount.ToString();
        StartCoroutine(waitSpawnPlayer());

        //pv = GetComponent<PhotonView>
    }

    private void OnDisable()
    {
        GameEvents.current.onKill -= PlayerKill;
        deadPlayers.Clear();
    }

    private IEnumerator waitSpawnPlayer()
    {
        while (SpawnPlayer.spawnPlayer.TeamsCount == 0)
        {
            yield return null;
        }

        teamsCountText.text = SpawnPlayer.spawnPlayer.TeamsCount.ToString();
    }


    private void PlayerKill(Player killer, Player murdered)
    {
        if (IsAlive(murdered))
        {
            Debug.Log("PlayerKill");
            _killer = killer;
            _murdered = murdered;

            SpawnKill(killMessagePrefab, killsPanel.transform);

            OnPlayerLeft(murdered);

            PlayerHandlerState(killer, murdered);
            //PlayerHandlerState(killer, true);
            //PlayerHandlerState(murdered, false);
        }
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (this == null) return;
        Debug.Log(otherPlayer);
        Debug.Log(deadPlayers.Count);
        if (IsAlive(otherPlayer))
        {
            Debug.Log("OnPlayerLeftRoom");
            OnPlayerLeft(otherPlayer);
        }
    }

    public static bool IsAlive(Player player)
    {
        int index = deadPlayers.FindIndex(x => x == player);
        if (index == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnPlayerLeft(Player playerLeft)
    {
        playersCount--;
        if (playersCountText != null && !enabled) playersCountText.text = playersCount.ToString();
        deadPlayers.Add(playerLeft);

        //Debug.Log(photonTeamsManager.TryGetTeamMembers(playerLeft.GetPhotonTeam(), out Player[] teamMates));
        //Debug.Log(playerLeft.TryGetTeamMates(out Player[] teamMates));
        //Debug.Log(teamMates.Length);

        if (!photonTeamsManager.TryGetTeamMembers(playerLeft.GetPhotonTeam(), out Player[] teamMates) || teamMates.Length == 0)
        {
            OnTeamLeft();
        }

        //if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["isDead"] && playerLeft == SpawnPlayer.spawnPlayer.Teammates[0])
        //{
        //    Invoke(nameof(ChangeCameraTarget), 3f);
        //}

        //teamsCountText.text = SpawnPlayer.spawnPlayer.TeamsCount.ToString();
    }


    private void PlayerHandlerState(Player killer, Player murdered)
    {
        if (murdered.GetPhotonTeam() == PhotonNetwork.LocalPlayer.GetPhotonTeam())
        {
            SpawnPlayer.spawnPlayer.Teammates.Remove(murdered);
            //if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["isDead"] && player == SpawnPlayer.spawnPlayer.Teammates[0])
            //{
            //    Invoke(nameof(ChangeCameraTarget), 3f);
            //}

            if (murdered == PhotonNetwork.LocalPlayer)
            {
                myKiller = killer;
                deathPanel.SetActive(true);

                if (SpawnPlayer.spawnPlayer.Teammates.Count == 0)
                {
                    Debug.Log("Teammates.Count == 0");
                    pv.RPC(nameof(OnTeamLeft), RpcTarget.All);
                }

                //PhotonNetwork.LocalPlayer.CustomProperties["isDead"] = true;
                //Invoke(nameof(ChangeCameraTarget), 3f);
                //ChangeCameraTarget();
            }
            else
            {
                SpawnKill(myTeammateDeathMessagePrefab, ourKillsPanel.transform);
            }
        }
        else if (killer.GetPhotonTeam() == PhotonNetwork.LocalPlayer.GetPhotonTeam())
        {
            if (killer == PhotonNetwork.LocalPlayer)
            {
                myKillsCount++;
                myKillsText.text = myKillsCount.ToString();
                
                SpawnKill(myKillPrefab, ourKillsPanel.transform);
            }
            else
            {
                SpawnKill(myTeammateKillMessagePrefab, ourKillsPanel.transform);
            }
        }
    }

    [PunRPC]
    private void OnTeamLeft() //Player killer
    {
        SpawnPlayer.spawnPlayer.TeamsCount--;
        Debug.Log("OnTeamLeft");
        Debug.Log(SpawnPlayer.spawnPlayer.TeamsCount);
        teamsCountText.text = SpawnPlayer.spawnPlayer.TeamsCount.ToString();

        if (SpawnPlayer.spawnPlayer.TeamsCount == 1 && myKiller == null) //!= PhotonNetwork.LocalPlayer.GetPhotonTeam()
        {
            photonView.RPC(nameof(EndOfGame), RpcTarget.All);
        }
    }

    [PunRPC]
    private void EndOfGame(PhotonMessageInfo info)
    {
        if (info.Sender.GetPhotonTeam() == PhotonNetwork.LocalPlayer.GetPhotonTeam())
        {
            Debug.Log("Я победил!!!)");
            winPanel.SetActive(true);
        }
    }


    private void SpawnKill(KillMassegePaths kill, Transform parent)
    {
        var killMessage = Instantiate(kill, parent);
        killMessage.Render(_killer.NickName, _murdered.NickName);
        //killMessage.transform.GetChild(0).GetComponent<Text>().text = _killer.NickName;
        //killMessage.transform.GetChild(2).GetComponent<Text>().text = _murdered.NickName;
        Destroy(killMessage.gameObject, 3.55f);
    }

    //private void ChangeCameraTarget()
    //{
    //    Debug.Log("ChangeCameraTarget");
    //    var teammates = SpawnPlayer.spawnPlayer.Teammates;
    //    if (teammates.Count > 0) 
    //    {
    //        SpawnPlayer.spawnPlayer.targetCamera.SetParent((Transform)teammates[0].TagObject);
    //    }
    //}
}
