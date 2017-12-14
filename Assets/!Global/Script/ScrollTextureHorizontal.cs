using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScrollTextureHorizontal : MonoBehaviour
{
    public float scrollSpeed = 1.5f;

    private Renderer rend;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = rend.material.mainTextureOffset;
        offset.x += scrollSpeed * Time.deltaTime;
        offset.x = Mathf.Clamp(offset.x, -1, 1);
        rend.material.mainTextureOffset = offset;
    }
}
