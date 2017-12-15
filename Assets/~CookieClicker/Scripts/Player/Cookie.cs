using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookieClicker
{
    public class Cookie : MonoBehaviour
    {
        public int clickModifier = 1;

        public delegate void ClickCallback(Vector3 point);
        public ClickCallback onClick;

        private int clicksToAdd = 1;

        public void Click(Vector3 point)
        {
            // Increment clicks
            GameManager.Instance.AddCookies(clicksToAdd * clickModifier);

            // Are there functions subscribed with onClick?
            if(onClick != null)
            {
                // Call all subscribed functions
                onClick.Invoke(point);
            }
        }
    }
}
