using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextView : MonoBehaviour
{
    //public static DamageTextView current;

    [SerializeField] private DamageText damageTextPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private Gradient gradient;

    private Camera mainCamera;

    private void Awake()
    {
        //current = this;
        GameEvents.current.onHealthChanged += HealthChanged;

        mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        GameEvents.current.onHealthChanged -= HealthChanged;
    }

    private void HealthChanged(Health health)
    {
        if (health.lastDamagePlayer == PhotonNetwork.LocalPlayer && health.lastDamage > 0) CreateDamageText(health.lastDamage, health.Value, health.MaxValue, health.lastDamagePosition); // && health.lastDamagePhoton.TryGetComponent(out Bullet t)
    }

    private void CreateDamageText(int damage, int health, int maxHealth, Vector3 pos)
    {
        var damageText = Instantiate(damageTextPrefab, container);
        damageText.Text.text = damage.ToString();
        damageText.Text.color = gradient.Evaluate((float)health / maxHealth);

        damageText.TextWorldPosition = pos;
        damageText.MainCamera = mainCamera;
    }
}
