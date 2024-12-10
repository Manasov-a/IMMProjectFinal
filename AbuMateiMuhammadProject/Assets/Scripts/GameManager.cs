using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Level & Background Settings")]
    public List<Sprite> levelBackgrounds;  // Assign 10 background sprites in the Inspector
    public SpriteRenderer backgroundRenderer;
    private int currentLevel = 1;
    private int maxLevels = 10; // Total number of levels

    [Header("Asteroid Settings")]
    public List<GameObject> asteroidPrefabs; // Assign multiple asteroid prefabs in the Inspector (e.g., 5)
    private float spawnRate = 1.5f;   // Base time between asteroid spawns
    private float asteroidSpeed = 2f; // Base asteroid speed

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    [Header("Game State")]
    public bool isGameActive = false;
    private int score = 0;

    // Score needed per level before moving on to the next
    private int scorePerLevel = 100;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Start the game at difficulty 1
        StartGame(1);
    }

    public void StartGame(int difficulty)
    {
        currentLevel = 1;
        isGameActive = true;
        score = 0;

        // Set initial difficulty based on the difficulty parameter
        spawnRate = 1.5f / difficulty;
        asteroidSpeed = 2f * difficulty;

        UpdateScore(0); // Update UI initially
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // Set the initial background
        if (backgroundRenderer != null && levelBackgrounds.Count > 0)
        {
            backgroundRenderer.sprite = levelBackgrounds[0];
        }

        // Start spawning asteroids
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);

            float randomX = Random.Range(-12f, 12f);
            Vector3 spawnPos = new Vector3(randomX, 13f, 0f);

            // Spawn a random asteroid prefab from the list
            int index = Random.Range(0, asteroidPrefabs.Count);
            GameObject asteroid = Instantiate(asteroidPrefabs[index], spawnPos, Quaternion.identity);

            Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
            if (asteroidScript != null)
            {
                asteroidScript.SetSpeed(asteroidSpeed);
            }

            // Check if score reached threshold for next level
            if (score >= currentLevel * scorePerLevel && currentLevel < maxLevels)
            {
                NextLevel();
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void NextLevel()
    {
        currentLevel++;

        // Change background if we have more backgrounds available
        if (currentLevel <= levelBackgrounds.Count)
        {
            backgroundRenderer.sprite = levelBackgrounds[currentLevel - 1];
        }
        else
        {
            // If we run out of new backgrounds, keep the last one
            backgroundRenderer.sprite = levelBackgrounds[levelBackgrounds.Count - 1];
        }

        // Increase difficulty each new level
        spawnRate *= 0.8f;    // Spawn faster
        asteroidSpeed *= 1.2f; // Asteroids move faster
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
