using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public static event Action<string> SendAbilityUI;

    AbilityBase currentAbility;

    //Create instances of each ability 
    public ElectricityAbility electricityAbility = new ElectricityAbility();
    public FireAbility fireAbility               = new FireAbility();
    public EmptyAbility emptyAbility             = new EmptyAbility();

    public SO_AbilityData abilityData;

    
    //Reference to input system
    [HideInInspector] public StarterAssetsInputs input;
    [HideInInspector] public CameraDetector detector;
    [Header("Character Animations")]
    [Tooltip("The animtion controller for the player arms")]
    public Animator ac_PlayerArms;

    [Header("Fire Ability")]
    public List<ParticleSystem> fireIdle;
    public List<ParticleSystem> fireOnce;
    public List<ParticleSystem> fireShoot;
    public int FireEmissionCount = 1;
    public AK.Wwise.Event fireSound;

    [Header("Electric Ability")]
    public List<ParticleSystem> electricIdle;
    public List<ParticleSystem> electricOnce;
    public List<ParticleSystem> electricShoot;
    public int ElectricEmissionCount = 1;
    public AK.Wwise.Event electricSound;

    // Start is called before the first frame update
    private void Awake()
    {
        currentAbility = emptyAbility;
    }
    void Start()
    {
        currentAbility.Activate(this);
        detector = GetComponent<CameraDetector>();
        input    = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    public void ActivateAbility()
    {
        currentAbility.UpdateAbility(this);
        
        HandleAbilitiesAnim();
    }

    public void ChangeAbility(AbilityBase ability)
    {
        currentAbility = ability;
        ability.Activate(this);
    }

    

    public void GetAbilityText(string text)
    {
        SendAbilityUI?.Invoke(text);
    }

    public void HandleAbilitiesAnim()
    {
        if(currentAbility != emptyAbility)
        {
            if (input.fire)
            {
                ac_PlayerArms.SetTrigger("Fired");
                ac_PlayerArms.SetBool("isFiring", true);
            }
            else
            {
                //ac_PlayerArms.SetTrigger("Fired");
                ac_PlayerArms.SetBool("isFiring", false);
            }
        }
    }
}
