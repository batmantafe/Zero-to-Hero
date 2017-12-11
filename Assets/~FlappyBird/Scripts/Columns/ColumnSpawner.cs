using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class ColumnSpawner : MonoBehaviour
    {
        public GameObject columnPrefab; // The column prefab we want to spawn
        public int columnPoolSize = 5; // How many columns to keep on standby
        public float spawnRate = 3f; // How quickly each columns spawn
        public float columnMin = -1f; // Minimum y value of the column position
        public float columnMax = 3.5f; // Maximum y value of the column position

        public Vector3 standbyPos = new Vector3(-15, -25); // Holding position for the unused columns offscreen
        public float spawnXPos = 10f;

        private GameObject[] columns; // Collection of pooled columns
        private int currentColumn = 0; // Index of the currnt column in the collection
        private float spawnTimer = 0f;

        // Use this for initialization
        void Start()
        {
            // Initialise column pool
            columns = new GameObject[columnPoolSize];

            // Loop through the collection...
            for (int i = 0; i < columnPoolSize; i++)
            {
                // ... and create the individual columns
                columns[i] = Instantiate(columnPrefab, standbyPos, Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Count up the Timer
            spawnTimer += Time.deltaTime;

            // Is game not over AND
            // spawnTimer reached spawnRate?
            if (GameManager.Instance.gameOver == false &&
                spawnTimer >= spawnRate)
            {
                // Reset timer
                spawnTimer = 0f;

                // Spawn the column (i.e. re-use a column)
                SpawnColumn();
            }

        }

        void SpawnColumn()
        {
            // Set a random y spawn position for the column
            float spawnYPos = Random.Range(columnMin, columnMax);

            // Get current column
            GameObject column = columns[currentColumn];

            // Set position of current column
            column.transform.position = new Vector2(spawnXPos, spawnYPos);

            // Increase value of current column
            currentColumn++; // '++' increments value by 1

            // If the new currentColumn reaches the end of the array
            if (currentColumn >= columnPoolSize)
            {
                // ... set it back to zero
                currentColumn = 0;
            }

        }
    }
}
