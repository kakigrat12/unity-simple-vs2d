using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverToLine : MonoBehaviour
{
    [SerializeField] private Transform firstTransform;
    [SerializeField] private Transform secondTransform;
    [SerializeField] private float time;

    private void Start()
    {
        transform.position = firstTransform.position;
        ToSecondPosition();
    }

    private void ToFirstPosition()
    {
        LeanTween.moveLocal(gameObject, firstTransform.position, time);
        Invoke("ToSecondPosition", time);
    }

    private void ToSecondPosition()
    {
        LeanTween.moveLocal(gameObject, secondTransform.position, time);
        Invoke("ToFirstPosition", time);
    }
}
