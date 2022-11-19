using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public int x = 0;

    public int y = 1;

    public int z = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x, y, z, Space.World);
    }
}
