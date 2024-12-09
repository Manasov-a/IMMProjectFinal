using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f; // Lifetime of the projectile before disappearing

    void Start()
    {
        // Destroy the projectile after a certain time
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid")) // Check if it hit an asteroid
        {
            // Destroy both the asteroid and the projectile
            Destroy(other.gameObject);
            Destroy(gameObject);

            // Optionally, update the score
            GameManager.Instance.UpdateScore(10); // Assumes GameManager handles the score
        }
    }
}
