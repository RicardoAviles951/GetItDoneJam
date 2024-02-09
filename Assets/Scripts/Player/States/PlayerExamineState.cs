using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class PlayerExamineState : PlayerBaseState
{
    IExaminable examinedObj;
    Vector3 originalPosition;
    Quaternion originalRotation;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Current state: Examine State");
        if(player.CurrentObject != null)
        {
            examinedObj = player.CurrentObject.GetComponent<IExaminable>();

            //Cache original positions [Obsolete]
            originalPosition = player.CurrentObject.transform.position;
            originalRotation = player.CurrentObject.transform.rotation;
        }
        
        if (examinedObj != null)
        {
            Debug.Log("Moving examined object...");
            //Moves the object into view
            examinedObj.Examine(player.examinePos.transform.position);
        }
        //Turns on UI and makes the cursor visible
        player.ToggleExamineUI("show");
        CursorCalibration(player);
   

    }
    public override void UpdateState(PlayerStateManager player)
    {
        //Rotates the object based on mouse position
        ExamineObject(player);

        //If pressing the interact button again the item will be put back where it was
        if (player.input.interact)
        {
            if (examinedObj != null)
            {
                examinedObj.UnExamine(originalPosition, originalRotation);
            }

            //Play sound here

            ExitState(player);
        }
        
        //If pressing the grab button the item will be added to inventory
        if(player.input.grab)
        {
            //Add item to inventory 
            InventoryManager.instance.AddItem(examinedObj);
            InventoryManager.instance.GetInventoryItems();
            examinedObj.ToggleExaminable(false,Vector3.zero);

            //Play sound here

            ExitState(player);
        }
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
        
    }

    void ExamineObject(PlayerStateManager player)
    {
        if (examinedObj != null)
        {
            Transform obj = player.CurrentObject.transform;
            Vector3 deltaMouse = Input.mousePosition - player.lastMousePos;
            float rotationSpeed = 1.0f;
            obj.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            obj.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            player.lastMousePos = Input.mousePosition;
        }
    }

    //Makes the cursor visible and not locked to center.
    void CursorCalibration(PlayerStateManager player)
    {
        player.lastMousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Locks cursor to center and hides it. 
    void CursorReturn(PlayerStateManager player)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Toggle the Examine UI when going back to move state. 
    void ExitState(PlayerStateManager player)
    {
        player.ToggleExamineUI("hide");
        CursorReturn(player);
        player.ChangeState(player.moveState);
    }
    
}
