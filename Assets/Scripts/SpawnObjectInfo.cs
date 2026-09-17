using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnObjectInfo
{
    public GameObject SpawnObject;
    [Range(0, 100)]
    public float Population;
}
