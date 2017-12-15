using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookieClicker
{


    public class CursorManager : MonoBehaviour
    {
        public float offsetAngle = 90f;
        public float rotationSpeed = 360f;
        public float radius = 5f;
        public float step = 1f;
        public GameObject cursorPrefab;

        [Header("References")]
        public Transform cursorContainer;

        private float angle = 0f;
        private List<GameObject> cursors = new List<GameObject>();

        public static CursorManager Instance = null;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        Vector3 GetNextPosition()
        {
            angle += step * Mathf.Deg2Rad;
            float currAngle = cursorContainer.eulerAngles.z * Mathf.Deg2Rad;
            float nextAngle = angle + currAngle;
            float x = Mathf.Cos(nextAngle) * radius;
            float y = Mathf.Sin(nextAngle) * radius;
            return cursorContainer.position + new Vector3(x, y);
        }

        Quaternion GetRotationFromDir(Vector3 dir)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle += offsetAngle;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // Spawns a new cursor prefab on the right angle
        void SpawnCursor()
        {
            // Get next position for the cursor
            Vector3 pos = GetNextPosition();
            Vector3 dir = cursorContainer.position - pos;

            // Get rotation for new cursor
            Quaternion rot = GetRotationFromDir(dir.normalized);

            // Instantiate a new cursor
            GameObject clone = Instantiate(cursorPrefab, pos, rot);

            // Attach to cursor container
            clone.transform.SetParent(cursorContainer);

            // Add cursor to list
            cursors.Add(clone);
        }

        public void AddCursor()
        {
            SpawnCursor();
        }

        // Update is called once per frame
        void Update()
        {
            cursorContainer.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward);
        }
    }
}
