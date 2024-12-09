using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 2.0f; // Base speed
    private float speedMultiplier = 1.0f; // Default multiplier

    void Update()
    {
        transform.Translate(Vector3.down * speed * speedMultiplier * Time.deltaTime);

        // Destroy if off-screen
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
