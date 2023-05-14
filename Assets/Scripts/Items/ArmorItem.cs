using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to handle armor items
// Armor items are items that increase the armor of the player
// Armor items can be of different types
// Armor items can be of different values
// Armor Items play a sound when picked up
public class ArmorItem : Item
{
    public string ArmorType = "normal";

    private void OnTriggerEnter(Collider other)
    {
        // if colliding with player
        if (other.transform.tag == "Player")
        {
            // get the player
            GameObject player;
            player = GameObject.Find(other.transform.gameObject.name);

            // if the player can pickup the item
            if (
                player
                    .GetComponent<PlayerStats>()
                    .CanPickupArmorItem(Value, ArmorType)
            )
            {
                // play the pickup sound
                _audioSource.Play();

                if (!ShouldDestroy)
                {
                    // destroy the item
                    ShouldDestroy = true;

                    // increase the armor of the player
                    player
                        .GetComponent<PlayerStats>()
                        .IncreaseArmor(Value, ArmorType);
                }
            }
        }
    }
}
