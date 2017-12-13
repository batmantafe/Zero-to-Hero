using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] groups;
        public int nextIndex = 0;

        // Spawns the next random group element
        public void SpawnNext()
        {
            // Spawn the random group
            Instantiate(groups[nextIndex], transform.position, Quaternion.identity);

            // Get next random index
            nextIndex = Random.Range(0, groups.Length);

            // Remove any empty parents
            RemoveEmptyParents();
        }

        void RemoveEmptyParents()
        {
            // Check for any parents without children
            Group[] groups = GameObject.FindObjectsOfType<Group>();
            foreach (Group g in groups)
            {
                if (g.transform.childCount == 0)
                {
                    Destroy(g.gameObject);
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            // Spawn the initial group
            SpawnNext();
        }
    }
}
