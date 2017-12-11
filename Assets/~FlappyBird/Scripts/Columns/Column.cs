using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Column : MonoBehaviour
    {


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // Have we collided with the bird?
            if (other.name.StartsWith("Bird")) // or .Contains("Bird")
            {
                // Then the bird scored
                GameManager.Instance.BirdScored();
            }
        }
    }
}
