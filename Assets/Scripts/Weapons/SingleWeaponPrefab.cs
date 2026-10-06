using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{

    public ProjectileWeapon weapon;

    private Vector3 moveDirection;

    void Start()
    {
        weapon = GameObject.Find("Projectile").GetComponent<ProjectileWeapon>();

       // detroys self after range
        Destroy(gameObject, weapon.stats[weapon.weaponLevel].range);

        // Find the nearest enemy to the projectile
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            // Calculate the direction towards the nearest enemy
            moveDirection = (nearestEnemy.transform.position - transform.position).normalized;
     
            RotateProjectile(moveDirection);
        }
        else
        {
           // if no enemy just sends it right
            moveDirection = transform.right; 
        }
    }  

    void Update()
    {
        // moves weapon in direction at appropriate speed 
        transform.Translate(moveDirection * weapon.stats[weapon.weaponLevel].speed * Time.deltaTime, Space.World); 
    }

    private GameObject FindNearestEnemy()
    {
        // Detects game objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = 10f;

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
               
                enemy.TakeDamage(weapon.stats[weapon.weaponLevel].damage);

            
            }
        }
    }
}