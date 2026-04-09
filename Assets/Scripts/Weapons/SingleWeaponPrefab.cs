using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifetime = 5f;
    public ProjectileWeapon weapon;

    private Vector3 moveDirection;

    void Start()
    {
        // Pull stats from the weapon like AoE system
        weapon = GameObject.Find("Projectile").GetComponent<ProjectileWeapon>();
        speed = weapon.stats[weapon.weaponLevel].speed;
        damage = weapon.stats[weapon.weaponLevel].damage;

        // Destroy after lifetime/range
        Destroy(gameObject, weapon.stats[weapon.weaponLevel].range);

        // Find the nearest enemy to the projectile
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            // Calculate the direction towards the nearest enemy
            moveDirection = (nearestEnemy.transform.position - transform.position).normalized;

            // Rotate the projectile to face the enemy (on spawn)
            RotateProjectile(moveDirection);
        }
        else
        {
            // Fallback: If no enemy found, move forward along the initial direction
            moveDirection = transform.right; // Projectile will move along its local right direction
        }
    }

    void Update()
    {
        
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World); 
    }

    private GameObject FindNearestEnemy()
    {
        // Detects game objects with the "Enemy" tag in 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        // Loops through each enemy in list to find closest
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void RotateProjectile(Vector3 direction)
    {
        // magical angle calculation line 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // sets rotation to angle
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
  
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Deal damage to the enemy
                enemy.TakeDamage(damage);

            
            }
        }
    }
}