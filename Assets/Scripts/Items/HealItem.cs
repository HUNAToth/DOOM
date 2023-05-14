using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GameObject player;
            player = GameObject.Find(other.transform.gameObject.name);
            if (player.GetComponent<PlayerStats>().CanPickupHealthItem(Value))
            {
                _audioSource.Play();
                if (!ShouldDestroy)
                {
                    ShouldDestroy = true;
                    player.GetComponent<PlayerStats>().IncreaseHealth(Value);
                }
            }
        }
    }
}
