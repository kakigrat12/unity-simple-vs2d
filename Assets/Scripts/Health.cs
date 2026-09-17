using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(PhotonView))]
//[RequireComponent(typeof(HitBoxes))]
public class Health : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    public int Value = 100;
    [HideInInspector] public int MaxValue;
    [SerializeField] private bool isPlayer;

    public UnityEvent Death;

    private bool isDead = false;

    [HideInInspector] public PhotonView lastDamagePhoton;
    [HideInInspector] public Player lastDamagePlayer;
    [HideInInspector] public Vector3 lastDamagePosition;
    [HideInInspector] public Vector2 lastDamageNormal;
    [HideInInspector] public int lastDamage;

    //private Scrollbar mainHealthBar;
    //[SerializeField] private GameObject damageTextPrefab;
    //[SerializeField] private Color color1;
    //[SerializeField] private Color color2;
    //private GameObject damageText;
    //private RectTransform damageTextTransform;
    //[SerializeField] private GameObject healthBarPrefab;
    //private RectTransform healthBarTransform;
    //private Scrollbar healthBarScroll;
    //[SerializeField] private Vector3 healthBarPosition;

    //private List<float> healthPluses = new List<float>();

    //[SerializeField] private Inventory inventory;
    //[SerializeField] private PhotonView inventoryPhotonView;

    //private Vector3 textWorldPosition;
    //private GameObject canvas;
    //private Camera mainCamera;

    //private void Awake()
    //{
    //    PhotonNetwork.LocalPlayer.TagObject = gameObject.transform;
    //    Debug.Log(PhotonNetwork.LocalPlayer.TagObject);
    //}

    private void Start()
    {
        //SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Players"));

        MaxValue = Value;

        photonView.Owner.TagObject = transform; // пусть сделает спаунер

        //PhotonNetwork.LocalPlayer.CustomProperties["health"] = value;
        //PhotonNetwork.LocalPlayer.CustomProperties["maxHealth"] = maxValue;
        //PhotonNetwork.LocalPlayer.CustomProperties["isDead"] = false;
        //PhotonNetwork.LocalPlayer.SetCustomProperties[] Можно использовать для авто синхронизации
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "maxHealth", MaxValue } } );
        //mainCamera = Camera.main;
        //canvas = GameObject.Find("Canvas");

        //PhotonTeamsManager.Instance.

        //healthBar = canvas.Find("ScrollbarHealth").GetComponent<Scrollbar>();
        //PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;

        //if (photonView.IsMine)
        //{
        //    mainHealthBar = canvas.GetComponentInChildren<Scrollbar>();
        //    mainHealthBar.size = 1;
        //}

        //StartCoroutine(WaitFillingTeammates());
    }

    private void OnDisable()
    {
        photonView.Owner.TagObject = null;
    }

    //private void ChangeProperties()
    //{

    //}

    //private IEnumerator WaitFillingTeammates()
    //{
    //    while (SpawnPlayer.spawnPlayer.Teammates.Count < SpawnPlayer.spawnPlayer.roomInformation.CountPlayersInTeam)
    //    {
    //        yield return null;
    //    }

    //    int index = SpawnPlayer.spawnPlayer.Teammates.FindIndex(x => x == PhotonNetwork.LocalPlayer);
    //    if (index != -1 && !photonView.IsMine)
    //    {
    //        healthBarPrefab = Instantiate(healthBarPrefab, canvas.transform);
    //        healthBarPrefab.GetComponent<DestroyWithPlayer>().ThisPlayer = photonView.Owner;
    //        healthBarTransform = healthBarPrefab.GetComponent<RectTransform>();
    //        healthBarScroll = healthBarPrefab.GetComponent<Scrollbar>();

    //        healthBarScroll.size = value / maxValue;
    //    }
    //}

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    if(obj.Code == 4)
    //    {
    //        object[] datas = (object[])obj.CustomData;
    //        if ((int)datas[1] == photonView.ViewID)
    //        {
    //            onUseHilk((int)datas[0]);
    //        }
    //    }
    //}

    [PunRPC]
    private void ChangeHealth(int Damage, Vector3 collisionPosition, Vector2 normal, Player DamagePlayer, PhotonMessageInfo info) //, PhotonMessageInfo info
    {
        //Bullet bullet = info.photonView.GetComponent<Bullet>();
        //if (collisionPosition != null) textWorldPosition = collisionPosition;
        Debug.Log(Damage);
        Value -= Damage;
        Value = Mathf.Clamp(Value, 0, MaxValue);

        lastDamagePhoton = info.photonView;
        lastDamage = Damage;
        lastDamagePosition = collisionPosition;
        lastDamageNormal = normal;
        lastDamagePlayer = DamagePlayer; // info.Sender;
                                         //DamagePlayer.TryGetTeamMates(out Player[] fff);


        if (Value <= 0 && !isDead)  // && !isDead
        {
            //StartCoroutine(DeathScenario());
            Debug.Log("Death");
            isDead = true;
            Death?.Invoke();
            if (isPlayer) GameEvents.current.Kill(DamagePlayer, photonView.Owner); //info.Sender, photonView.Owner
        }

        if (isPlayer)
        {
            //PhotonNetwork.LocalPlayer.CustomProperties["health"] = value; 
            GameEvents.current.HealthChanged(this);
        }

        //float proportion = value / maxValue;

        //if (mainHealthBar != null)
        //{
        //    mainHealthBar.size = proportion;
        //}

        //if (healthBarScroll != null)
        //{
        //    healthBarScroll.size = proportion;
        //}

        //info.Sender.GetPhotonTeam().

        //Invoke("StopCoroutine", 1f);

        //if (!photonView.IsMine && collisionPosition != null)
        //{
        //    damageText = Instantiate(damageTextPrefab, canvas.transform);
        //    Color color = Color.Lerp(color2, color1, proportion);
        //    Text text = damageText.GetComponentInChildren<Text>();
        //    text.text = Damage.ToString();
        //    text.color = color;
        //    damageTextTransform = damageText.GetComponent<RectTransform>();
        //    Destroy(damageText, 0.6f);
        //}
    }

    private IEnumerator DeathScenario()
    {
        yield return new WaitForSeconds(1.2f);
        gameObject.SetActive(false);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown("h") && photonView.IsMine)
    //    {
    //        TryToUseHilk(0);
    //    }

    //    if (damageTextTransform != null)
    //    {
    //        damageTextTransform.position = mainCamera.WorldToScreenPoint(textWorldPosition);
    //    }

    //    if (healthBarTransform != null)
    //    {
    //        healthBarTransform.position = mainCamera.WorldToScreenPoint(transform.position + healthBarPosition);
    //    }
    //}

    //private void StopCoroutine()
    //{
    //    Destroy(damageText);
    //}

    //private void Die()
    //{
    //    gameObject.SetActive(false);
    //}

    //private void TryToUseHilk(int numberHilk)
    //{
    //    var hilk = hilkItemData[numberHilk];
    //    if (InventorySimplePanel.current.countOneTypeItems[hilk.Order] > 0 && value < maxValue)
    //    {
    //        InventorySimplePanel.current.Grouper(hilk.Order, -1);
    //        //inventoryPhotonView.RPC("RemoveItem", RpcTarget.All, hilk.Order, 1);
    //        photonView.RPC("ChangeHealth", RpcTarget.All, -hilk.Value, null, null);
    //    }
    //}

    //public void NewHilk(GameObject hilk)
    //{
    //    Debug.Log(hilk.GetComponent<HilkBox>().healthPlus);
    //    healthPluses.Add(hilk.GetComponent<HilkBox>().healthPlus);
    //    hilk.SetActive(false);
    //}
    //public void UseHilk(int number)
    //{
    //    if (healthPluses[number] + health <= maxHealth)
    //    {
    //        object[] datas = new object[] { number, photonView.ViewID };
    //        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
    //        PhotonNetwork.RaiseEvent(4, datas, raiseEventOptions, SendOptions.SendUnreliable);
    //    }
    //}
    //private void onUseHilk(int number)
    //{
    //    health = Mathf.Clamp(health + healthPluses[number], 0, maxHealth);
    //}
}
