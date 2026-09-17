using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour //, IPunInstantiateMagicCallback
{
    private int damage;
    [SerializeField] private float speed;
    //[SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private PhotonView photonViewMine;
    //private PhotonView photonViewAther;
    private bool isHit;

    private void Start()
    {
        //SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Players"));
        damage = (int)photonViewMine.InstantiationData[0];

        if (!KillsView.IsAlive(photonViewMine.Owner)) Destroy(gameObject);

        //int viewID = (int)photonViewMine.InstantiationData[1];
        //if (viewID >= 0)
        //{
        //    ChangePosition(viewID);
        //    //List<Collider2D> results = new List<Collider2D>();
        //    //ContactFilter2D contactFilter2D = new ContactFilter2D();
        //    //contactFilter2D.useTriggers = true;
        //    //collider2D.OverlapCollider(contactFilter2D, results);
        //    ////Debug.Log("Bullet Contact Count");
        //    ////Debug.Log(results.Count);
        //    //foreach (var s in results)
        //    //{
        //    //    Debug.Log("BulletContact");
        //    //    var lift = s.GetComponentInParent<Lift>();
        //    //    if (lift != null)
        //    //    {
        //    //        int viewID = lift.Pv.ViewID;
        //    //        photonViewMine.RPC(nameof(ChangePosition), RpcTarget.OthersBuffered, viewID);
        //    //        ChangePosition(viewID);
        //    //        break;
        //    //    }
        //    //}
        //}

        ChangeVelocity();
        //Destroy(gameObject, (float)photonViewMine.InstantiationData[1]);
    }

    //public void OnPhotonInstantiate(PhotonMessageInfo info)
    //{
    //    var parent = ((Transform)info.Sender.TagObject).parent;
    //    if(parent != null)
    //    {
    //        var lift = parent.GetComponent<Lift>();
    //        if (lift != null)
    //        {
    //            Debug.Log("OnPhotonInstantiate");
    //            transform.position = parent.transform.position + (Vector3)photonViewMine.InstantiationData[1];
    //        }
    //    }
    //}

    //private void ChangePosition(int viewID)
    //{
    //    Debug.Log((Vector3)photonViewMine.InstantiationData[2]);
    //    transform.position = PhotonNetwork.GetPhotonView(viewID).transform.position + (Vector3)photonViewMine.InstantiationData[2];
    //}

    private void ChangeVelocity()
    {
        collider2D.attachedRigidbody.velocity = transform.rotation * Vector2.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit(collision);
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    toDestroy = true;
    //}

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    Debug.Log("OnTriggerEnter2D Bullet");
    //    HitBoxes hitBoxes = collider.GetComponent<HitBoxes>();
    //    if (hitBoxes != null && hitBoxes.enabled == true)
    //    {
    //        Debug.Log("hitBoxes Bullet");
    //        if (hitBoxes.PhotonHealthSyn.Owner.GetPhotonTeam() == photonViewMine.Owner.GetPhotonTeam())
    //        {
    //            Debug.Log("IgnoreCollision");
    //            Physics2D.IgnoreCollision(collider, collider2D);
    //            toDestroy = false;
    //        }
    //    }
    //}

    private void Hit(Collision2D collision)
    {
        if (isHit) return;

        var target = collision.collider;
        var effect = target.GetComponent<BulletHitEffect>();
        Vector2 from = transform.position;
        if (effect != null)
        {
            Vector3 reflected = Vector3.Reflect(from.normalized, collision.contacts[0].normal);
            Quaternion rot = Quaternion.FromToRotation(transform.right, reflected);
            if (transform.rotation.eulerAngles.z > 180f || transform.rotation.eulerAngles.z < 360f)
            {
                rot *= Quaternion.AngleAxis(180f, Vector3.forward);
            }
            effect.Effect(from, rot);
        }


        //if (photonViewAther.Owner.GetPhotonTeam() == photonViewMine.Owner.GetPhotonTeam())
        //{
        //    collider2D.isTrigger = true;
        //    rigidbody2D.velocity = transform.rotation * Vector2.right * speed;
        //}



        bool toDestroy = true;
        HitBoxes hitBoxes = target.GetComponent<HitBoxes>();
        if (hitBoxes != null && hitBoxes.enabled == true)
        {
            Debug.Log("Bullet 1");
            if (hitBoxes.PhotonHealthSyn.IsRoomView || hitBoxes.PhotonHealthSyn.Owner.GetPhotonTeam() != photonViewMine.Owner.GetPhotonTeam())
            {
                Debug.Log("Bullet 2");
                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("Bullet 3");
                    int newDamage = Mathf.RoundToInt(damage * hitBoxes.Factor(target, this));
                    if (newDamage > 0) hitBoxes.PhotonHealthSyn.RPC("ChangeHealth", RpcTarget.AllBuffered, newDamage, transform.position, collision.contacts[0].normal, photonViewMine.Owner);
                }
                    //isHit = true;
                    //PhotonNetwork.Destroy(gameObject);

                    //if (photonViewAther.IsRoomView) // || photonViewAther.Owner.GetPhotonTeam() != photonViewMine.Owner.GetPhotonTeam()
                    //{
                    //}
                    //else if(photonViewAther.Owner.GetPhotonTeam() == photonViewMine.Owner.GetPhotonTeam())
                    //{
                    //    photonViewMine.RPC(nameof(ChangeTriggering), RpcTarget.All, true);
                    //    rigidbody2D.velocity = transform.rotation * Vector2.right * speed;
                    //}
            }
            else
            {
                Physics2D.IgnoreCollision(target, collider2D);
                ChangeVelocity();
                toDestroy = false;
            }
        }

        if (toDestroy)
        {
            isHit = true;
            Destroy(gameObject);  //PhotonNetwork.
        }
    }

    //[PunRPC]
    //private void ChangeTriggering(bool value)
    //{
    //    collider2D.isTrigger = value;
    //}
}
