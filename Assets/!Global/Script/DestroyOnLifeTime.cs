using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLifeTime : MonoBehaviour
{
    public float lifeTime = 1f;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
