using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to manage the animator
// This script is attached to the enemy
// editor variables
// +    anim: the animator component
// +    rb: the rigidbody component
// +    soundScript: the sound script component
public class AnimatorManager : MonoBehaviour
{
    public Animator anim;

    public Rigidbody rb;

    protected EnemySoundScript soundScript;

    // get sound script and rigidbody
    void Awake()
    {
        soundScript = GetComponent<EnemySoundScript>();
        rb = GetComponent<Rigidbody>();
    }

    // play the target animation
    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void PlayWalk()
    {
        //soundScript.PlayDamageSound();
        PlayTargetAnimation("Walk", true);
    }

    public void PlayAttack()
    {
        soundScript.PlayAttackSound();
        PlayTargetAnimation("Attack", true);
    }

    public void PlayDamage()
    {
        soundScript.PlayDamageSound();
        PlayTargetAnimation("Damage01", true);
    }

    public void PlayDeath()
    {
        //   soundScript.PlayDeathSound();
        PlayTargetAnimation("Death01", true);
    }
}
