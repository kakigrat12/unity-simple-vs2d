using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Color particlesColor;

    public void Effect(Vector3 pos, Quaternion rot)
    {
        var effect = Instantiate(particles);
        ParticleSystem.MainModule main = effect.main;
        main.startColor = particlesColor;

        effect.transform.position = pos;
        effect.transform.rotation = rot;
        Destroy(effect.gameObject, 0.8f);
    }
}
