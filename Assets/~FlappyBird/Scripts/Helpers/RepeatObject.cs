using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RepeatObject : MonoBehaviour
    {
        public float padding = 0.01f;

        private BoxCollider2D box;
        private float width;

        // Use this for initialization
        void Start()
        {
            // Get boxcollider component
            box = GetComponent<BoxCollider2D>();
            // Store size of collider along horizontal axis
            width = box.size.x * transform.localScale.x;
        }

        // Update is called once per frame
        void Update()
        {
            // Store the position in a smaller variable
            Vector3 pos = transform.position;
            // Is the position on the x outside of the camera?
            if(pos.x < -width)
            {
                // Repeat the object
                Repeat();
            }
        }

        void Repeat()
        {
            // Offset of the ground to be placed outside the screen
            Vector3 groundOffset = new Vector3((width - padding) * 2, 0);
            transform.position += groundOffset;
        }
    }
}
