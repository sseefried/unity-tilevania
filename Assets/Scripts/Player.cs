using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Config")]
    [Range(0f, 10f)] [SerializeField] float runSpeed = 3f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    bool isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRigidBody.velocity = new Vector2(0f, 0f); ;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal") * runSpeed; // value between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        if (Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon)
        {
            myAnimator.SetBool("running", true);
        } else
        {
            myAnimator.SetBool("running", false);
        }

    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            Vector2 s = transform.localScale;
            transform.localScale = new Vector2(Mathf.Abs(s.x) * Mathf.Sign(myRigidBody.velocity.x), s.y);
            
        }
    }
}
