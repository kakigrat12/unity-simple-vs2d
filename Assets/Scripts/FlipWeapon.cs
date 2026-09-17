using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWeapon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform weapon;
    [SerializeField] private bool flipX;
    [SerializeField] private bool flipY;
    //private enum type { X, Y };
    //[SerializeField] private type _type;

    private void Update()
    {
        if (spriteRenderer == null) return;

        if (weapon.rotation.eulerAngles.z < 270 && weapon.rotation.eulerAngles.z > 90)
        {
            Flip(true);
            //if (!spriteRenderer.flipY) 
            //{
            //    spriteRenderer.flipY = true; 
            //}
        }
        else //if (spriteRenderer.flipY)
        {
            //spriteRenderer.flipY = false;
            Flip(false);
        }
    }

    private void Flip(bool isFlip)
    {
        //switch (_type)
        //{
        //    case
        //        type.X:
        //        if (isFlip != spriteRenderer.flipX)
        //        {
        //            spriteRenderer.flipX = isFlip;
        //        }
        //        break;

        //    case
        //        type.Y:
        //        if (isFlip != spriteRenderer.flipY)
        //        {
        //            spriteRenderer.flipY = isFlip;
        //        }
        //        break;
        //}

        if (flipX)
        {
            if (isFlip != spriteRenderer.flipX)
            {
                spriteRenderer.flipX = isFlip;
            }
        }

        if (flipY)
        {
            if (isFlip != spriteRenderer.flipY)
            {
                spriteRenderer.flipY = isFlip;
            }
        }
    }

    //public void ChangeSpriteRenderer(Thing thing)
    //{
    //    weapon = thing.transform;
    //    spriteRenderer = thing.SpriteRenderer;
    //}
}
