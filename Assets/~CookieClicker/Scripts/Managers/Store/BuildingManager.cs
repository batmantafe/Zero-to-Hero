using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using System.IO;

namespace CookieClicker
{
    public class BuildingManager : MonoBehaviour
    {
        public string jsonFile = "buildings.json";
        public GameObject buildingPrefab;

        [Header("References")]
        public Transform buildingContainer;

        private List<Building> buildings = new List<Building>();

        JsonData LoadBuildingData(string file)
        {
            string fullPath = Application.dataPath + "/~CookieClicker/Resources/" + file;
            string jsonString = File.ReadAllText(fullPath);
            return JsonMapper.ToObject<JsonData>(jsonString);
        }

        void CreateBuilding(JsonData data)
        {
            // Instantiate Building
            GameObject clone = Instantiate(buildingPrefab, buildingContainer, false);

            // Get Building script
            Building building = clone.GetComponent<Building>();
            building.data = new BuildingData(data);
            buildings.Add(building); // Add to List

            // Tie building to activity
            Activity activity = CheckForActivity(data);

            // If activity was created for the building's data
            if (activity != null)

                // Bind the building's on clickto activity's increase
                building.onBuy += activity.Increase;
        }

        Activity CheckForActivity(JsonData data)
        {
            // Check if building has activity tied to it
            bool hasActivity = bool.Parse(data["hasActivity"].ToString());

            if (hasActivity)
            {
                // If so, create a new activity object and return it
                return ActivityManager.Instance.CreateActivity(data);
            }

            // Otherwise, return nothing
            return null;
        }

        // Use this for initialization
        void Start()
        {
            JsonData data = LoadBuildingData(jsonFile);

            for (int i = 0; i < data.Count; i++)
            {
                CreateBuilding(data[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
