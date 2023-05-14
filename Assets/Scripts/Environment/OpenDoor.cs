using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to open doors
// This script is attached to the switch object
// editor variables
// +    DoorToOpen: the door object that will be opened
// +    SwitchOn: the switch object that will be turned on
// +    SwitchOff: the switch object that will be turned off
// +    MoveSpeed: the speed at which the door will open
// +    isActivateOnce: if true, the switch can only be activated once
// +    deactivationTime: the amount of time before the door closes
// +    isKeyRequired: if true, the player must have the key to open the door
// +    isKeyCollected: if true, the player has the key
// +    isAutoTriggered: if true, the door will open automatically
//
public class OpenDoor : MonoBehaviour
{
    //GameObjects
    public GameObject DoorToOpen;

    public Animator DoorAnim;

    public GameObject SwitchOn;

    public GameObject SwitchOff;

    //Move
    public float MoveSpeed = 4.0f;

    //MoveLogic
    public bool isActivateOnce = false;

    public bool isActive = false;

    public float deactivationTime;

    //KeyLogic
    public bool isKeyRequired = false;

    public bool isKeyCollected = false;

    public bool isAutoTriggered = false;

    //PrivateLogic
    private bool FirstActivationDone = false;

    private float deactivationTimer = Mathf.Infinity;

    void Update()
    {
        // Move the door up if it is active and not at the top yet
        // and not auto triggered and
        // not key required or key required and key collected
        // and not activate once or activate once and first activation
        if (
            ((isActivateOnce && !FirstActivationDone) || !isActivateOnce) &&
            deactivationTimer <= 0 &&
            isActive
        )
        {
            // Move the door down
            // Set the deactivation timer to default value
            // Set the door to not active
            // If not auto triggered, set the switch on and off
            DoorAnim.SetTrigger("IsDeactivated");
            deactivationTimer = Mathf.Infinity;
            isActive = false;

            if (!isAutoTriggered)
            {
                SwitchOff.SetActive(true);
                SwitchOn.SetActive(false);
            }
        }

        // Deactivation timer decrement
        if (deactivationTimer >= 0)
        {
            deactivationTimer -= Time.deltaTime;
        }
    }

    // If the player is in the trigger zone
    private void OnTriggerStay(Collider other)
    {
        // If the player presses E and the door is not active and
        // not activate once or activate once and first activation
        if (
            !isActive &&
            ((isActivateOnce && !FirstActivationDone) || !isActivateOnce)
        )
        {
            // If the player presses E or the door is auto triggered
            // and not key required or key required and key collected
            if (
                (Input.GetKey(KeyCode.E) || isAutoTriggered) &&
                (!isKeyRequired || (isKeyRequired && isKeyCollected))
            )
            {
                // Move the door up
                // Set the deactivation timer
                // Set the door to active
                // Set the first activation done to true
                // If not auto triggered, set the switch on and off
                DoorAnim.SetTrigger("IsActivated");
                deactivationTimer = deactivationTime;
                isActive = true;
                FirstActivationDone = true;
                if (!isAutoTriggered)
                {
                    SwitchOff.SetActive(false);
                    SwitchOn.SetActive(true);
                }
            }
        }
    }

    // Collect the key and set the key collected to true
    public void SetKeyCollected(bool keyValue)
    {
        isKeyCollected = keyValue;
    }
}
