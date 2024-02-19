using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player State: Idle State");
        player.TogglePlayerHUD("hide");
        
        player.triggerVoiceLine.TriggerVO();
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        
    }

    public override void LateUpdateState(PlayerStateManager player)
    {
        player.movementController.CameraRotation();
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {
        
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider other)
    {
        
    }

    public override void UpdateState(PlayerStateManager player)
    {
        SwitchToMove(player);
    }

    void SwitchToMove(PlayerStateManager player)
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            player.ChangeState(player.moveState);
        }
        
    }

}
