using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateManager : MonoBehaviour
{
    public static UnityAction ShowExamineUI;
    public static UnityAction HideExamineUI;

    public static UnityAction ShowPlayerHUD;
    public static UnityAction HidePlayerHUD;

    

    PlayerBaseState currentState;

    public PlayerMoveState moveState         = new PlayerMoveState();
    public PlayerExamineState examineState   = new PlayerExamineState();
    public PlayerDialogueState dialogueState = new PlayerDialogueState();

    [HideInInspector] public _FirstPersonController movementController;
    [HideInInspector] public StarterAssetsInputs input;
    [HideInInspector] public AbilityManager abilityManager;
    [HideInInspector] public CameraDetector detector;



    //State Variables 
    [Header("Examine State")]
    public GameObject CurrentObject;
    public bool isExamining = false;
    public Transform examinedObject;
    public GameObject examinePos;
    [HideInInspector] public Vector3 lastMousePos;

    public Dictionary<Transform, Vector3> originalPos    = new Dictionary<Transform, Vector3>();
    public Dictionary<Transform, Quaternion> originalRot = new Dictionary<Transform, Quaternion>();
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        //Get components
        abilityManager = GetComponent<AbilityManager>();
        movementController = GetComponent<_FirstPersonController>();
        detector = GetComponent<CameraDetector>();
        input = GetComponent<StarterAssetsInputs>();

        //Set state
        currentState = dialogueState;

        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
        InputResets();
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private void LateUpdate()
    {
        currentState.LateUpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);   
    }

    public void ChangeState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    public void ToggleExamineUI(string toggleUI)
    {
        switch (toggleUI)
        {
            case "show": ShowExamineUI?.Invoke(); break;
            case "hide": HideExamineUI?.Invoke(); break;
        }
       
    }
    
    public void TogglePlayerHUD(string toggleHUD)
    {
        switch(toggleHUD)
        {
            case "show": ShowPlayerHUD?.Invoke(); break;
            case "hide": HidePlayerHUD?.Invoke(); break;
        }
    }

    void InputResets()
    {
        //The input system as configured does not reset the isPressed value when the input is released. So we have to do it manually.
        if (input.change)
        {
            input.change = false;
        }

        if(input.interact)
        {
            input.interact = false;
        }
    }

    
}
