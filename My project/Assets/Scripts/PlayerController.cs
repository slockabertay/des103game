using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float playerSpeed = 10.0f;
    private float jumpPower = 10f;
    private Collider2D coll;
    private LayerMask jumpableGround;
    private ContactFilter2D ContactFilter;
    private SpriteRenderer sRender;
    private enum State { idle, running, jumping, falling }
    private State state = State.idle;
    //following https://youtu.be/fmU2tJ0Mnq4 Unity 2018 - Platformer Tutorial 15: Finite State Machine by Alvin Roe I made the
    //state machine but coded the parameters for each state myself

    public Rigidbody2D rb;
    public Animator anim;
    public int playerHealth;
    public GameObject gameoverScreenUI;
    public GameObject deathBox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        jumpableGround = LayerMask.GetMask("Ground");
        sRender = gameObject.GetComponent<SpriteRenderer>();
        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {

        IsGrounded();
        MovePlayer();
        AnimControl();
        spriteDirection();
        anim.SetInteger("state", (int)state);

        if (Input.GetButton("Jump") && IsGrounded() == true)
        {
            Jump();
        }
                

        if (playerHealth == 0)
        {
            Death();
        }

    }




    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);
        //key A or D for left or right
    }

    private void spriteDirection()
    {
        if (rb.velocity.x < 0)
        {
            sRender.flipX = true;
        }
        if (rb.velocity.x > 0)
        {
            sRender.flipX = false;
        }

        //checks movement direction to flip sprite
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        //checks the player is touching or reasonably close to any 'ground' or object layer tagged as ground
    }


    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    private void AnimControl()
    {
        if (rb.velocity.y > 0)
        {
            state = State.jumping;
        }
        else if (rb.velocity.y < 0)
        {
            state = State.falling;
        }
        else if (rb.velocity.x * rb.velocity.x > 0)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Death()
    {
        gameoverScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            playerHealth = 0;
        }
    }
}
