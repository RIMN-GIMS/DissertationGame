
using NUnit.Framework;
using System.Collections.Generic;
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
    public int experience;
    public int currentLevel;
    public int maxLevel;
    [SerializeField]
    public List<Weapon> activeWeapons;
    public List<int> playerLevels;
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
        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.1f + 15));
        }
        playerCurHealth = playerMaxHealth;
        UIController.Instance.UpdatePHealthUI();
        UIController.Instance.UpdateXPUI();
    }

    void Update()
    {
        //2D movement using the Unity default thing
        inputx = Input.GetAxisRaw("Horizontal");
        inputy = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3(inputx, inputy).normalized;
        animatorP.SetFloat("MoveX", inputx);
        animatorP.SetFloat("MoveY", inputy);
        if (playerMoveDirection == Vector3.zero)
        {
            animatorP.SetBool("Idle", true);
        }
        else
        {
            animatorP.SetBool("Idle", false);
        }

        if (ImmunityTimer > 0)
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
    public void GetExperience(int amount)
    {
        experience += amount;
        UIController.Instance.UpdateXPUI();
        if (experience >= playerLevels[currentLevel])
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        // maintains XP between levels
        experience -= playerLevels[currentLevel];
       // increments level
        currentLevel++;
        // updates UI
        UIController.Instance.UpdateXPUI();
        UIController.Instance.LeveUpMenuO();
        //Defines buttons on menu appearance 
        if (activeWeapons[0].stats[0].active == true)
        {
            UIController.Instance.levelUpButtons[0].activatebutton(activeWeapons[0]);
        }
        if (activeWeapons[1].stats[1].active == true)
        {
            UIController.Instance.levelUpButtons[1].activatebutton(activeWeapons[1]);
        }
        if (activeWeapons[2].stats[2].active == true)
        {
            UIController.Instance.levelUpButtons[2].activatebutton(activeWeapons[2]);
        }
    }
}
