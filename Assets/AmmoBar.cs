using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace SG
{
    public class AmmoBar : MonoBehaviour
    {

        [SerializeField] private GunInventory gunInventory;
        [SerializeField] TextMeshProUGUI ammoStat;
        private GunScript gunScript;

        private void Update(){
            gunScript = gunInventory.currentGun.GetComponent<GunScript>();
            ammoStat.text = gunScript.bulletsIHave.ToString() + "/" + gunScript.bulletsInTheGun.ToString();
        }
    }
}