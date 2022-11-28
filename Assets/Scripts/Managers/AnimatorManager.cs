using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator anim;

    public Rigidbody rb;

    protected EnemySoundScript soundScript;

    void Awake()
    {
        soundScript = GetComponent<EnemySoundScript>();
        rb = GetComponent<Rigidbody>();
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void PlayWalk()
    {
        //soundScript.PlayDamageSound();
        PlayTargetAnimation("Character_Walk", true);
    }

    public void PlayAttack()
    {
        soundScript.PlayAttackSound();
        PlayTargetAnimation("Attack", false);
    }

    public void PlayDamage()
    {
        soundScript.PlayDamageSound();
        PlayTargetAnimation("Damage01", false);
    }

    public void PlayDeath()
    {
        //   soundScript.PlayDeathSound();
        PlayTargetAnimation("Death01", false);
    }
}
