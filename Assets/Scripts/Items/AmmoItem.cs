using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : Item
{
    public string AmmoType = "normal";

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GameObject player;
            player = GameObject.Find(other.transform.gameObject.name);
            if (
                player
                    .GetComponent<GunInventory>()
                    .CanPickupAmmoItem(Value, AmmoType)
            )
            {
                _audioSource.Play();
                if (!ShouldDestroy)
                {
                    ShouldDestroy = true;
                    player
                        .GetComponent<GunInventory>()
                        .IncreaseAmmo(Value, AmmoType);
                }
            }
        }
    }
}
