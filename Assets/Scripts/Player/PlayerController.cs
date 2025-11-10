using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // makes global for use in other scripts
    public static PlayerController Instance;

    private float inputx;
    private float inputy;
    public Vector3 playerMoveDirection;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animatorP;
    [SerializeField]
    private float moveSpeed;

    private void Awake()
    {
        // for scene transition
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //2D movement using the Unity default thing
        inputx = Input.GetAxisRaw("Horizontal");
        inputy = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3(inputx, inputy).normalized;
        animatorP.SetFloat("MoveX", inputx);
        animatorP.SetFloat("MoveY",inputy);
        if (playerMoveDirection == Vector3.zero)
        {
            animatorP.SetBool("Idle", true);
        }
        else
        {
            animatorP.SetBool("Idle", false);
        }
        
 
    }
    private void FixedUpdate() //Physics update
    {
        rb.velocity = new Vector2(playerMoveDirection.x * moveSpeed, playerMoveDirection.y * moveSpeed);
    }
}
