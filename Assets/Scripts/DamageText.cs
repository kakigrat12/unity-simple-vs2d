using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    public Text Text;
    public Vector3 TextWorldPosition;

    [Space]

    [SerializeField] private int screenBoundOffset = 65;

    [HideInInspector] public Camera MainCamera;

    private void Start()
    {
        Destroy(gameObject, 0.4f);
    }

    private void Update()
    {
        Vector2 pos = MainCamera.WorldToScreenPoint(TextWorldPosition);
        pos.x = Mathf.Clamp(pos.x, screenBoundOffset, Screen.width - screenBoundOffset);
        pos.y = Mathf.Clamp(pos.y, screenBoundOffset, Screen.height - screenBoundOffset);
        rectTransform.position = pos;
    }
}
