using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private float deadZone = 0.5f;
    //private Camera mainCamera;
    private Vector3 offer;
    private bool isActive = true;

    //private void Awake()
    //{
    //    Screen.SetResolution(460, 280, true);
    //}

    private void Start()
    {
        GameEvents.current.onIsAnyPanelOpened += ChangeIsActive;
        //mainCamera = Camera.main;
        offer = new Vector3(0f, 0f, -10f);
        //
    }

    private void OnDisable()
    {
        GameEvents.current.onIsAnyPanelOpened -= ChangeIsActive;
    }

    private void ChangeIsActive(bool newValue)
    {
        isActive = !newValue;
    }


    private void FixedUpdate()
    {
        //transform.position = target.position + offer + Input.mousePosition / 1000f;
        Vector3 positionTo = target.position + offer;

        positionTo.x -= Mathf.Clamp(positionTo.x - transform.position.x, -deadZone, deadZone);
        //if(Mathf.Abs(target.position.x - transform.position.x) < deadZone)
        //{
        //    positionTo += transform.position;
        //}
        //else
        //{
        //    positionTo += target.position;
        //}

        if (Input.GetMouseButton(1) && isActive)
        {
            positionTo += (Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2) / Screen.width;
        }
        Vector3 move = Vector3.Lerp(transform.position, positionTo, speed * Time.fixedDeltaTime);
        move = new Vector3(Mathf.Round(move.x * 100) / 100, Mathf.Round(move.y * 100) / 100, -10);
        transform.position = move;


        //Screen.SetResolution(460, 280, true);
        //UnityEngine.U2D.PixelPerfectRendering.pixelSnapSpacing = 0.1f;
        //Camera.main.pixelRect = new Rect(new Vector2Int(0, 0), new Vector2Int(480, 280));
        //Camera.main.sensorSize = new Vector2Int(0, 0);
        //Screen.SetResolution(460, 280, true);
        //Camera.main.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);

        //Camera.main.fieldOfView = 1f;
        //Screen.fullScreen = true;

        //0.001006036
        //0.9979879
    }

    //public void ChangeTarget(Transform newTarget)
}
