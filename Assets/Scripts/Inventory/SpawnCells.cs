using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCells : MonoBehaviour
{
    public void SpawnNewCells(GameObject cellPrefab, Transform container, int count)
    {
        DestroyAllCells(container);

        for (int i = 0; i < count; i++)
        {
            Spawn(cellPrefab, container);
        }
    }

    private void DestroyAllCells(Transform container)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);
    }

    private void Spawn(GameObject prefab, Transform container)
    {
        Instantiate(prefab, container);
    }
}
