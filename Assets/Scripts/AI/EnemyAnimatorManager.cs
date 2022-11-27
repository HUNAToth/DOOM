using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyManager enemyManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyManager = GetComponent<EnemyManager>();
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
                enemyManager.enemyRigidbody.drag = 0;
                Vector3 deltaPosition = anim.deltaPosition;
                deltaPosition.y = 0;
                Vector3 velocity = deltaPosition / delta;
                enemyManager.enemyRigidbody.velocity = velocity;
            }
        }
        else
        {
            SelfAI.enabled = false;
        }
    }
}
