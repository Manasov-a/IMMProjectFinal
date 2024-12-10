using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject); // Destroy the asteroid
            Destroy(gameObject); // Destroy the projectile
            GameManager.Instance.UpdateScore(10);
        }
    }
}
