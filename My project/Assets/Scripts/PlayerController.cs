using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float playerSpeed = 10.0f;
    private float jumpPower = 12.5f;
    private Collider2D coll;
    private LayerMask jumpableGround; 
    private SpriteRenderer sRender;
    private enum State { idle, running, jumping, falling }
    private State state = State.idle;
    //following https://youtu.be/fmU2tJ0Mnq4 Unity 2018 - Platformer Tutorial 15: Finite State Machine by Alvin Roe I made the
    //state machine but coded the parameters for each state myself

    public Rigidbody2D rb;
    public Animator anim;
    public static int playerHealth;
    public GameObject gameoverScreenUI;
    public GameObject deathBox;
    private bool onPlatform = false;
    public static bool gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        jumpableGround = LayerMask.GetMask("Ground");
        sRender = gameObject.GetComponent<SpriteRenderer>();
        playerHealth = 3;
        gameOverScreen = false;
    }

    // Update is called once per frame
    void Update()
    {        
        IsGrounded();
        MovePlayer();

        if(onPlatform)
        {
            PlatformAnimControl();
        }
        else
        {
            AnimControl();
        }        

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
        if (onPlatform == true)
        {
            rb.velocity = new Vector2(horizontalInput * playerSpeed + MovingPlatform.velocity.x, rb.velocity.y + MovingPlatform.velocity.y);        
        }
        else 
        {
            rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);
        }
       
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
        //checks movment to change states
    }

    private void PlatformAnimControl()
    {
        if (rb.velocity.y - MovingPlatform.velocity.y > 0)
        {
            state = State.jumping;
        }
        else if (rb.velocity.y - MovingPlatform.velocity.y < 0)
        {
            state = State.falling;
        }
        else if ((rb.velocity.x - MovingPlatform.velocity.x) * (rb.velocity.x - MovingPlatform.velocity.x) > 0)
        {
            state = State.running;
        }
        else if (rb.velocity.x == MovingPlatform.velocity.x)
        {
            state = State.idle;
        }
        //because of added velocities I had to create another state controller taking the platforms velocity into account
    }

    private void Death()
    {
        gameoverScreenUI.SetActive(true);
        Time.timeScale = 0f;
        gameOverScreen = true;
        //death screen
    }

    public IEnumerator Flash()
    {
        sRender.color = Color.red;
        yield return new WaitForSeconds(.01f);
        sRender.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            playerHealth = 0;
            //checks for collission with invisible box at the bottom of the screen to kill the player
        }              

        if (collision.gameObject.CompareTag("Health"))
        {
            if (playerHealth < 3)
            {
                playerHealth++;
                //player health up 
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            onPlatform = true;           
        }
        else
        {
            onPlatform = false;
        }    

        //checks if the player is touching the moving platforms
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            playerHealth--;            
            //player health down
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            StartCoroutine(Flash());
        }
    }
}
