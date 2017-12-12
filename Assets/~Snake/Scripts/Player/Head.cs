using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq; // need this for Last method in RefreshTail function

namespace Snake
{
    public class Head : MonoBehaviour
    {
        public float moveRate = 0.3f; // Movement Interval
        public float sprintRate = 0.1f; // Sprint Speed
        public float keyDownDuration = 1f; // How long does a key have to be donwn before sprinting?
        public GameObject tailPrefab; // Prefab of tail to spawn
        public Vector2 direction = Vector2.right; // Movement direction of snake

        private float keyDownTimer = 0f; // How long has any key been pressed?
        private float moveTimer = 0f; // Timer to keep track of elapsed time
        private float interval = 0f; // Store the move rate / sprint rate
        private bool hasEaten = false; // Has the Snake eaten?
        private List<Transform> tail = new List<Transform>(); // List to keep track of tails

        public void Sprint()
        {
            // Check for Sprint
            if (Input.anyKey)
            {
                // Count how long a key is Down for
                keyDownTimer += Time.deltaTime;

                // If key has been down for a set time (duration)
                if (keyDownTimer >= keyDownDuration)
                {
                    // Snake is now running
                    interval = sprintRate;
                }
            }
        }

        public void Walk()
        {
            // Reset the key down timer
            keyDownTimer = 0f;

            // Reset the move speed
            interval = moveRate;
        }

        void AppendTail(Vector3 gapPos)
        {
            // Load prefab into the world
            GameObject clone = Instantiate(tailPrefab, gapPos, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, clone.transform);

            // Reset the flag (the Snake is now NEVER satisfied)
            hasEaten = false;
        }

        void RefreshTail(Vector3 gapPos)
        {
            // Do we have a tail?
            if (tail.Count > 0) // Count for LISTs, Length for ARRAYs
            {
                // Move the last Tail element to where the Head's old position was
                tail.Last().position = gapPos;

                // Add to front of list, remove from the back
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }

        void Move()
        {
            // Save current position
            Vector2 gapPos = transform.position;

            // Move Head into the new direction
            transform.Translate(direction);

            // Has the Snake eaten something?
            if (hasEaten)
            {
                // Append size of the tail
                AppendTail(gapPos);
            }

            else
            {
                // Refresh the Tail location
                RefreshTail(gapPos);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // If collided with Food
            if (other.name.Contains("Food"))
            {
                // Get longer in next Move call
                hasEaten = true;

                // Remove the food
                Destroy(other.gameObject);

                // Tell GameManager to spawn things
                GameManager.Instance.Spawn();

                // Tell GameManager we scored!
                GameManager.Instance.AddScore(1);
            }

            else
            {
                // Game Over
                GameManager.Instance.ResetGame();
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Count up the timer
            moveTimer += Time.deltaTime;

            // Is it time to move?
            if (moveTimer > interval)
            {
                Move(); // Move the Snake
                moveTimer = 0f; // Reset the timer
            }
        }
    }
}
