using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier;

    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;

    private float speedMilestoneCount;
    private float speedMilestoneCountStore;

    public float jumpForce;
    public bool grounded;
    public float groundCheckRadius;

    public int coin = 0;
    public TextMeshProUGUI coinText;

    private bool isRunning = false;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Rigidbody2D rb;
    private Animator anim;
    public GameManager theGameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        coin = 0;

        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
    }

    private void Update()
    {
        if (!isRunning)
            return;

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }

        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
 

        if (SwipeController.swipeUp)
        {
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }       
        }
        if (SwipeController.swipeDown)
        {
            StartSliding();
            Invoke("StopSliding", 0.5f);
        }

        anim.SetFloat("Speed", rb.velocity.x);
        anim.SetBool("Grounded", grounded);
    }

    private void StartSliding()
    {
        anim.SetBool("Slided", true);
    }

    private void StopSliding()
    {
        anim.SetBool("Slided", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {           
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin += 1;
            coinText.text = coin.ToString();
        }
    }

    public void StartRunning()
    {
        transform.localScale = Vector3.one;
        isRunning = true;
    }
}
