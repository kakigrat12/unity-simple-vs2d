using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWorldToUI : MonoBehaviour
{
    [SerializeField] private GameObject uiElement;

    private void Start()
    {
        StartCoroutine(damageTextShow(transform.position));
    }

    private IEnumerator damageTextShow(Vector3 collisionPosition)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            uiElement.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(collisionPosition);
        }
    }
}
