using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Config")]
    [Range(0f, 10f)] [SerializeField] float runSpeed = 3f;
    [Range(1f, 30f)] [SerializeField] float jumpSpeed = 5f;
    [Range(0f, 0.5f)] [SerializeField] float climbSpeed = 0.1f;
    [Range(0f, 2f)] [SerializeField] float timeScale = 1f;
    [SerializeField] float deathWaitInSeconds = 2f;

    bool dying = false;
    float myEpsilon = 1e-5f;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;
    float startingGravity;
    bool isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        startingGravity = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
        if (!dying)
        {
            Run();
            Jump();
            Climb();
            FlipSprite();
        }
    }

    public void HandleDeath()
    {
        DisableColliders();
        myRigidBody.gravityScale = 0f;
        myRigidBody.velocity = new Vector2(0f, 0f);
        myAnimator.SetBool("dying", true);
        dying = true;
        StartCoroutine(Die());
    }

    public void BounceOnKill(float killBounceVelocity)
    {
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, killBounceVelocity);
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal") * runSpeed;
        Vector2 playerVelocity = new Vector2(controlThrow, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        // Set animation state based 
        if (HasNoVerticalVelocity() && IsTouchingGround())
        {
            if (PlayerHasHorizontalSpeed())
            {
                SetRunning();
            }
            else
            {
                SetIdle();
            }
        }
    }

    private void Jump()
    {
        if (!IsTouchingGround()) { return; }
        
        // if on ground and not in the process of jumping
        if (myAnimator.GetBool("jumping") && HasNoVerticalVelocity()) {
            SetIdle();
        }

        if (Input.GetButtonDown("Jump"))
        {
            SetJumping();
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }

    }

    private void Climb()
    {
        if (!IsTouchingLadder()) {
            myAnimator.SetBool("climbing", false);
            myRigidBody.gravityScale = startingGravity;
            return;
        }
        
        if (Input.GetButton("Vertical"))
        {
            SetClimbing();
            myRigidBody.gravityScale = 0f;
            myRigidBody.velocity = new Vector2(0f, 0f);
            float controlThrow = Input.GetAxis("Vertical");
            myRigidBody.position =
                new Vector2(myRigidBody.position.x,
                            myRigidBody.position.y + controlThrow * climbSpeed);
        }
    }

    private void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed())
        {
            Vector2 s = transform.localScale;
            transform.localScale = new Vector2(Mathf.Abs(s.x) * Mathf.Sign(myRigidBody.velocity.x), s.y);
            
        }
    }

    private bool PlayerHasHorizontalSpeed()
    {
        return Mathf.Abs(myRigidBody.velocity.x) > myEpsilon; 
    }

    private bool IsTouchingGround()
    {
        return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsTouchingLadder()
    {
        return myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
    }

    private bool HasNoVerticalVelocity()
    {
        return Mathf.Abs(myRigidBody.velocity.y) < myEpsilon;
    }

    private void SetIdle()
    {
        myAnimator.SetBool("running", false);
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("climbing", false);

    }
    private void SetRunning()
    {
        myAnimator.SetBool("running", true);
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("climbing", false);
    }
    private void SetJumping()
    {
        myAnimator.SetBool("running", false);
        myAnimator.SetBool("jumping", true);
        myAnimator.SetBool("climbing", false);
    }

    private void SetClimbing()
    {
        myAnimator.SetBool("running", false);
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("climbing", true);

    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(deathWaitInSeconds);
        Destroy(gameObject);
    }

    private void DisableColliders()
    {
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = false;
        }
    }

}
