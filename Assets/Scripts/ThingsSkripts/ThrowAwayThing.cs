using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThrowAwayThing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private PhotonView photonView;
    private void Start()
    {
        //GameEvents.current.onThrow += Throw;
        //photonView = GetComponent<PhotonView>();
        //rigidbody2D.isKinematic = true;
        //Throw();
    }
    public void Throw()
    {
        Debug.Log("canT");
        //if (id == photonView.ViewID)
        //{
            Debug.Log("canT2");
            rigidbody2D.isKinematic = false;
            transform.localPosition = new Vector3(0f, 0f, -1f);
            transform.SetParent(default);
            //rigidbody2D.AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 0 && !rigidbody2D.isKinematic)
        {
            Debug.Log("Collision");
            rigidbody2D.isKinematic = true;
            rigidbody2D.constraints = (RigidbodyConstraints2D)7;
            //GameEvents.current.Throw(photonView.ViewID);
            //enabled = false;
        }
    }
}
