using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player State: Dialogue state");
        CursorCalibrate(player);

        Debug.Log("Current console: " + player.currentConsole.name);

        
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //if (player.input.interact)
        //{
        //    player.ToggleDialogue("hide");
        //    CursorReturn(player);
        //    player.ChangeState(player.moveState);
        //}
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        
    }

    public override void LateUpdateState(PlayerStateManager player)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {
        
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider other)
    {
        
    }

    void CursorCalibrate(PlayerStateManager player)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
