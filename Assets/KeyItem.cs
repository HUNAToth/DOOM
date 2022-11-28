using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public List<GameObject> LocksToOpen;

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
        if (other.transform.tag == "Player")
        {
            foreach (var Lock in LocksToOpen)
            {
                Lock.GetComponent<OpenDoor>().SetKeyCollected(true);
            }

            Destroy (gameObject);
        }
    }
}
