using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheackInvis : MonoBehaviour
{
    public UnityEvent Visible;
    public UnityEvent Invisible;

    private void OnBecameVisible()
    {
        Visible?.Invoke();
    }

    private void OnBecameInvisible()
    {
        Invisible?.Invoke();
    }
}
