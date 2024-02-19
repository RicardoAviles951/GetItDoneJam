using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Current state: Move State");
        player.TogglePlayerHUD("show");
        CursorReturn(player);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //Movement logic
        _FirstPersonController controller = player.movementController;
        controller.JumpAndGravity();
        controller.GroundedCheck();
        controller.Move();

        CheckInteractables(player);     

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

    void CursorReturn(PlayerStateManager player)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Checks specifically what kind of interactable we are dealing with
    void CheckInteractables(PlayerStateManager player)
    {
        CameraDetector detector = player.detector;

        if (detector.detected)
        {
            //Base interface for IExaminable, IDialogue, IPlacer....
            IInteractable interactable = detector.hit.collider.GetComponent<IInteractable>();

            if (player.input.interact)
            {
                switch (interactable)
                {
                    case IExaminable ex:

                        if (ex.isGrabbable)
                        {
                            player.TogglePlayerHUD("hide");
                            player.CurrentObject = detector.hit.collider.gameObject;
                            player.ChangeState(player.examineState);
                        }
                        
                        break;

                    case IDialogue d:
                        player.currentConsole = d;
                        if (d.HasDialogue())
                        {
                            if (d.CheckForRepeatable())
                            {
                                d.LoadDialogue();
                                player.TogglePlayerHUD("hide");
                                player.ChangeState(player.dialogueState);
                            }
                        }

                        break;

                    case IPlacer pl:

                        IExaminable item = InventoryManager.instance.DropNextItem();
                        if (item != null)
                        {  
                            pl.PlaceItem(item);
                        }
                        else
                        {
                            pl.PlaySound();
                        }
                        break;

                    default: Debug.Log("No interactable detected"); break;
                }
            }
        }
    }
}
