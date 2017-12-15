using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookieClicker
{
    [RequireComponent(typeof(Cookie))]

    public class UserInput : MonoBehaviour
    {
        private Cookie cookie;

        // Use this for initialization
        void Start()
        {
            cookie = GetComponent<Cookie>();
        }

        // Only check for mouse down only if mouse is over cookie
        void OnMouseOver()
        {
            // Check for mouse down
            if (Input.GetMouseButtonDown(0))
            {
                // Convert screen to world point
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Click that cookie!
                cookie.Click(point);
            }
        }
    }
}
