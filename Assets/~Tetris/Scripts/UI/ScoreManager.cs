using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace Tetris
{
    public class ScoreManager : MonoBehaviour
    {
        public Text scoreText;
        public int score;
        public int value;

        // Use this for initialization
        void Start()
        {
            Grid.onRowsCleared += OnRowsClear;
        }

        // Gets called everytime a row gets cleared
        void OnRowsClear(int rows)
        {
            score += value * rows;
            scoreText.text = "score: " + score.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
