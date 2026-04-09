using UnityEngine;

public class BloodCircle : Weapon
{
   
    [SerializeField]
    private GameObject prefab;
    private float spawnCounter;
    void Update()
    {
        // if timer 0 spawn weapon
        spawnCounter -= Time.deltaTime;
        if( spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].cooldown;
            Instantiate(prefab,transform.position,transform.rotation);
        }
    }
}
