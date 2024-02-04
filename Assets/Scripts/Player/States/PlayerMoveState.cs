using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Current state: Move State");
        player.TogglePlayerHUD("show");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        _FirstPersonController controller = player.movementController;
        controller.JumpAndGravity();
        controller.GroundedCheck();
        controller.Move();

        CameraDetector detector = player.detector;

        if (detector.detected)
        {
            IExaminable examinable = detector.hit.collider.GetComponent<IExaminable>();
            if (examinable != null)
            {
                if (player.input.interact)
                {
                    player.TogglePlayerHUD("hide");
                    player.CurrentObject = detector.hit.collider.gameObject;
                    player.ChangeState(player.examineState);
                }
                
            }
        }

        player.abilityManager.ActivateAbility();
        
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {
        
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider other)
    {
        
    }

    public override void LateUpdateState(PlayerStateManager player)
    {
        player.movementController.CameraRotation();
    }
}
