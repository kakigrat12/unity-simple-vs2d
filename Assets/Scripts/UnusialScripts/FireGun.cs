using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class FireGun : MonoBehaviour
{
    public GameObject BulletPrefab;
    [SerializeField] private Transform firePoint;

    private enum typeShoot { oneShoot, machineGun };
    [SerializeField] private typeShoot _typeShoot;

    [SerializeField] private int _shop = 30;
    [SerializeField] private float timeShoot;
    //public int NumberCartridges;
    public string namesCartridges;
    [SerializeField] private float timeRecharge;

    private static int shop;
    public PhotonView photonView;
    public PlayerWeapons playerWeapons;
    private CatridgeKind catridgeKind;
    //private int numberCartridges;

    [SerializeField] private bool recharged = true;

    private int ID;

    private void Start()
    {
        ////GameEvents.current.onThrow += End;
        //shop = _shop;
        //ID = GetComponent<PhotonView>().ViewID;

        //var catridge = null; // playerWeapons.catridgeKind;
        //for (int i = 0; i < catridge.Length; i++)
        //{
        //    if(catridge[i].nameCatridge == namesCartridges)
        //    {
        //        catridgeKind = catridge[i];
        //    }
        //}
        ////numberCartridges = _numberCartridges;
        ////StartCoroutine(recharge());
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        //Debug.Log(catridgeKind.quantity);

        if (recharged)
        {
            if (_shop > 0)
            {
                switch (_typeShoot)
                {
                    case typeShoot.oneShoot:
                        if (Input.GetButtonDown("Fire1"))
                            Shoot();
                        break;

                    case typeShoot.machineGun:
                        if (Input.GetButton("Fire1"))
                            Shoot();
                        break;

                }
            }
            else if(Input.GetButtonDown("Fire1"))
            {
                Recharge();
            }
        }
        if (Input.GetKeyDown("r"))
        {
            Recharge();
        }
    }

    private IEnumerator timer(float coundownTime, bool isRecharge)
    {
        recharged = false;

        while (coundownTime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            coundownTime -= 0.1f;
        }
        if (isRecharge)
        {
            int difference = shop - _shop;
            //Debug.Log((int)Mathf.Clamp(_numberCartridges - difference, -_numberCartridges, 0f));
            //difference = (int)Mathf.Clamp(_numberCartridges - difference, -_numberCartridges, 0f) + difference;
            difference += (int)Mathf.Clamp(catridgeKind.quantity - difference, -10000f, 0f);
            //Debug.Log((int)Mathf.Clamp(_numberCartridges - difference, -_numberCartridges, 0f) + difference);
            Debug.Log(difference);
            catridgeKind.quantity -= difference;
            _shop += difference;
        }
        //if(_shop > 0)
        //{
        //    recharged = true;
        //}
        recharged = true;
    }

    //private IEnumerator recharge()
    //{
    //    for (shop = _shop; shop > 0; shop--)
    //    {
    //        yield return new WaitForSeconds(timeShoot);
    //        isRecharging = false;
    //    }

    //    yield return new WaitForSeconds(timeRecharge);

    //    shop = Mathf.Clamp(_numberCartridges, 0, _shop);
    //    _numberCartridges -= shop;
    //}
    private void Recharge()
    {
        if(_shop < shop && catridgeKind.quantity > 0)
        {
            StartCoroutine(timer(timeRecharge, true));
        }
    }
    private void Shoot()
    {
        StartCoroutine(timer(timeShoot, false));
        _shop -= 1;
        PhotonNetwork.Instantiate(BulletPrefab.name, firePoint.transform.position, firePoint.transform.rotation);
    }

    private void End(int id)
    {
        if (id == ID)
        {
            //GameEvents.current.onThrow -= End;
            enabled = false;
        }
    }
}
