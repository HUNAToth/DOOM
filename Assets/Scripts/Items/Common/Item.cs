using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Value = 10;

    public AudioSource _audioSource;

    public bool ShouldDestroy = false;

    public float destroyTimer = 0.3f;

    /*TODO
        Awake()-ben behúzni a komponensben megadott timerértéket editorban
    */
    // Update is called once per frame
    void Update()
    {
        // if the item should be destroyed
        if (ShouldDestroy)
        {
            if (destroyTimer > 0)
            // if the timer is not done then decrease the timers
            {
                destroyTimer -= Time.deltaTime;
            }
            else
            // shound delay is done then destroy the object
            {
                Destroy (gameObject);
            }
        }
    }
}
