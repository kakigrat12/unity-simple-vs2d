using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    //[SerializeField] private Transform weaponPlace;
    //[SerializeField] private Inventory inventory;
    [SerializeField] private WeaponFire weaponFire;
    //[HideInInspector] public WeaponItemData[] oldWeapons;
    //[SerializeField] private int maxCount = 3;
    //public int countOfWeapons = 0;
    //public int Chosen = 0;
    //private bool isChanged = false;

    //[Space]

    //[SerializeField] private GameObject weaponPrefab;

    //private PhotonView photonView;
    //private PhotonView inventoryPhotonView;
    //public WeaponsPanel weaponsPanel;

    //private RaiseEventOptions raiseEventOptions;

    //private void Start()
    //{
    //    //GameEvents.current.onThingListUpdate += NewWeapon;

    //    //oldWeapons = new WeaponItemData[maxCount];

    //    //photonView = GetComponent<PhotonView>();
    //    //inventoryPhotonView = transform.parent.Find("Inventory").GetComponent<PhotonView>();
    //    //inventory = GetComponent<Inventory>();

    //    //PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    //    //raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

    //    //GetComponent<RotateGun>().photonView = photonView;
    //}

    //private void Update()
    //{
    //    //if (!photonView.IsMine) return;

    //    //var value = Input.mouseScrollDelta.y;
    //    //if (value != 0) photonView.RPC("ThingHandler", RpcTarget.All, value);

    //    //ChangeWeapon(Mathf.Clamp(Chosen - Mathf.RoundToInt(Input.mouseScrollDelta.y), 0, weaponPlace.childCount - 1));

    //    //if (chosen < weaponPlace.childCount)
    //    //{
    //    //    ChangeWeapon(chosen - Mathf.RoundToInt(Input.mouseScrollDelta.y));
    //    //    //photonView.RPC("ChangeWeapon", RpcTarget.All, chosen - Input.mouseScrollDelta.y);
    //    //    //object[] datas = new object[] { numberWeapon + 1, photonView.ViewID };
    //    //    //PhotonNetwork.RaiseEvent(2, datas, raiseEventOptions, SendOptions.SendUnreliable);
    //    //    //ChangeWeapon(numberWeapon + 1);
    //    //}
    //    //else
    //    //{
    //    //    ChangeWeapon(0);
    //    //    //photonView.RPC("ChangeWeapon", RpcTarget.All, 0);
    //    //    //object[] datas = new object[] { 0, photonView.ViewID };
    //    //    //PhotonNetwork.RaiseEvent(2, datas, raiseEventOptions, SendOptions.SendUnreliable);
    //    //    //ChangeWeapon(0);
    //    //}

    //    //if (Input.mouseScrollDelta.y == -1f)
    //    //{

    //    //}

    //    //if (Input.anyKeyDown)
    //    //{
    //    //    int newNumber = int.Parse(Input.inputString) - 1;
    //    //    if (newNumber < weaponPlace.childCount && newNumber >= 0)
    //    //    {
    //    //        ChangeWeapon(newNumber);
    //    //        //object[] datas = new object[] { newNumber, photonView.ViewID };
    //    //        //PhotonNetwork.RaiseEvent(2, datas, raiseEventOptions, SendOptions.SendUnreliable);
    //    //        //ChangeWeapon(newNumber);
    //    //    }
    //    //}
    //}

    //private void ChangeWeapon(int newNumber)
    //{
    //    photonView.RPC("ThingHandler", RpcTarget.All, newNumber);
    //    //ThingHandler(weaponPlace.GetChild(chosen), false);
    //    //ThingHandler(weaponPlace.GetChild(newNumber), true);
    //    //Chosen = newNumber;
    //}

    [PunRPC]
    public void ThingHandler(float changing)
    {
        for (int i = 0; i < Mathf.Abs(changing); i++)
        {
            if (changing > 0)
            {
                transform.GetChild(0).SetSiblingIndex(transform.childCount - 1);
            }
            else
            {
                transform.GetChild(transform.childCount - 1).SetSiblingIndex(0);
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (i == transform.childCount - 1)
            {

                child.SetActive(true);
                //weaponFire.NewWeapon(child.GetComponent<PhotonView>());
            }
            else
            {
                child.SetActive(false);
            }
        }
        //if (photonView.IsMine) 
        //{
        //    weaponsPanel.Render(inventoryPhotonView, transform); 
        //}
        //for (int i = 0; i < weaponPlace.childCount; i++)
        //{
        //    bool isActive = false;
        //    if (i == childNumber)
        //    {
        //        isActive = true;
        //    }
        //    weaponPlace.GetChild(i).gameObject.SetActive(isActive);
        //}
        //if (childNumber < countOfWeapons) weaponPlace.GetChild(childNumber).gameObject.SetActive(isActive);
        //thing.GetComponent<FireGun>().enabled = isActive;
        //thing.GetComponent<RotateGun>().enabled = isActive;
    }

    //public void TruToThrowWeapon(int order)
    //{
    //    if (transform.childCount == maxCount)
    //    {
    //        //inventory.ThrowItem(order, 1, transform.GetChild(0)); //Chosen
    //    }
    //}

    

    //private void NewWeapon(int order, bool isPut)
    //{
    //    AssetItem item = inventory.assetsItemContainer.assetItems[order];
    //    if (item.Container == 0 || !photonView.IsMine) return;
    //    if (isChanged)
    //    {
    //        isChanged = false;
    //        return;
    //    }

    //    WeaponItemData processedWeapon = (WeaponItemData)item;

    //    if (isPut)
    //    {
    //        //object[] data = new object[2] { order, chosen + 1 };
    //        if (countOfWeapons < maxCount)
    //        {
    //            Debug.Log("isPur 1");
    //            oldWeapons[countOfWeapons] = processedWeapon;

    //            //PhotonNetwork.Instantiate(weaponPrefab.name, Vector2.zero, Quaternion.identity, 8, data);

    //            countOfWeapons++;
    //        }
    //        else
    //        {
    //            int orderId = oldWeapons[Chosen].Order;
    //            //PhotonNetwork.Destroy(weaponPlace.GetChild(Chosen).gameObject);

    //            oldWeapons[Chosen] = processedWeapon;
    //            isChanged = true;

    //            //inventory.GetComponent<PhotonView>().RPC("ThrowItem", RpcTarget.All, orderId, 1);

    //            //PhotonNetwork.Instantiate(weaponPrefab.name, Vector2.zero, Quaternion.identity, 8, data);
    //        }
    //    }
    //    else
    //    {
    //        bool isThrown = false;
    //        for (int i = 0; i < countOfWeapons; i++)
    //        {
    //            if (oldWeapons[i].Order == order)
    //            {
    //                isThrown = true;
    //            }

    //            if (isThrown)
    //            {
    //                if (i < oldWeapons.Length - 1)
    //                {
    //                    oldWeapons[i] = oldWeapons[i + 1];
    //                }
    //                else
    //                {
    //                    oldWeapons[i] = null;
    //                }
    //            }
    //        }
    //        //PhotonNetwork.Destroy(weaponPlace.GetChild(chosen).gameObject);
    //        countOfWeapons--;
    //    }

    //    ChangeWeapon(Mathf.Clamp(Chosen, 0, countOfWeapons - 1));
    //    Debug.Log(Mathf.Clamp(Chosen, 0, countOfWeapons - 1));
    //    weaponsPanel.Render(photonView, oldWeapons);
    //}

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    if (obj.Code == 2)
    //    {
    //        object[] datas = (object[])obj.CustomData;
    //        if ((int)datas[1] == photonView.ViewID)
    //        {
    //            ChangeWeapon((int)datas[0]);
    //        }
    //    }
    //}

    //public void NewCatridge(GameObject catridge)
    //{
    //    CatridgeBox catridgeBox = catridge.GetComponent<CatridgeBox>();
    //    foreach(var s in catridgeKind)
    //    {
    //        if (s.nameCatridge == catridgeBox.nameCatridge)
    //        {
    //            s.quantity += catridgeBox.quantity;
    //            catridge.SetActive(false);
    //        }
    //    }
    //}

    ////private void AddPotrons(int i, int quantity)
    ////{
    ////    weapons[i].GetComponent<FireGun>().NumberCartridges += quantity;
    ////}


    //public void NewWeapon(GameObject weapon)
    //{
    //    weapon.transform.SetParent(weaponPlace);

    //    weapon.GetComponent<FlipSprite>().photonView = photonView;
    //    weapon.GetComponent<FireGun>().photonView = photonView;

    //    weapon.GetComponent<FireGun>().playerWeapons = this;
    //    //weapon.GetComponent<ThrowAwayThing>().enabled = false;
    //    weapon.GetComponent<BoxCollider2D>().enabled = false;
    //    //weapon.GetComponent<PhotonTransformView>().enabled = true;

    //    weapon.transform.localPosition = Vector2.zero;
    //    weapon.transform.localRotation = Quaternion.identity;

    //    if (weapons.Count < maxAmount)
    //    {
    //        weapons.Add(weapon);
    //        //numberWeapon = weapons.Count - 1;
    //    }
    //    else
    //    {
    //        Debug.Log("Throw");
    //        Throw(weapon);
    //    }

    //    ChangeWeapon(numberWeapon);
    //    //namesCatridges[numberWeapon] = weapon.GetComponent<FireGun>().namesCartridges;
    //}

    //private void Throw(GameObject weapon)
    //{

    //    //namesCatridges[numberWeapon] = null;
    //    //weapons[numberWeapon].GetComponent<FlipSprite>().End();
    //    //weapons[numberWeapon].GetComponent<FlipSprite>().enabled = false;
    //    //weapons[numberWeapon].GetComponent<FireGun>().enabled = false;
    //    //weapons[numberWeapon].GetComponent<PutThing>().enabled = true;

    //    //weapons[numberWeapon].GetComponent<ThrowAwayThing>().enabled = true;
    //    //weapon.GetComponent<FireGun>().photonView = null;
    //    weapons[numberWeapon].GetComponent<BoxCollider2D>().enabled = true;

    //    weapons[numberWeapon].GetComponent<ThrowAwayThing>().Throw();

    //    weapons[numberWeapon] = weapon;
    //}
}
