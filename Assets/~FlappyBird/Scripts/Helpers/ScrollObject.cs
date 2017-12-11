using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ScrollObject : MonoBehaviour
    {
        private Rigidbody2D rigid;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            rigid.velocity = new Vector2(GameManager.Instance.scrollSpeed, 0);
        }

        // Update is called once per frame
        void Update()
        {
            // Check if game is over
            if (GameManager.Instance.gameOver)
            {
                // Cancel velocity to stop scrolling
                rigid.velocity = Vector2.zero;
            }
        }
    }
}
