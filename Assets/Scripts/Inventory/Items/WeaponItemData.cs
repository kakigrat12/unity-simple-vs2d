using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TypeShoot { oneShoot, machineGun, shotgun };
[CreateAssetMenu(fileName = "newItemWeapon", menuName = "Data/Items/Weapon", order = 51)]
public class WeaponItemData :  IItem, IHandsItem
{
    [Header("WeaponSettings")]
    //public Sprite IconInHand;
    public Sprite IconBackground;
    public Sprite IconOnScreen;
    public Sprite IconInThing;

    [Space]

    public TypeShoot TypeOfShoot;
    public AssetItem Patrons;
    public int Damage;
    public float TimeToDestroyBullet = 1.2f;
    public int Shop;
    public float TimeTake;
    public float TimeShoot;
    public float TimeRecharge;
    public float TimeBeforeShoot;
    public Vector3 FirePoint;

    [Space]
    [Header("Effects")]

    public RuntimeAnimatorController animatorController;

    public AudioClip Take;
    public AudioClip Shoot;
    public AudioClip Recharge;

    public RuntimeAnimatorController _animatorController()
    {
        return animatorController;
    }

    public AudioClip[] Audio()
    {
        return new AudioClip[3] { Take, Shoot, Recharge };
    }

    //public override Sprite IconOnPlayer()
    //{
    //    return IconInHand;
    //}

    //PlayerWeapons playerWeapons;

    //private void Awake()
    //{
    //    Container = 1;
    //}

    public override void IsPutting(Thing thing)
    {
        Debug.Log("AddWeaponW");

        Weapons.current.AddWeapon(thing);

        //thing.transform = container;
        //playersWeapons.Add(weapon);

        //playerWeapons = container.GetComponent<PlayerWeapons>();
        //playerWeapons.TruToThrowWeapon(Order);
        //thing.ThingView();
        //Работает с трансформ
        //weapon.SetParent(container);
        //weapon.SetSiblingIndex(0);
        //weapon.localPosition = Vector2.zero;
        //weapon.localRotation = Quaternion.identity;
        //playerWeapons.ThingHandler(0);
    }

    public override void IsThrowing(Thing thing, PointerEventData eventData, int numberInCell)
    {
        Weapons.current.ThrowWeapon(thing, false);
        ////Transform weapon, Vector2 pos, Quaternion rot
        //Debug.Log("MinWeaponW");

        //weapon.GetComponent<Thing>().ThingView();
        //weapon.SetParent(default);
        //weapon.position -= new Vector3(0f, 0f, 1f);
        //playerWeapons.ThingHandler(0);

        ////if (photonView.IsMine)
        ////{
        ////    photonView.transform.GetChild(3).GetChild(siblingIndex).GetComponent<PhotonView>().RPC("ThrowWeapon", RpcTarget.All);
        ////}

        ////Player player = PhotonNetwork.LocalPlayer;
        ////Transform weapon = ((Transform)player.TagObject).GetChild(3).GetChild(siblingIndex);
        ////weapon.GetComponent<PhotonView>().RPC("ThrowWeapon", RpcTarget.All);
        ////weapon.GetComponent<Thing>().ThrowWeapon();
    }

    public override void StartGame(Thing thing)
    {
    //    animatorController.animationClips[0].frameRate = 0.1f;
    //    animatorController.SetFl

        thing.ChangeInstantiationData(MaxCollectionCount, 1);
        thing.ChangeInstantiationData(Shop, 2);
    }

    public override object[] ThingPanelData(int number, object[] instantiationData)
    {
        object[] data = new object[] { Name, IconInThing, instantiationData[2], Patrons.InventoryIcon};
        return data;
    }

    //public override void CellsRender(InventorySimplePanel inventorySimplePanel)
    //{
    //}
}
