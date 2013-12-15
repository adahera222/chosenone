using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationController : MonoBehaviour {

    // ================================================================================
    //  declarations
    // --------------------------------------------------------------------------------

    public enum AvatarAnimation
    {
        idle,
        walk,
        attack,
        die
    }

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public GameObject displayObject;

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private ActorController actorController;
    private Animator animator;

    private AvatarAnimation currentAnimation = AvatarAnimation.idle;

    private FadingTimer _fadeOutTimer = null;

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------

    void Start() {
        actorController = GetComponent<ActorController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (_fadeOutTimer != null)
        {
            _fadeOutTimer.Update();
            ApplyFadeOut();

            if (_fadeOutTimer.hasEnded)
            {
                SetMaterialColor(new Color(1.0f, 1.0f, 1.0f, 0f));
            }
        }
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

    // sets color for all sprite children
    public void SetMaterialColor(Color color)
    {
        var list = displayObject.GetComponentsInChildren<Renderer>();
        foreach (var item in list)
        {
            item.material.color = color;
        }
    }

    public void FadeOut()
    {
        if (_fadeOutTimer == null)
        {
            _fadeOutTimer = new FadingTimer(0, 2.0f, 0.4f);
        }
    }

    public void Reset()
    {
        animator.SetTrigger("idle");
        currentAnimation = AvatarAnimation.idle;
        _fadeOutTimer = null;
        SetMaterialColor(new Color(1.0f, 1.0f, 1.0f, 1.0f));
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

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

    private void ApplyFadeOut()
    {
        Color color = new Color(1.0f, 1.0f, 1.0f, _fadeOutTimer.progress);
        SetMaterialColor(color);
    }
}