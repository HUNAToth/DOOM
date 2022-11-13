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

    public Animator anim;

    public float deactivationTime;

    private float deactivationTimer = Mathf.Infinity;

    public bool IsActive = false;

    private bool FirstActivationDone = false;

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
            anim.SetTrigger("IsDeactivated");
            deactivationTimer = Mathf.Infinity;
            IsActive = false;
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
            if (Input.GetKey(KeyCode.E))
            {
                anim.SetTrigger("IsActivated");
                deactivationTimer = deactivationTime;
                IsActive = true;
                FirstActivationDone = true;
            }
        }
    }
}
