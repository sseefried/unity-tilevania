using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float deathWaitInSeconds = 0.5f;
    [SerializeField] float deathBounceVelocity = 30f;
    Animator myAnimator;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCollider;
    BoxCollider2D myFeetCollider;
    Collider2D headCollider;
    

    float direction = 1f; // either 1 or -1f; 1 is left, -1 is right

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            if (c.tag == "MushroomHead")
            {
                headCollider = c;
            }
        }
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

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player player = otherCollider.gameObject.GetComponent<Player>();
        if (!player) { return; }
        Rigidbody2D r = otherCollider.gameObject.GetComponent<Rigidbody2D>();        
        if (!r) { return; } // defensive

        if (myCollider.IsTouching(otherCollider))
        {
            player.HandleDeath();
            headCollider.enabled = false;
        }

        if (headCollider.IsTouching(otherCollider))
        {
            HandleDeath(player);
        }
    }

    private void HandleDeath(Player player)
    {
        DisableColliders();
        FindObjectOfType<Spawner>().MushroomKilled();
        myRigidBody.velocity = new Vector2(0f, 0f);
        player.BounceOnKill(deathBounceVelocity);
        myAnimator.SetBool("dying", true);
        myCollider.enabled = false;
        StartCoroutine(Die());
    }

    public IEnumerator Die()
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
