using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlood : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private void Start()
    {
        GameEvents.current.onHealthChanged += HealthChanged;
    }

    private void OnDisable()
    {
        GameEvents.current.onHealthChanged -= HealthChanged;
    }

    private void HealthChanged(Health health)
    {
        Debug.Log("PlayerBlood");
        Effect(health.lastDamagePosition, health.lastDamageNormal);
    }

    private void Effect(Vector3 pos, Vector2 normal)
    {
        var effect = Instantiate(particles);

        effect.transform.position = pos;
        effect.transform.rotation = Quaternion.FromToRotation(Vector2.left, normal);
        Destroy(effect.gameObject, 0.8f);
    }
}
