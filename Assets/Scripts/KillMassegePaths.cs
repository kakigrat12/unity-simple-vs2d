using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillMassegePaths : MonoBehaviour
{
    [SerializeField] private Text killer;
    [SerializeField] private Text murdered;

    public void Render(string _killer, string _murdered)
    {
        if (killer != null) killer.text = _killer;
        if (murdered != null) murdered.text = _murdered;
    }
}
