using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AddThrowForce))]
public class Weapons : MonoBehaviour
{
    public static Weapons current;

    [SerializeField] private PhotonView photonView;
    private AddThrowForce addThrowForce;
    public int MaxCount;
    //private Transform weaponPlace;
    public List<Thing> Things = new List<Thing>();
    public int Selected;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        GameEvents.current.onKill += ThrowAllWeapons;
        addThrowForce = GetComponent<AddThrowForce>();
        //StartCoroutine(WaitTagObject());
    }
    private void OnDisable()
    {
        GameEvents.current.onKill -= ThrowAllWeapons;
    }
    //private IEnumerator WaitTagObject()
    //{
    //    while (PhotonNetwork.LocalPlayer.TagObject == null)
    //    {
    //        yield return null;
    //    }

    //    Transform player = (Transform)PhotonNetwork.LocalPlayer.TagObject;
    //    weaponPlace = player.GetComponentInChildren<RotateGun>().transform;
    //    //photonView = weaponPlace.GetComponent<PhotonView>();
    //}

    private void Update()
    {
        int newMeaning = Selected;
        var message = Input.inputString;
        if (!string.IsNullOrEmpty(message) && int.TryParse(message, out int m) && m <= Things.Count)
        {
            newMeaning = m - 1;
        }

        var value = Mathf.RoundToInt(Input.mouseScrollDelta.y);
        if (value != 0)
        {
            newMeaning = Selected + value;
            if (newMeaning >= Things.Count)
            {
                newMeaning = 0;
            }
            else if (newMeaning < 0)
            {
                newMeaning = Things.Count - 1;
            }
            newMeaning = Mathf.Clamp(newMeaning, 0, Things.Count - 1);
            //GameEvents.current.PatronsUpdate();
            //UpdateWeapons(value);
        }

        if(newMeaning != Selected)
        {
            Selected = newMeaning;
            GameEvents.current.ChoseWeapon();
        }
    }

    public void AddWeapon(Thing thing)
    {
        if (Things.Count >= MaxCount)
        {
            ThrowWeapon(Things[Selected], true);
            Things[Selected] = thing;
        }
        else
        {
            Things.Add(thing);
            Selected = Things.Count - 1;
        }
        photonView.RPC("ChangeActive", RpcTarget.AllBuffered, thing.PhotonView.ViewID, false, addThrowForce.WeaponPlace.position, addThrowForce.WeaponPlace.rotation, thing.PhotonView.InstantiationData);
        GameEvents.current.ChoseWeapon();

        //Instantiate(cellPrefab, containerOnScreen);
        //UpdateWeapons(0f);

        //thing.gameObject.SetActive(false);
        //thing.transform.SetParent(weaponPlace);
    }

    public void ThrowWeapon(Thing thing, bool changed)
    {
        if (!changed)
        {
            Things.Remove(thing);
        }
        photonView.RPC(nameof(ChangeActive), RpcTarget.AllBuffered, thing.PhotonView.ViewID, true, addThrowForce.WeaponPlace.position, addThrowForce.WeaponPlace.rotation, thing.PhotonView.InstantiationData);
        Selected = Mathf.Clamp(Selected, 0, Things.Count - 1);
        GameEvents.current.ChoseWeapon();

        //Destroy(containerOnScreen.GetChild(0).gameObject);
        //UpdateWeapons(0f);
    }

    [PunRPC]
    private void ChangeActive(int viewID, bool active, Vector3 pos, Quaternion rot, object[] instantiationData)
    {
        var potonViewWeapon = PhotonNetwork.GetPhotonView(viewID);
        var weapon = potonViewWeapon.gameObject;
        weapon.SetActive(active);

        if (active)
        {
            potonViewWeapon.InstantiationData = instantiationData;
            ForceThrow(weapon, new Vector3(pos.x, pos.y, -0.1f), rot); 
        }
    }

    private void ForceThrow(GameObject weapon, Vector3 pos, Quaternion rot)
    {
        //Transform weaponPoint = ((Transform)info.Sender.TagObject).GetComponentInChildren<RotateGun>().transform;
        weapon.transform.position = pos;

        addThrowForce.Throw(weapon, rot);

        //weapon.GetComponent<Thing>().Throw(info.Sender.ActorNumber);//Можно переделать но только вместе с обычным инвентарём
    }

    private void ThrowAllWeapons(Player f, Player murder)
    {
        if (murder == PhotonNetwork.LocalPlayer)
        {
            //Небольшой костыль, лучше в предмеьах Trow убирать и описывать AddForce как-то отдельно, наверное
            float angel = 180f / Things.Count - 1;

            for (int i = 0; i < Things.Count; i++)
            {
                var weapon = Things[i];
                photonView.RPC("ChangeActive", RpcTarget.AllBuffered, weapon.PhotonView.ViewID, true, addThrowForce.WeaponPlace.position, Quaternion.AngleAxis(angel * i, Vector3.forward), weapon.PhotonView.InstantiationData);
            }
            Things.Clear();
            GameEvents.current.ChoseWeapon();
        }
    }
}
