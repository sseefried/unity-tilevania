using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    EnemyMovement enemy;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("EnemyHead collision");
        Player player = otherCollider.gameObject.GetComponent<Player>();
        if (player)
        {
            Rigidbody2D r = otherCollider.gameObject.GetComponent<Rigidbody2D>();
            r.velocity = new Vector2(r.velocity.x, 30f); // FIXME: Magic number
            enemy.HandleDeath();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyMovement>();
    }

}
