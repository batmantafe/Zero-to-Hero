using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class Spawner : MonoBehaviour
    {
        public float spawnRate = 4f;
        public float startTime = 3f;
        public GameObject foodPrefab; // Food prefab to spawn

        [Header("References")]
        public Transform borderTop;
        public Transform borderBottom;
        public Transform borderLeft;
        public Transform borderRight;

        // Use this for initialization
        void Start()
        {
            //InvokeRepeating("Spawn", startTime, spawnRate);

            Subscribe();
        }

        void OnDestroy()
        {
            Unsubscribe();
        }

        public void Subscribe()
        {
            // Subscribe function to this function call
            GameManager.Instance.onSpawn += Spawn;
        }

        public void Unsubscribe()
        {
            // Subscribe function to this function call
            GameManager.Instance.onSpawn -= Spawn;
        }

        void Spawn()
        {
            // Get coordinates of borders
            float left = borderLeft.position.x;
            float right = borderRight.position.x;
            float bottom = borderBottom.position.y;
            float top = borderTop.position.y;

            // Get random x and y coordinates
            int x = (int)Random.Range(left + 1, right - 1);
            int y = (int)Random.Range(top - 1, bottom + 1);

            // Spawn food at this point
            Instantiate(foodPrefab,
                        new Vector2(x, y),
                        Quaternion.identity);
        }
    }
}
