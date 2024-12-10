using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    private float xMin, xMax;

    void Start()
    {
        Camera mainCamera = Camera.main;
        xMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0)).x;
        xMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0)).x;
    }

    void Update()
    {
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right arrow keys or A/D
        Vector3 movement = Vector3.right * horizontal * speed * Time.deltaTime;
        transform.Translate(movement);
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * projectileSpeed;
    }
}
