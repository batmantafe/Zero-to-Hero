using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace Tetris
{
    public class Grid : MonoBehaviour
    {
        public UnityEvent onGameOver;
        public static Grid instance = null;
        public static int width = 10, height = 20;
        public static Transform[,] grid = new Transform[width, height];

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        void OnDrawGizmos()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.DrawWireCube(new Vector2(x, y), Vector2.one);
                }
            }
        }

        public delegate void RowsClearedCallback(int rows);
        public static RowsClearedCallback onRowsCleared;

        public static void GameOver()
        {
            // If there are functions subscribed
            if (instance.onGameOver != null)
            {
                // Invoke all subscribed functions
                instance.onGameOver.Invoke();
            }
        }

        public static Vector2 RoundVec2(Vector2 v)
        {
            float roundX = Mathf.Round(v.x);
            float roundY = Mathf.Round(v.y);
            return new Vector2(roundX, roundY);
        }

        public static bool InsideBorder(Vector2 pos)
        {
            // Truncate the Vector
            int x = (int)pos.x;
            int y = (int)pos.y;

            if(x >= 0 && x < width &&
                y >= 0)
            {
                // Inside border
                return true;
            }

            // Outside border
            return false;
        }

        // Deletes a row with a given y coordinate
        public static void DeleteRow(int y)
        {
            // Loop through the row using x - width
            for (int x = 0; x < width; x++)
            {
                // Destroy each element
                Destroy(grid[x, y].gameObject);

                // Return each grid element back to null
                grid[x, y] = null;
            }
        }

        // Shifts the row in the y coordinate down one space
        public static void DecreaseRow(int y)
        {
            // Loop through entire column
            for (int x = 0; x < width; x++)
            {
                // Check if index is not null
                if (grid[x, y] != null)
                {
                    // Move one towards bottom
                    grid[x, y - 1] = grid[x, y]; // Set grid element to one above
                    grid[x, y] = null;

                    // Update block position
                    grid[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }

        // Shifts rows above from given y
        public static void DecreaseRowsAbove(int y)
        {
            // Loop through each row
            for (int i = y; i < height; i++)
            {
                // Decrease each row
                DecreaseRow(i);
            }
        }

        // Check if we have a full row
        public static bool IsRowFull(int y)
        {
            // Loop through each column
            for (int x = 0; x < width; x++)
            {
                // If cell is empty
                if (grid[x, y] == null)
                    return false;
            }

            // The row is full!
            return true;
        }

        // Delete the full rows
        public static int DeleteFullRows()
        {
            int rows = 0;

            for (int y = 0; y < height; y++)
            {
                // Is the row full?
                if (IsRowFull(y))
                {
                    // Add row to count
                    rows++;
                    
                    // delete entire row
                    DeleteRow(y);

                    // Decrease the rows above
                    DecreaseRowsAbove(y + 1);

                    // Decrease current y coordinate
                    // (so we don't skip the next row)
                    y--;
                }
            }

            // If there are rows that were cleared AND
            // Functions are subscribed to onRowsCleared
            if (rows > 0 && onRowsCleared != null)
            {
                // Invoke all the subscribed functions
                onRowsCleared.Invoke(rows);
            }

            // return counted rows
            return rows;
        }
    }
}
