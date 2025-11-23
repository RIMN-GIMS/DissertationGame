
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // makes global for use in other scripts
    public static PlayerController Instance;

    private float inputx;
    private float inputy;
    private bool isImmune;
    public Vector3 playerMoveDirection;
    public float playerMaxHealth;
    public float playerCurHealth;
    [Header("Adjustables")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animatorP;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float ImmunityDuration;
    [SerializeField]
    private float ImmunityTimer;

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
    private void Start()
    {
        playerCurHealth = playerMaxHealth;
        UIController.Instance.UpdatePHealthUI();
    }

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
        
        if(ImmunityTimer > 0)
        {
            ImmunityTimer -= Time.deltaTime;
        }
        else
        {
            isImmune = false;
        }
 
    }
    private void FixedUpdate() //Physics update
    {
        rb.linearVelocity = new Vector2(playerMoveDirection.x * moveSpeed, playerMoveDirection.y * moveSpeed);
    }

    public void TakeDamage(float damage)
    {
        if (isImmune == false)
        {
            isImmune = true;
            ImmunityTimer = ImmunityDuration;

            playerCurHealth -= damage;
            UIController.Instance.UpdatePHealthUI();
            if (playerCurHealth <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
        }
    }
}
