using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PhotonView))]
//[RequireComponent(typeof(Health))]
public class HitBoxes : MonoBehaviour
{
    public PhotonView PhotonHealthSyn;
    public Collider2D[] Colliders;
    [SerializeField] private float[] factors;

    private Bullet lastBullet;

    public float Factor(Collider2D collider, Bullet bullet)
    {
        if(bullet != lastBullet)
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
                if (Colliders[i] == collider)
                {
                    return factors[i];
                }
            }
        }
        
        return 0f;
    }
}
