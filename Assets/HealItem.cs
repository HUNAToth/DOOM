using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
public class HealItem : MonoBehaviour
{
    public int PointsRestored = 10;
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
                    player.GetComponent<PlayerStats>().IncreaseHealth(PointsRestored);
                    Destroy (gameObject);
                }
    }
}
}