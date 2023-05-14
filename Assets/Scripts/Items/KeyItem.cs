using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyItem : MonoBehaviour
{
    public List<GameObject> LocksToOpen;

    public string keyType = "Blue";

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
            switch (keyType)
            {
                case "Blue":
                    GameObject
                        .Find("Key_Blue_UI")
                        .GetComponent<Image>()
                        .enabled = true;
                    break;
                case "Yellow":
                    GameObject
                        .Find("Key_Yellow_UI")
                        .GetComponent<Image>()
                        .enabled = true;
                    break;
                case "Red":
                    GameObject
                        .Find("Key_Red_UI")
                        .GetComponent<Image>()
                        .enabled = true;
                    break;
                default:
                    break;
            }

            Destroy (gameObject);
        }
    }
}
