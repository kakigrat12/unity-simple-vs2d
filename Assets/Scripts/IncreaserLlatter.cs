using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaserLlatter : MonoBehaviour
{
    [SerializeField] private float factor;
    [SerializeField] private Vector2 delta;
    private enum kind { first, last }
    [SerializeField] private kind _kind;
    private Transform lastChild;

    private void OnTransformChildrenChanged()
    {
        FindChild();
    }

    private void FindChild()
    {
        Debug.Log("Increase");
        if (transform.childCount == 0) return;

        if (lastChild != null)
        {
            Increase(1 / factor, -delta);
            //lastChild.localScale /= factor;
            //lastChild.GetChild(0).localPosition -= (Vector3)delta;
        }

        int number = 0;
        switch (_kind)
        {
            case kind.first:
                number = 0;
                break;

            case kind.last:
                number = transform.childCount - 1;
                break;
        }

        lastChild = transform.GetChild(number);
        Increase(factor, delta);
        //lastChild.localScale *= factor;
        //lastChild.GetChild(0).localPosition += (Vector3)delta;
    }

    private void Increase(float factor, Vector2 delta)
    {
        lastChild.localScale *= factor;
        lastChild.GetChild(0).localPosition += (Vector3)delta;
    }
}
