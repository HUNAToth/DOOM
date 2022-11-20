using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject DoorToOpen;

    // Start is called before the first frame update
    public float MoveSpeed = 4.0f;

    public bool TriggeredOnce = false;

    public Animator DoorAnim;

    public float deactivationTime;

    private float deactivationTimer = Mathf.Infinity;

    public bool IsActive = false;

    private bool FirstActivationDone = false;

    public GameObject SwitchOn;

    public GameObject SwitchOff;

    public bool isKeyRequired = false;

    public bool isKeyCollected = false;

    public bool isAutoTriggered = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (
            ((TriggeredOnce && !FirstActivationDone) || !TriggeredOnce) &&
            deactivationTimer <= 0 &&
            IsActive
        )
        {
            DoorAnim.SetTrigger("IsDeactivated");
            deactivationTimer = Mathf.Infinity;
            IsActive = false;

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
            !IsActive &&
            ((TriggeredOnce && !FirstActivationDone) || !TriggeredOnce)
        )
        {
            if (
                (Input.GetKey(KeyCode.E) || isAutoTriggered) &&
                (!isKeyRequired || (isKeyRequired && isKeyCollected))
            )
            {
                DoorAnim.SetTrigger("IsActivated");
                deactivationTimer = deactivationTime;
                IsActive = true;
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
