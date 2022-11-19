using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    float x;
    float y;
    float z;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0.1f,0,Space.World);
    }
}
