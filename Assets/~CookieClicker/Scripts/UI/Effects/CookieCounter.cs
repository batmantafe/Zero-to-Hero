using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace CookieClicker
{
    public class CookieCounter : MonoBehaviour
    {
        public float refreshDelay = 0.5f;

        [Header("References")]
        public Text cookieText;
        public Text perSecondText;
        public const string perSecondPrefix = "per second";
        public const string singleCookieSuffix = "Cookie";
        public const string multipleCookiesSuffix = "Cookies";

        private float refreshTimer = 0f;

        void RefreshCookies()
        {
            int totalCookies = GameManager.Instance.GetTotalCookies();
            string suffix = multipleCookiesSuffix;

            if (totalCookies == 1)
            {
                suffix = singleCookieSuffix;
            }

            cookieText.text = totalCookies.ToString("0") + " " + suffix;
        }

        void RefreshPerSecond()
        {
            float perSecond = GameManager.Instance.GetPerSecond();
            perSecondText.text = perSecondPrefix + ": " + perSecond.ToString("0.0");
        }

        // Update is called once per frame
        void Update()
        {
            refreshTimer += Time.deltaTime;

            if (refreshTimer >= refreshDelay)
            {
                refreshTimer = 0f;

                RefreshCookies();
                RefreshPerSecond();
            }
        }
    }
}
