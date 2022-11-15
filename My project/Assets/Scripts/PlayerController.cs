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

    public Rigidbody2D _rigidbody2D;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();        
        sRender = GetComponent<SpriteRenderer>();
        jumpableGround = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        MovePlayer();
        



        if (Input.GetButton("Jump") && IsGrounded() == true)
        {
            Jump();
        }

    }

    void fixedUpdate()
    {
        spriteDirection();
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalInput * playerSpeed, _rigidbody2D.velocity.y);
    }
      
    private void spriteDirection()
    {
        if(_rigidbody2D.velocity.x < 0)
        {
            sRender.flipX = true;
        }
        if (_rigidbody2D.velocity.x > 0)
        {
            sRender.flipX = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Jump()
    {    
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpPower);       
    }
}
