using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

namespace Snake
{
    public class GameManager : MonoBehaviour
    {
        // Keep record of the score
        public int currentLevel = 0;
        public int score = 0;

        [Header("UI")]
        public Text scoreText;

        public static GameManager Instance = null;
        public delegate void SpawnCallBack();
        public SpawnCallBack onSpawn;

        public delegate void ScoreCallBack(int score);
        public ScoreCallBack onScoreAdded;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            // Subscribe to scene changes
            SceneManager.sceneLoaded += OnSceneWasLoaded; // Call here
        }
        
        // Access this function to spawn whatever subscibed to spawn callback
        public void Spawn()
        {
            // If there are subscribed functions
            if (onSpawn != null)
            {
                // Invoke them
                onSpawn.Invoke();
            }
        }

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score.ToString(); // Update Score Text when Score changes

            // Are functions subscribed to onScoreAdded?
            if (onScoreAdded != null)
            {
                // Invoke all subscribed
                onScoreAdded.Invoke(score);
            }
        }

        public void ResetGame()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        public void LoadNextLevel()
        {
            // Increment next level
            currentLevel++;

            // Check if next scene is valid
            if (currentLevel >= SceneManager.sceneCountInBuildSettings)
            {
                // Load MainMenu otherwise
                currentLevel = 0;
            }
                    
            // Loads next level
            SceneManager.LoadScene(currentLevel);
        }

        // Check what scene we're in
        public void OnSceneWasLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MainMenu")
            {
                currentLevel = 0;
            }
        }
    }
}
