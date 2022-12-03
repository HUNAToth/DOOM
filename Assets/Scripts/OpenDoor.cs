using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (
            ((isActivateOnce && !FirstActivationDone) || !isActivateOnce) &&
            deactivationTimer <= 0 &&
            isActive
        )
        {
            DoorAnim.SetTrigger("IsDeactivated");
            deactivationTimer = Mathf.Infinity;
            isActive = false;

            if (!isAutoTriggered)
            {
                SwitchOff.SetActive(true);
                SwitchOn.SetActive(false);
            }
        }
        if (deactivationTimer >= 0)
        {
            deactivationTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (
            !isActive &&
            ((isActivateOnce && !FirstActivationDone) || !isActivateOnce)
        )
        {
            if (
                (Input.GetKey(KeyCode.E) || isAutoTriggered) &&
                (!isKeyRequired || (isKeyRequired && isKeyCollected))
            )
            {
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

    public void SetKeyCollected(bool keyValue)
    {
        isKeyCollected = keyValue;
    }
}
