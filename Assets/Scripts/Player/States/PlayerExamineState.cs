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

            originalPosition = player.CurrentObject.transform.position;
            originalRotation = player.CurrentObject.transform.rotation;
        }
        
        if (examinedObj != null)
        {
            Debug.Log("Moving examined object...");
            examinedObj.Examine(player.examinePos.transform.position);
        }
        player.ToggleExamineUI("show");
        CursorCalibration(player);


    }
    public override void UpdateState(PlayerStateManager player)
    {
        ExamineObject(player);


        if (player.input.interact)
        {
            if (examinedObj != null)
            {
                CursorReturn(player);
                examinedObj.UnExamine(originalPosition, originalRotation);
            }

            player.ToggleExamineUI("hide");
            player.ChangeState(player.moveState);
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


    void CursorCalibration(PlayerStateManager player)
    {
        player.lastMousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CursorReturn(PlayerStateManager player)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
}
