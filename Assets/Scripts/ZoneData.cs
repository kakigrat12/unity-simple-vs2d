using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZoneData
{
    public int Damage;
    public float IncreaseSpeed;
    public float ExpectationTime;
    public GameObject[] Boxes;
    [HideInInspector] public Vector2[] StartScale;
    [HideInInspector] public Vector3[] StartPosition;
}
