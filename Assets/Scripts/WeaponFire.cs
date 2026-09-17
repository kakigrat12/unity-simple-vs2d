using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    //[SerializeField] private Inventory inventory;
    //[SerializeField] private PhotonView inventoryPhotonView;
    [SerializeField] private HandsPlayer handsPlayer;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private KeyCode keyCodeToBlock;
    //[SerializeField] private Texture2D invMouse;
    //[SerializeField] private Texture2D defaultMouse;
    private List<Collider2D> NotCollisionColliders = new List<Collider2D>();
    private PhotonView weaponPhotonView;
    private bool recharged = true;
    private bool isActive = true;
    //private int shop;

    //private Thing thingInHands;
    private WeaponItemData weaponItemData;
    private List<Quaternion> additionalRotation = new List<Quaternion>();

    public UnityEvent OnNewWeaponEvent;
    public UnityEvent OnShootEvent;
    public UnityEvent OnRechargeEvent;

    //[SerializeField] private ChangeWeaponEvent onChangeWeapon;

    private void Start()
    {
        //GameEvents.current.onDeceleration += ChangeState;
        GameEvents.current.onChoseWeapon += NewWeapon;
        GameEvents.current.onIsAnyPanelOpened += ChangeIsActive;
        //FindCollidersTemmates();
        //ChangeCursor(defaultMouse);
    }

    //Дурак, это всё локально. Иди в пулю!!!
    //private void FindCollidersTemmates()
    //{
    //    foreach (var s in SpawnPlayer.spawnPlayer.Teammates)
    //        NotCollisionColliders.AddRange(((Transform)s.TagObject).GetComponentInChildren<HitBoxes>().Colliders.ToList());
    //}

    //private void SetIgnoreTemmates(Collider2D collider2D)
    //{
    //    foreach(var s in NotCollisionColliders)
    //        Physics2D.IgnoreCollision(collider2D, s);
    //}

    private void OnDisable()
    {
        GameEvents.current.onChoseWeapon -= NewWeapon;
        GameEvents.current.onIsAnyPanelOpened -= ChangeIsActive;
    }

    //private void Start()
    //{
    //    if (!inventoryPhotonView.IsMine) this.enabled = false;
    //}

    public void NewWeapon() //PhotonView _photonView, WeaponItemData _weaponItemData
    {
        //StopAllCoroutines();

        if (weaponPhotonView != null) weaponPhotonView.RPC("ChangeInstantiationData", RpcTarget.All, (int)weaponPhotonView.InstantiationData[2], 0);

        //thingInHands = weaponPhotonView.GetComponent<Thing>();
        //weaponItemData = (WeaponItemData)thingInHands.assetItem;

        var things = Weapons.current.Things;
        if (things.Count > 0)
        {
            var weapon = things[Weapons.current.Selected];
            weaponItemData = (WeaponItemData)weapon.assetItem;
            weaponPhotonView = weapon.PhotonView; 
            
            PatronsUpdate();

            recharged = false;
            handsPlayer.NewItem(weaponItemData, weaponItemData.TimeTake, _Recharged, 0, false);
            photonView.RPC("ChangeAnimation", RpcTarget.All, 0);
        }
        else
        {
            weaponPhotonView = null;
            handsPlayer.NewItem(null, 0f, null, 0, false);
        }

        OnNewWeaponEvent?.Invoke();
        //onChangeWeapon?.Invoke(thingInHands);


        //StartCoroutine(timer(weaponItemData.TimeTake, false));
        //shop = (int)weaponPhotonView.InstantiationData[2];
        //inventory.countOneTypeItems[weaponItemData.Patrons.Order];
    }

    private void Update()
    {
        //if (Input.GetKeyDown(keyCodeToBlock))
        //{
        //    isActive = !isActive;
        //    if (isActive)
        //    {
        //        ChangeCursor(invMouse);
        //    }
        //    else
        //    {
        //        ChangeCursor(defaultMouse);
        //    }
        //}

        if (isActive && recharged && weaponPhotonView) // && transform.childCount > 0
        {
            if ((int)weaponPhotonView.InstantiationData[2] > 0)
            {
                additionalRotation.Clear();
                switch (weaponItemData.TypeOfShoot)
                {
                    case TypeShoot.oneShoot:
                        if (Input.GetButtonDown("Fire1"))
                        {
                            additionalRotation.Add(Quaternion.identity);
                            TryToShoot();
                            //UsePatron();
                        }
                        break;

                    case TypeShoot.machineGun:
                        if (Input.GetButton("Fire1"))
                        {
                            additionalRotation.Add(Quaternion.identity);
                            TryToShoot();
                        }
                        break;

                    case TypeShoot.shotgun:
                        if (Input.GetButtonDown("Fire1"))
                        {
                            for (int i = 0; i <= 14; i++)
                            {
                                additionalRotation.Add(Quaternion.AngleAxis(8 - 1.14286f * i, Vector3.forward));
                                TryToShoot(); //Надобы исправить
                            }
                        }
                        break;

                }
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                Recharge();
            }
        }
        if (Input.GetKeyDown("r"))
        {
            Recharge();
        }
    }

    private void ChangeIsActive(bool newValue)
    {
        isActive = !newValue;
    }

    //private void OnDisable()
    //{
    //    GameEvents.current.onDeceleration -= ChangeState;
    //}

    //private void ChangeState(float isHandsBusy)
    //{
    //    StopAllCoroutines();
    //    recharged = true;
    //}

    //private IEnumerator timer(float coundownTime, bool isRecharge)
    //{
    //    recharged = false;

    //    while (coundownTime > 0)
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //        coundownTime -= 0.1f;
    //    }
    //    if (isRecharge)
    //    {
    //        int difference = weaponItemData.Shop - (int)weaponPhotonView.InstantiationData[2];
    //        difference += (int)Mathf.Clamp(InventorySimplePanel.current.countOneTypeItems[weaponItemData.Patrons.Order] - difference, -10000f, 0f);
    //        UsingPatrons(difference);
    //        weaponPhotonView.InstantiationData[2] = (int)weaponPhotonView.InstantiationData[2] + difference;
    //    }
    //    recharged = true;
    //    //animator.SetInteger("State", 0);
    //    //photonView.RPC("ChangeAnimation", RpcTarget.All, 0);
    //}

    private void _Recharged(bool passed) 
    {
        Recharged();
    }

    private void Recharged() 
    {
        Debug.Log("Recharged");
        recharged = true; 
    }

    private void Recharge()
    {
        if ((int)weaponPhotonView.InstantiationData[2] < weaponItemData.Shop && InventorySimplePanel.current.countOneTypeItems[weaponItemData.Patrons.Order] > 0 && recharged)
        {
            //GameEvents.current.Deceleration(true);
            if (handsPlayer.CanNewAction(weaponItemData.TimeRecharge, ShopRecharge, 1, true))
            {
                //StartCoroutine(timer(weaponItemData.TimeRecharge, true));
                //animator.SetInteger("State", 1);
                recharged = false;
                photonView.RPC("ChangeAnimation", RpcTarget.All, 1);
                OnRechargeEvent?.Invoke();
            }
        }
    }

    private void UsePatron()
    {
        //StartCoroutine(timer(weaponItemData.TimeShoot, false));
        weaponPhotonView.InstantiationData[2] = (int)weaponPhotonView.InstantiationData[2] - 1;
        PatronsUpdate();
    }

    private void ShopRecharge(bool passed)
    {
        if (passed)
        {
            int difference = weaponItemData.Shop - (int)weaponPhotonView.InstantiationData[2];
            difference += (int)Mathf.Clamp(InventorySimplePanel.current.countOneTypeItems[weaponItemData.Patrons.Order] - difference, -10000f, 0f);
            weaponPhotonView.InstantiationData[2] = (int)weaponPhotonView.InstantiationData[2] + difference;

            InventorySimplePanel.current.Grouper(weaponItemData.Patrons.Order, -difference);
            PatronsUpdate();
            //Invoke("PatronsUpdate", 0.0001f);
        }
        //GameEvents.current.Deceleration(false);
        Recharged();
        //PatronsUpdate();
        //InventorySimplePanel.current.countOneTypeItems[weaponItemData.Patrons.Order] -= value;
    }

    private void PatronsUpdate()
    {
        GameEvents.current.PatronsUpdate();
    }

    private void TryToShoot()
    {
        recharged = !handsPlayer.CanNewAction(-1f, WaitBeforeShoot, 2, false);
    }

    private void WaitBeforeShoot(bool passed)
    {
        if (passed) handsPlayer.NewItem(weaponItemData, weaponItemData.TimeBeforeShoot, Shoot, 0, false);
    }

    private void Shoot(bool passed)
    {
        if (passed)
        {
            foreach (var rot in additionalRotation)
            {
                //int PlayerParentViewID = -1;
                //if(transform.parent.parent != default)
                //{
                //    var pv = transform.parent.parent.GetComponent<PhotonView>();
                //    if(pv != null)
                //    {
                //        PlayerParentViewID = pv.ViewID;
                //    }
                //}
                object[] data = new object[] { weaponItemData.Damage, weaponItemData.TimeToDestroyBullet }; //, transform.parent.localPosition + transform.localPosition + transform.rotation * weaponItemData.FirePoint }; //transform.parent.localPosition + 
                var bullet = PhotonNetwork.Instantiate(BulletPrefab.name, transform.position + transform.rotation * weaponItemData.FirePoint, transform.rotation * rot, 0, data);
                //SetIgnoreTemmates(bullet.GetComponent<Collider2D>());
            }
            //animator.SetInteger("State", 2);

            UsePatron();
            Invoke(nameof(Recharged), weaponItemData.TimeShoot);
            photonView.RPC("ChangeAnimation", RpcTarget.All, 2);
            OnShootEvent?.Invoke();
        }
    }
    //private void OnGUI()
    //{
    //    Vector2 m = Event.current.mousePosition;
    //    GUI.depth = 0;
    //    GUI.Label(new Rect(m.x, m.y, invMouse.width, invMouse.height), invMouse);
    //}

    //private void ChangeCursor(Texture2D texture)
    //{
    //    Cursor.SetCursor(texture, new Vector2(invMouse.width/2, invMouse.height/2), CursorMode.Auto);
    //}

    //private void ChangeAnimation(int number)
    //{
    //    animator.SetInteger("State", number);
    //}
}

[System.Serializable]
public class ChangeWeaponEvent : UnityEvent<Thing> { }