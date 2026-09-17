using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour//, IPunObservable
{
    public static SpawnPlayer spawnPlayer;

    //public RoomInformation roomInformation;
    [SerializeField] private Transform[] teams;
    [SerializeField] private Color[] teams—olors;
    [SerializeField] private Material outline;

    [SerializeField] private GameObject PlayerPrefab;
    public Transform targetCamera;

    //[SerializeField] private int teammatesLayer = 15;
    public int TeamsCount;

    public List<Player> Teammates = new List<Player>();
    public UnityEvent EndSpawn;
    //private int numberInTeam = 0;

    private int[] spawnPointsNumbers;

    private void Awake()
    {
        spawnPlayer = this;
        //var s = SceneManager.CreateScene("Players");
        //SceneManager.LoadSceneAsync(s.buildIndex, LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        spawnPlayer = null;
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Player[] players = PhotonNetwork.PlayerList;
            spawnPointsNumbers = new int[PhotonNetwork.PlayerList.Length];

            var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            TeamsCount = playerCount / (int)PhotonNetwork.CurrentRoom.CustomProperties["pIT"];

            //PhotonNetwork.PlayerList[0].JoinTeam(0);
            //PhotonNetwork.PlayerList[1].JoinTeam(0);

            //var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            //PhotonNetwork.CurrentRoom.CustomProperties["l"] = playerCount / roomInformation.CountPlayersInTeam;
            int numberPlayer = 0;

            for (int i = 0; i < TeamsCount; i++)
            {
                for (int l = 0; l < (int)PhotonNetwork.CurrentRoom.CustomProperties["pIT"]; l++)
                {
                    int randomNumber = Random.Range(0, playerCount - numberPlayer);
                    players[randomNumber].JoinTeam((byte)i);
                    spawnPointsNumbers[randomNumber] = l;
                    Debug.Log(spawnPointsNumbers[randomNumber]);
                    players[randomNumber] = players[playerCount - numberPlayer - 1];
                    //PhotonNetwork.PlayerList[randomNumber].JoinTeam((byte)i);
                    //PhotonNetwork.PlayerList[randomNumber] = PhotonNetwork.PlayerList[playerCount - numberPlayer - 1];
                    numberPlayer++;
                }
            }

            GetComponent<PhotonView>().RPC("Sinhronisation", RpcTarget.AllBuffered, spawnPointsNumbers, TeamsCount); //, roomInformation.CountPlayersInTeam
        }
    }

    //public void OnPhotonSerializeView(PhotonStream steram, PhotonMessageInfo info)
    //{
    //    if (steram.IsWriting)
    //    {
    //        steram.SendNext(TeamsCount);
    //    }
    //    else
    //    {
    //        TeamsCount = (int)steram.ReceiveNext();
    //    }
    //}

    [PunRPC]
    void Sinhronisation(int[] numbers, int teamsCount) //int countPlayersInTeam, 
    {
        spawnPointsNumbers = numbers;
        //roomInformation.CountPlayersInTeam = countPlayersInTeam;
        TeamsCount = teamsCount;
        //Debug.Log(roomInformation.CountPlayersInTeam);
        Debug.Log("Sinhronisation");
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        Debug.Log("waitSpawn");
        while(PhotonNetwork.LocalPlayer.GetPhotonTeam() == null)
        {
            yield return null;
        }
        Debug.Log("startSpawn");

        int numberPlayer = PhotonNetwork.LocalPlayer.ActorNumber;
        int numberTeam = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code;
        int numberInTeam = 0; //spawnPointsNumbers[numberPlayer - 1];

        Debug.Log(numberPlayer);
        Debug.Log(numberTeam);
        Debug.Log(numberInTeam);

        Vector3 spawnPoint = teams[numberTeam].GetChild(numberInTeam).position;
        PlayerPrefab = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPoint, Quaternion.identity);

        //var delta = targetCamera.transform.position;
        //PlayerPrefab.GetComponent<Death>().TargetCamera = targetCamera;
        targetCamera.SetParent(PlayerPrefab.transform);
        //targetCamera.transform.localPosition = delta;
        targetCamera.transform.localPosition = Vector3.zero;

        //PhotonView photonView = GetComponent<PhotonView>();
        //photonView.RPC("FindeMyTeammates", RpcTarget.All);

        FindeMyTeammates();
    }

    //[PunRPC]
    private void FindeMyTeammates()
    {
        Debug.Log("FindeMyTeammates");
        //Teammates = new Transform[roomInformation.CountPlayersInTeam];
        int i = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.GetPhotonTeam() == PhotonNetwork.LocalPlayer.GetPhotonTeam())
            {
                //StartCoroutine(AddTeammate(player));
                //AddTeammate(player);
                Teammates.Add(player);

                Color color = teams—olors[i];
                Vector3 vector3 = new Vector3(color.r, color.g, color.b);
                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "teams—olors", vector3 } });

                StartCoroutine(SpawnLabel(player, color));

                i++;
            }
        }

        EndSpawn?.Invoke();
    }

    private IEnumerator SpawnLabel(Player player, Color color)
    {
        Debug.Log("SpawnLabel");
        while ((Transform)player.TagObject == null) // || Teammates.Count < roomInformation.CountPlayersInTeam || Teammates[Teammates.Count - 1].CustomProperties["teams—olors"] == null
        {
            yield return null;
        }
        Transform playerTransform = (Transform)player.TagObject;

        Debug.Log(playerTransform);
        //playerTransform.GetComponentInChildren<HitBoxes>().gameObject.SetActive(false);

        Material newMaterial = Instantiate(outline);
        newMaterial.SetColor("_MainColor", color);
        foreach (var component in playerTransform.GetComponentsInChildren<SpriteRenderer>())
        {
            component.material = newMaterial;
        }
        //playerTransform.GetComponent<SpriteRenderer>().material = newMaterial;

        var lable = playerTransform.GetComponent<Target>();
        lable.TargetColor = color;
        lable.enabled = true;
    }
    //private void AddTeammate(Player player)
    //{
    //    //while (player.TagObject == null)
    //    //{
    //    //    yield return null;
    //    //}
    //    //Teammates.Add(player);
    //    //Color color = teams—olors[i];
    //    //Vector3 vector3 = new Vector3(color.r, color.g, color.b);
    //    //player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "teams—olors", vector3 } } );
    //}
}
