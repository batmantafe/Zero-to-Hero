using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using LitJson;

namespace CookieClicker
{
    [System.Serializable]
    public class ActivityData
    {
        public Sprite icon;
        public Sprite background;

        public ActivityData(JsonData data)
        {
            string iconPath = data["activityIcon"].ToString();
            string backgroundPath = data["activityBackground"].ToString();
            icon = Resources.Load<Sprite>(iconPath);
            background = Resources.Load<Sprite>(backgroundPath);
        }
    }

    public class Activity : MonoBehaviour
    {
        public GameObject imagePrefab;
        public ActivityData data;

        [Header("References")]
        public Transform background;
        public Transform countContainer;

        private Image[] images;

        // Use this for initialization
        void Start()
        {
            images = background.GetComponentsInChildren<Image>();
            Refresh();
        }

        void Refresh()
        {
            // Loop through images in background
            for (int i = 0; i < images.Length; i++)
            {
                // Set all sprites to icon
                images[i].sprite = data.background;
            }
        }
        
        public void Increase()
        {
            // Activate activity
            gameObject.SetActive(true);

            // Spawn new image
            GameObject clone = Instantiate(imagePrefab, countContainer, false);

            // Change the image for the clone
            Image image = clone.GetComponent<Image>();
            image.sprite = data.icon;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
