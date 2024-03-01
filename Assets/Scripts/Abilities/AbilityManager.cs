using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public static event Action<AbilityInfo> SendAbilityUI;

    AbilityBase currentAbility;

    //Create instances of each ability 
    public ElectricityAbility electricityAbility = new ElectricityAbility();
    public FireAbility fireAbility               = new FireAbility();
    public EmptyAbility emptyAbility             = new EmptyAbility();

    public AbilityInfo abilityInfo;

    public SO_AbilityData abilityData;

    
    //Reference to input system
    [HideInInspector] public StarterAssetsInputs input;
    [HideInInspector] public CameraDetector detector;
    [Header("Character Animations")]
    [Tooltip("The animation controller for the player arms")]
    public Animator ac_PlayerArms;

    [Header("Fire Ability")]
    public List<ParticleSystem> fireIdle;
    public List<ParticleSystem> fireOnce;
    public List<ParticleSystem> fireShoot;
    public int FireEmissionCount = 1;
    public AK.Wwise.Event fireSound;
    public AK.Wwise.Event fireLoopSound;

    [Header("Electric Ability")]
    public List<ParticleSystem> electricIdle;
    public List<ParticleSystem> electricOnce;
    public List<ParticleSystem> electricShoot;
    public int ElectricEmissionCount = 1;
    public AK.Wwise.Event electricSound;
    public AK.Wwise.Event electricLoopSound;


    public string currentIcon;

    // Start is called before the first frame update
    private void Awake()
    {
        currentAbility = emptyAbility;
    }
    void Start()
    {
        abilityInfo = new AbilityInfo(currentIcon,currentAbility.Name);
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

    public void ChangeToFire()
    {
        currentAbility = fireAbility;
        fireAbility.Activate(this);
    }

    public void GetAbilityText()
    {
        abilityInfo.file = currentIcon;
        abilityInfo.Name = currentAbility.Name;
        SendAbilityUI?.Invoke(abilityInfo);
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
