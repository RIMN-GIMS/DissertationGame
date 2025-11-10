using System.Collections;
using System.Collections.Generic;
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
    private GameObject destroyEffect;
    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
     
    }

    private void FixedUpdate()
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
        // makes enemy walk toward player position
        direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // on collision checks tag then destroys
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            // creates cloud on enemy destroy
            Instantiate(destroyEffect,transform.position,transform.rotation);
        }
    }
}
