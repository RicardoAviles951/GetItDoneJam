using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateManager : MonoBehaviour
{
    //Static Events to trigger UI events
    public static UnityAction ShowExamineUI;
    public static UnityAction HideExamineUI;

    public static UnityAction ShowPlayerHUD;
    public static UnityAction HidePlayerHUD;

    public static UnityAction ShowDialogue;
    public static UnityAction HideDialogue;

    public static UnityAction ShowOpeningUI;
    public static UnityAction HideOpeningUI;





    PlayerBaseState currentState;

    public PlayerMoveState moveState         = new PlayerMoveState();
    public PlayerExamineState examineState   = new PlayerExamineState();
    public PlayerDialogueState dialogueState = new PlayerDialogueState();
    public PlayerDeathState deathState       = new PlayerDeathState();
    public PlayerIdleState idleState         = new PlayerIdleState();

    [HideInInspector] public _FirstPersonController movementController;
    [HideInInspector] public StarterAssetsInputs input;
    [HideInInspector] public AbilityManager abilityManager;
    [HideInInspector] public CameraDetector detector;
    [HideInInspector] public PlayerHealth health;
    [HideInInspector] public TriggerVoiceLine triggerVoiceLine;
    public ParticleSystem buffParticles;
    public ParticleSystem debuffParticles;
    



    //State Variables 
    [Header("Examine State")]
    public GameObject CurrentObject;
    public bool isExamining = false;
    [HideInInspector ]public Transform examinedObject;
    public GameObject examinePos;
    public AK.Wwise.Event itemPickupSound;
    public AK.Wwise.Event itemGrabSound;
    [HideInInspector] public Vector3 lastMousePos;
    

    public Dictionary<Transform, Vector3> originalPos    = new Dictionary<Transform, Vector3>();
    public Dictionary<Transform, Quaternion> originalRot = new Dictionary<Transform, Quaternion>();

    [HideInInspector] public IDialogue currentConsole;
    private void OnEnable()
    {
        PlayerHealth.OnDeath += Die;
        DialogueManager.DialogueFinished += BackToMove;
        DialogueManager.PlayerStatusApplied += ApplyStatus;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDeath -= Die;
        DialogueManager.DialogueFinished += BackToMove;
        DialogueManager.PlayerStatusApplied -= ApplyStatus;
    }
    private void Start()
    {
        //Get components
        abilityManager     = GetComponent<AbilityManager>();
        movementController = GetComponent<_FirstPersonController>();
        detector           = GetComponent<CameraDetector>();
        input              = GetComponent<StarterAssetsInputs>();
        health             = GetComponent<PlayerHealth>();
        triggerVoiceLine   = GetComponent<TriggerVoiceLine>();

        //Set state
        currentState = idleState;

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

    public void ToggleDialogue(string toggleDialogue)
    {
        switch (toggleDialogue)
        {
            case "show": ShowDialogue?.Invoke(); break;
            case "hide": HideDialogue?.Invoke(); break;
        }

    }

    public void ToggleOpeningUI(string toggleOpeningUI)
    {
        switch(toggleOpeningUI)
        {
            case "show": ShowOpeningUI?.Invoke(); break;
            case "hide": HideOpeningUI?.Invoke(); break;
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

        if(input.grab)
        {
            input.grab = false;
        }
    }

    public void Die() => ChangeState(deathState);
    public void BackToMove() => ChangeState(moveState);

    void ApplyStatus(Outcome status)
    {
        switch (status)
        {
            case Outcome.speedDown:
                movementController.MoveSpeed   -= 2f;
                movementController.SprintSpeed -= 2f;
                Debug.Log("Speed reduced");
                debuffParticles.Play();
                break;

            case Outcome.speedUp:
                movementController.MoveSpeed   += 2f;
                movementController.SprintSpeed += 2f;
                Debug.Log("Speed increased");
                buffParticles.Play();
                break;

            default: Debug.Log("No speed effects applied."); break;

        }
    }
    
}
