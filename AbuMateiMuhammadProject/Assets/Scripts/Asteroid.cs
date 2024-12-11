using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float speed = 2f;

    // Explosion effect prefab
    public GameObject explosionPrefab;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        // Move the asteroid downwards
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle collision with player
            GameManager.Instance.GameOver();
            Destroy(other.gameObject);
            Explode();
        }
        else if (other.CompareTag("Projectile"))
        {
            // Handle collision with projectile
            Explode();
            Destroy(other.gameObject);
            Destroy(gameObject); 
            GameManager.Instance.UpdateScore(10);
        }
    }

    private void Explode()
    {
        if (explosionPrefab != null)
        {
            // Adjust the explosion position to match the asteroid's position
            Vector3 explosionPosition = transform.position;

            // Set the Z-position to ensure it's aligned with the camera's Z-plane
            explosionPosition.z = 0f;

            // Debug log to verify the explosion's position
            Debug.Log($"Instantiating explosion at position: {explosionPosition}");

            // Instantiate the explosion prefab
            GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);

            // Destroy the explosion object after 2 seconds (optional)
            Destroy(explosion, 2f);
        }
        else
        {
            Debug.LogError("Explosion prefab is not assigned in the Inspector.");
        }
    }

}

