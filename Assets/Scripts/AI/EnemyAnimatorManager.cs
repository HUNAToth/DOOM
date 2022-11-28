using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    private void Awake()
    {
        anim = GetComponent<Animator>();
        soundScript = GetComponent<EnemySoundScript>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        EnemyAI SelfAI = GetComponent<EnemyAI>();

        if (
            !GameObject
                .Find("GameManager")
                .GetComponent<GameManager>()
                .getIsPause() &&
            !GameObject
                .Find("GameManager")
                .GetComponent<GameManager>()
                .isGameOver
        )
        {
            if (delta != 0)
            {
                SelfAI.enabled = true;
                rb.drag = 0;

                Vector3 deltaPosition = anim.deltaPosition;
                deltaPosition.y = 0;
                Vector3 velocity = deltaPosition / delta;
                rb.velocity = velocity;
            }
        }
        else
        {
            SelfAI.enabled = false;
        }
    }
}
