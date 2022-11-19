using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
public class AmmoItem : MonoBehaviour
{
    public int PointsRestored = 10;
    public string AmmoType = "normal";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
       if (other.transform.tag == "Player"){
                    GameObject player;
                    player = GameObject.Find(other.transform.gameObject.name);
                    if( player.GetComponent<GunInventory>().CanPickupAmmoItem(PointsRestored,AmmoType)){
                        player.GetComponent<GunInventory>().IncreaseAmmo(PointsRestored,AmmoType);
                        Destroy (gameObject);
                    }
                }
    }
}
}