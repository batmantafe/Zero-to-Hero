using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;

namespace CookieClicker
{
    public class ActivityManager : MonoBehaviour
    {
        public GameObject activityPrefab;

        [Header("References")]
        public Transform activityContainer;

        private List<Activity> activities = new List<Activity>();

        public static ActivityManager Instance = null;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public Activity CreateActivity(JsonData data)
        {
            // Instantiate a new Activity
            GameObject clone = Instantiate(activityPrefab, activityContainer, false);
            clone.SetActive(false); // Deactivate object
            Activity activity = clone.GetComponent<Activity>(); // Get activity
            activity.data = new ActivityData(data); // Apply new data to activity
            activities.Add(activity); // Add the activity to list
            return activity; // Return the activity created
        }
    }
}
