using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinPoints = 1;
    bool isDestroying = false;

    private void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if (!isDestroying)
        {
            Player player = otherCollider.GetComponent<Player>();
            if (!player) { return; }
            Destroy(gameObject);
            isDestroying = true;
            FindObjectOfType<GameSession>().IncreaseScore(coinPoints);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        }
    }
}
