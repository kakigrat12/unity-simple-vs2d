using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThingPanelRender : MonoBehaviour
{
    public static ThingPanelRender current;

    //[SerializeField] private Transform container;
    private RectTransform panel; //[SerializeField] 

    //private object[] Data;
    private Coroutine coroutine;

    //[SerializeField] private RectTransform rectTransform;
    //[SerializeField] private Image icon;
    //[SerializeField] private Text name;
    //[SerializeField] private Text number;

    //private Camera mainCamera;

    private void Awake()
    {
        current = this;
    }

    //private void Start()
    //{
    //    mainCamera = Camera.main;
    //    rectTransform.gameObject.SetActive(false);
    //}

    //public void Render(Vector2 worldPosition, bool activeValue, Sprite inventoryIcon, string thingName, int thingNumber)
    //{
    //    Debug.Log("PANELTHINGRENDER");
    //    Debug.Log(activeValue);
    //    rectTransform.gameObject.SetActive(activeValue);
    //    rectTransform.position = mainCamera.WorldToScreenPoint(worldPosition);

    //    icon.sprite = inventoryIcon;
    //    name.text = thingName;
    //    number.text = thingNumber.ToString();
    //}

    public void Render(ThingPanelInfo prefab, object[] data)
    {
        if (panel != null) Stop();

        //Data = data;
        var thingPanel = Instantiate(prefab, transform);
        thingPanel.Data = data;
        panel = thingPanel.RectTransform;
        coroutine = StartCoroutine(PanelChangePosition());
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
        Destroy(panel.gameObject);
    }

    private IEnumerator PanelChangePosition()
    {
        while (true)
        {
            panel.position = Input.mousePosition;
            yield return null;
        }
    }
}
