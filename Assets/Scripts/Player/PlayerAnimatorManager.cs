using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : AnimatorManager
{
    //public Animator animator;
    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;

    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerManager = GetComponent<PlayerManager>();

        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    //public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    //{
    //    animator.SetBool("isInteracting", isInteracting);
    //    animator.CrossFade(targetAnimation, 0.2f);
    //}

    private void OnAnimatorMove()
    {
        if (playerManager.isInteracting == false)
            return;

        if(playerManager.isUsingRootMotion)
        {
            float delta = Time.deltaTime;
            playerLocomotion.playerRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.playerRigidbody.velocity = velocity;
        }        
    }

    public void UpdateAnimator(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        #region Animator Snapping (smooth)
        float snappedHorizontal;
        float snappedVertical;


        //Horizontal
        if(horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if(horizontalMovement> 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }

        //Vertical

        if(verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }


        #endregion

        if(isSprinting && verticalMovement > 0)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);

    }
}
