
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] 
    private SpriteRenderer spriteR;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float enemyHealth;
    [SerializeField]
    private int xpDrop;
    [SerializeField]
    private float pushTime;
    [SerializeField]
    private GameObject destroyEffect;
    private Vector3 direction;
    private float pushCounter;

    // Update is called once per frame
    void Update()
    {
     
    }

    private void FixedUpdate()
    {
        if (PlayerController.Instance.gameObject.activeSelf)
        {
            // if player vertical position is more than enemy
            if (PlayerController.Instance.transform.position.x > transform.position.x)
            {
                spriteR.flipX = true;

            }
            else
            {
                spriteR.flipX = false;
            }
            //knockback
            if (pushCounter > 0)
            {
                pushCounter -= Time.deltaTime;
                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed;
                }
                if (pushCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed);
                }
            }

                // makes enemy walk toward player position
                direction = (PlayerController.Instance.transform.position - transform.position).normalized;
                rb.linearVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }


        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // on collision checks tag then destroys
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);
           
        }
    }
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        DmgNumController.Instance.create(damage,transform.position);
        pushCounter = pushTime;
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
            // creates cloud on enemy destroy
            Instantiate(destroyEffect, transform.position, transform.rotation);
            PlayerController.Instance.GetExperience(xpDrop);
        }
    }
}
