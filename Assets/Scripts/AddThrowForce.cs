using Photon.Pun;
using System.Collections;
using UnityEngine;

public class AddThrowForce : MonoBehaviour
{
    [HideInInspector] public Transform WeaponPlace;

    private void Start()
    {
        StartCoroutine(WaitTagObject());
    }

    private IEnumerator WaitTagObject()
    {
        while (PhotonNetwork.LocalPlayer.TagObject == null)
        {
            yield return null;
        }

        Transform player = (Transform)PhotonNetwork.LocalPlayer.TagObject;
        WeaponPlace = player.GetComponentInChildren<RotateGun>().transform;
        //photonView = weaponPlace.GetComponent<PhotonView>();
    }

    public void Throw(GameObject weapon, Quaternion target)
    {
        Rigidbody2D itemRigidbody = weapon.GetComponent<Rigidbody2D>();

        itemRigidbody.isKinematic = false;
        itemRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //var weaponPoint = ((Transform)PhotonNetwork.PlayerList[playerId - LeaveRoom.current.DisabledMasterClients - 1].TagObject).GetComponentInChildren<RotateGun>().transform;
        //itemRigidbody.position = new Vector3(target.position.x, target.position.y, 0f);
        itemRigidbody.AddForce(target * Vector2.right * 100f);
    }
}
