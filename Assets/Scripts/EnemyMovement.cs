using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Animator myAnimator;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCollider;
    BoxCollider2D myFeetCollider;
    BoxCollider2D headCollider;
    bool dying = false;
    

    float direction = 1f; // either 1 or -1f; 1 is left, -1 is right


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        headCollider = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed * -direction, 0f);
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        direction *= -1;
        transform.localScale = new Vector2(direction, transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dying && collision.gameObject.GetComponent<Player>())
        {                               
            Destroy(collision.gameObject); // destroy player
        }
    }

    public void HandleDeath()
    {
        dying = true;
        StartCoroutine(Death());
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f); // FIXME: magic number
        Destroy(gameObject);
    }
}
