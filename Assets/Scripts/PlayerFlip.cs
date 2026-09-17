using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    private bool isRight = true;

    private void Update()
    {
        if (weapon.rotation.eulerAngles.z < 270 && weapon.rotation.eulerAngles.z > 90)
        {
            if (isRight)
            {
                Flip();
            }
        }
        else
        {
            if (!isRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        transform.localScale *= new Vector2(-1f, 1f);
        isRight = !isRight;
    }
}
