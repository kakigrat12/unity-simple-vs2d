using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCameras : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondCamera;
    [SerializeField] private RenderTexture tex;

    private void Awake()
    {
        //mainCamera = Camera.main;
        //secondCamera = GetComponent<Camera>();
        //tex.width = mainCamera.pixelWidth;
        //tex.height = mainCamera.pixelHeight;
        //Debug.Log(mainCamera.scaledPixelWidth);
        //Debug.Log(mainCamera.scaledPixelHeight);
        //tex.width = mainCamera.scaledPixelWidth;
        //tex.height = mainCamera.scaledPixelHeight;

        Debug.Log(mainCamera.scaledPixelWidth);
        Debug.Log(mainCamera.scaledPixelHeight);
        tex.width = mainCamera.scaledPixelWidth;
        tex.height = mainCamera.scaledPixelHeight;
        secondCamera.orthographicSize = mainCamera.orthographicSize;
        secondCamera.Render();
    }

    //private void Update()
    //{
    //    Debug.Log(mainCamera.scaledPixelWidth);
    //    Debug.Log(mainCamera.scaledPixelHeight);
    //    tex.width = mainCamera.scaledPixelWidth;
    //    tex.height = mainCamera.scaledPixelHeight;
    //    //tex.MarkRestoreExpected();
    //    secondCamera.orthographicSize = mainCamera.orthographicSize;
    //    secondCamera.Render();

    //    //Debug.Log(mainCamera.orthographicSize);
    //    //secondCamera.orthographicSize = mainCamera.orthographicSize;
    //    //mainCamera.
    //    //tex = mainCamera.targetTexture;
    //    //secondCamera.fieldOfView = mainCamera.fieldOfView;
    //    //secondCamera.sensorSize = mainCamera.sensorSize;
    //    //secondCamera.rect = mainCamera.rect;
    //    //mainCamera.
    //}
}
