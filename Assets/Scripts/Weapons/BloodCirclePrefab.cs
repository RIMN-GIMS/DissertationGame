using System.Collections.Generic;
using UnityEngine;

public class BloodCirclePrefab : MonoBehaviour
{
    public BloodCircle weapon;
    private Vector3 targetSize;
    private float timer;
    private float counter;
    public List<Enemy> enemiesInRange;

    void Start()
    {
        // finds weapon parent
        weapon = GameObject.Find("Blood Circle").GetComponent<BloodCircle>();
        // sets weapon size based on level
        targetSize = Vector3.one * weapon.stats[weapon.weaponLevel].range;
        transform.localScale = Vector3.zero;
        timer = weapon.stats[weapon.weaponLevel].duration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale,targetSize,Time.deltaTime * 6);
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            targetSize = Vector3.zero* weapon.stats[weapon.weaponLevel].range;
            if(transform.localScale.x == 0f)
            {
                Destroy(gameObject);
            }
        }

        counter -= Time.deltaTime;
        if (counter <= 0)
        { 
            counter = weapon.stats[weapon.weaponLevel].speed;
            for (int i = 0; i < enemiesInRange.Count; i++) 
            {
                enemiesInRange[i].TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            }
        }
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.GetComponent<Enemy>());
        }
    }
}
