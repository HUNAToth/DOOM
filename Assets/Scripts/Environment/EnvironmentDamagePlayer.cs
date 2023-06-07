using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to damage the player when they are in the environment
// This elements like fire, lava, etc.
// This script is attached to the environment object
// editor variables
// +    DamageValue: the amount of damage the player takes
// +    TimerMax: the amount of time between damage
public class EnvironmentDamagePlayer : MonoBehaviour
{
    public int DamageValue = 5;

    public int EnemyDamageValue = 0;

    float Timer = 0f;

    public float TimerMax = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        string otherTag = other.transform.tag;
        if (otherTag == "Player" || otherTag == "Dummie")
        {
            if (Timer <= 0)
            {
                if (otherTag == "Player")
                {
                    other.GetComponent<PlayerStats>().TakeDamage(DamageValue);
                }
                else if (otherTag == "Dummie" && EnemyDamageValue > 0)
                {
                    Debug.Log("DummiDamage_" + EnemyDamageValue);
                    other
                        .GetComponent<EnemyStats>()
                        .TakeDamage(EnemyDamageValue);
                }
                Timer = TimerMax;
            }
            else
            {
                Timer -= Time.deltaTime;
            }
        }
    }
}
