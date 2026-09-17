using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float timeToChangeCamera = 3f;

    //[HideInInspector] public Transform TargetCamera;

    //private void Start()
    //{
    //    ChangeCameraTarget();
    //}

    public void ChangeStateLight(bool value)
    {
        pointLight.SetActive(value);
    }

    public void Action()
    {
        //if (TargetCamera != null) StartCoroutine(Scenario());
    }

    private IEnumerator Scenario()
    {
        animator.SetBool("IsDead", true);
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        //rigidbody2D.isKinematic = true;

        yield return new WaitForFixedUpdate();

        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(timeToChangeCamera);

        //ChangeCameraTarget();
        gameObject.SetActive(false);
    }

    //private void ChangeCameraTarget()
    //{
    //    Debug.Log("ChangeCameraTarget");
    //    var teammates = SpawnPlayer.spawnPlayer.Teammates;
    //    if (teammates.Count > 0)
    //    {
    //        Transform Player = (Transform)teammates[0].TagObject;
    //        Death atherDeath = Player.GetComponent<Death>();
    //        atherDeath.TargetCamera = TargetCamera;
    //        atherDeath.ChangeStateLight(true);
    //        TargetCamera.SetParent(Player);
    //        TargetCamera.localPosition = Vector3.zero;

    //        ChangeStateLight(false);
    //    }
    //}
}
