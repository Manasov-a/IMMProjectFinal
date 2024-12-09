using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI gameOverText;
    public Button restartButton; 
    public GameObject titleScreen;

    private int score; 
    private int currentLevel = 1; 
    private int enemiesPerWave = 5;
    private float enemySpeedMultiplier = 1.0f;
    private float spawnRate = 1.5f; 
    private bool isGameActive;
    internal static object Instance;

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);

        titleScreen.SetActive(false);
        StartLevel(currentLevel);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartLevel(int level)
    {
        currentLevel = level;
        enemiesPerWave = 5 * currentLevel;
        enemySpeedMultiplier = 1.0f * currentLevel;
        spawnRate = Mathf.Max(0.5f, 1.5f / currentLevel);

        Debug.Log($"Starting Level {currentLevel}: Enemies = {enemiesPerWave}, Speed Multiplier = {enemySpeedMultiplier}");
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            if (!isGameActive) yield break;

            yield return new WaitForSeconds(spawnRate);

            // Spawn an enemy at a random position
            int enemyIndex = Random.Range(0, enemyPrefabs.Count);
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 6f, 0f);

            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetSpeed(enemySpeedMultiplier); 
        }

        // After wave, wait a moment and go to next level
        yield return new WaitForSeconds(3f);
        if (isGameActive)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        StartLevel(currentLevel + 1);
    }
}
