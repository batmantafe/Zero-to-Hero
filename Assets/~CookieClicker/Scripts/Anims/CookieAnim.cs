using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookieClicker
{
    [RequireComponent(typeof(Cookie))]

    public class CookieAnim : MonoBehaviour
    {
        public Animator anim;

        private Cookie cookie;

        // Use this for initialization
        void Start()
        {
            cookie = GetComponent<Cookie>();
            cookie.onClick += OnClick;
        }

        void OnClick(Vector3 point) // detects when Cursor Clicks on Gameobject
        {
            anim.SetTrigger("Click");
        }

        void OnMouseEnter() // detects when Cursor hovers on Gameobject
        {
            anim.SetTrigger("Enter");
        }

        void OnMouseExit()
        {
            anim.SetTrigger("Exit");
        }
    }
}
