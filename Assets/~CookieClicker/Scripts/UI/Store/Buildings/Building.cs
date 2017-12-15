using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson; // from Github, free?
using UnityEngine.UI;

namespace CookieClicker
{
    [System.Serializable] // Private Class but visible in Inspector
    public class BuildingData
    {
        public Sprite icon;
        public string name;
        public int cost;
        public int owned;
        public float perSecond;
        public float costModifier;

        // Default Constructor
        public BuildingData()
        {

        }

        // Custom Constructor that takes in JsonData
        public BuildingData(JsonData data)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("buildings");
            int index = int.Parse(data["icon"].ToString());
            icon = sprites[index];
            name = data["name"].ToString();
            cost = int.Parse(data["cost"].ToString());
            owned = int.Parse(data["owned"].ToString());
            perSecond = float.Parse(data["perSecond"].ToString());
            costModifier = float.Parse(data["costModifier"].ToString());
        }
    }

    public class Building : MonoBehaviour
    {
        public BuildingData data = new BuildingData();

        [Header("References")]
        public Button button;
        public Image iconImage;
        public Text nameText;
        public Text costText;
        public Text ownedText;

        public delegate void PurchaseCallback();
        public PurchaseCallback onBuy;

        void Refresh()
        {
            iconImage.sprite = data.icon;
            nameText.text = data.name;
            costText.text = data.cost.ToString();
            ownedText.text = data.owned.ToString();
        }

        // Use this for initialization
        void Start()
        {
            Refresh();
        }

        void OnGUI()
        {
            Refresh();
        }

        public void Buy()
        {
            if (data == null)
            {
                Debug.LogError("Data has not been assigned");
                return;
            }

            // Check if we have sufficient funds for purchase
            int totalCookies = GameManager.Instance.GetTotalCookies();

            if(totalCookies < data.cost)
            {
                Debug.LogWarning("You do not have sufficient funds to purchase a '" + name + "'.");
                return;
            }

            // Cash in the selected building
            GameManager.Instance.RemoveCookies(data.cost);

            // Increase cost by modifier
            data.cost = Mathf.RoundToInt(data.cost + data.costModifier);

            // Increase Owned
            data.owned++;

            // Increase per second
            GameManager.Instance.IncreasePerSecond(data.perSecond);

            // Refresh text
            Refresh();

            // If onBuy has subscribers
            if (onBuy != null)
            {
                // Invoke all subscribers
                onBuy.Invoke();
            }

            // Call OnBuy
            OnBuy();
        }

        // Only runs if we have enough cost
        protected virtual void OnBuy()
        {

        }
    }
}
