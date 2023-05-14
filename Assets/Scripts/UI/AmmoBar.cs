using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField]
    private GunInventory gunInventory;

    [SerializeField]
    TextMeshProUGUI ammoStat;

    private GunScript gunScript;

    private void Update()
    {
        if (gunInventory.currentGun != null)
        {
            gunScript = gunInventory.currentGun.GetComponent<GunScript>();

            ammoStat.text =
                gunScript.bulletsIHave.ToString() +
                "/" +
                gunScript.bulletsInTheGun.ToString();
        }
        else
        {
            ammoStat.text = "/";
        }
    }
}
