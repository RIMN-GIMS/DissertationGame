using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = stats[weaponLevel].cooldown;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (stats[weaponLevel].active && cooldownTimer <= 0)
        {
            cooldownTimer = stats[weaponLevel].cooldown;

            // Spawn projectile at weapon's position and rotation
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }
}
