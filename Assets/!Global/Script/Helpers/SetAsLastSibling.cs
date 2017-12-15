using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsLastSibling : MonoBehaviour
{

    void OnDrawGizmos()
    {
        transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetAsLastSibling();
    }
}
