using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private ZoneData[] zonesDatas;
    [SerializeField] private ZoneDamage zoneDamage;
    private int number = -1;

    // 200 из-за пикселей

    private void Awake()
    {
        Vector2 zeroScale = new Vector2(0f, 1f);
        foreach (var data in zonesDatas)
        {
            data.StartScale = new Vector2[data.Boxes.Length];
            data.StartPosition = new Vector3[data.Boxes.Length];
            for (int i = 0; i < data.Boxes.Length; i++)
            {
                data.StartScale[i] = data.Boxes[i].transform.localScale;
                data.StartPosition[i] = data.Boxes[i].transform.localPosition;
                data.Boxes[i].transform.localScale *= zeroScale;
                data.Boxes[i].transform.localPosition -= data.Boxes[i].transform.localRotation * ((Vector3)data.StartScale[i] - data.Boxes[i].transform.localScale) / 200;
                data.Boxes[i].SetActive(false);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(StateChanger());
    }

    private void FixedUpdate()
    {
        if (number >= 0)
        {
            var data = zonesDatas[number];
            for (int i = 0; i < data.Boxes.Length; i++)
            {
                data.Boxes[i].transform.localScale = Vector2.MoveTowards(data.Boxes[i].transform.localScale, data.StartScale[i], Time.fixedDeltaTime * 200 * data.IncreaseSpeed);
                data.Boxes[i].transform.localPosition = Vector3.MoveTowards(data.Boxes[i].transform.localPosition, data.StartPosition[i], Time.fixedDeltaTime * 1 * data.IncreaseSpeed);
            }
        }
    }

    private IEnumerator StateChanger()
    {
        for (int i = 0; i < zonesDatas.Length; i++)
        {
            //number = -1;
            yield return new WaitForSeconds(zonesDatas[i].ExpectationTime);
            var data = zonesDatas[i];
            zoneDamage.SetDamage(data.Damage);
            foreach (var s in data.Boxes)
            {
                s.SetActive(true);
            }
            number = i;
            //yield return new WaitForSeconds(increaseTime);
        }
    }
}
