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
    public ParticleSystem particles;

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
}
