using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private SpriteRenderer sprite;
    public Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioClip jumpSound;
    private bool doubleJump;
    public float FacingDirection => sprite.flipX ? -1f : 1f;

    private float dirX = 0f;
    [SerializeField] public float moveSpeed = 4.5f;
    [SerializeField] public float jumpForce = 9f;
    [SerializeField] private TrailRenderer tr;
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 10f;
    public float dashingTime = 0.4f;
    private float dashingCooldown = 1f;
    
    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    //Movement
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (rb == null || coll == null || sprite == null || anim == null)
        {
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (dirX * moveSpeed, rb.velocity.y);


        if (IsGrounded() && !Input.GetKey(KeyCode.W))
        {
            doubleJump = false;
        }
        
        if (Input.GetKeyDown(KeyCode.W) )
        {
            if ( IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(0, jumpForce);
                doubleJump = !doubleJump;
            }
        }


        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        //Animation 
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;

        }

        if ( rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
            //SoundManager.instance.PlaySound(jumpSound);
        }
        else if (rb.velocity.y< -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        if (coll == null)
        {
            return false;
        }

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    //Dash
    private IEnumerator Dash()
    {
        if (rb == null || sprite == null || anim == null)
        {
            yield break;
        }

        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        float dashDirection = Mathf.Sign(sprite.flipX ? -1 : 1);
        rb.velocity = new Vector2(dashDirection * dashingPower, 0f);
        anim.SetBool("roll", true);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        anim.SetBool("roll", false);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    public bool canAttack()
    {
        return dirX == 0 && IsGrounded();
    }
}
