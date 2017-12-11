using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {
        public bool gameOver = false;
        public float scrollSpeed = -1.5f;
        public int score = 0;

        public static GameManager Instance = null;

        public delegate void ScoreAddedCallback(int score);
        public ScoreAddedCallback scoreAdded;

        // Use this for initialization
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void BirdScored()
        {
            // The bird cant score if there is a game over
            if(gameOver)
            {
                // Exit the function
                return;
            }

            // Increase the score
            score++;

            // If there is a function subscribed
            if (scoreAdded != null)
            {
                // Call an event to state that a score has been added
                scoreAdded.Invoke(score);
            }
        }

        public void BirdDied()
        {
            // Set game over to true
            gameOver = true;
        }
    }
}