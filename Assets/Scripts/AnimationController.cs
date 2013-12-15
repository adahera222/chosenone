using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationController : MonoBehaviour {

    public enum AvatarAnimation
    {
        idle,
        walk,
        attack,
        die
    }

    ActorController actorController;
    Animator animator;

    AvatarAnimation currentAnimation = AvatarAnimation.idle;

    void Start() {
        actorController = GetComponent<ActorController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {

    }

    void LateUpdate()
    {
        if (actorController.actor.state == Actor.ActionState.Dead)
        {
            SetAnimation(AvatarAnimation.die);
        }
        else if (actorController.actor.state == Actor.ActionState.TakingAction)
        {
            SetAnimation(AvatarAnimation.attack);
        }
        else if (actorController.isMoving)
        {
            SetAnimation(AvatarAnimation.walk);
        }
        else
        {
            SetAnimation(AvatarAnimation.idle);
        }
    }

    private void SetAnimation(AvatarAnimation anim)
    {
        if (currentAnimation != anim)
        {
            switch (anim)
            {
                //case AvatarAnimation.idle:
                //    break;
                //case AvatarAnimation.walk:
                //    break;
                //case AvatarAnimation.attack:
                //    break;
                case AvatarAnimation.die:
                    animator.SetTrigger("die");

                    break;
                default:
                    animator.SetTrigger(anim.ToString());
                    break;
            }

            currentAnimation = anim;
        }
    }

}