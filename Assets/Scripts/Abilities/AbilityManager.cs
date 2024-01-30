using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    AbilityBase currentAbility;

    //Create instances of each ability 
    public ElectricityAbility electricityAbility = new ElectricityAbility();
    public FireAbility fireAbility               = new FireAbility();
    public EmptyAbility emptyAbility             = new EmptyAbility();

    public SO_AbilityData abilityData;

    public static string DebugAbility = "";
    //Reference to input system
    [HideInInspector] public StarterAssetsInputs input;
    public CameraDetector detector;
    public ParticleSystem particles;

    // Start is called before the first frame update
    private void Awake()
    {
        currentAbility = emptyAbility;
    }
    void Start()
    {
        currentAbility.Activate(this);
        input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAbility.UpdateAbility(this);
        DebugAbility = currentAbility.ToString();

        
        if (input.change)
        {
            input.change = false;
        }

        
    }

    public void ChangeAbility(AbilityBase ability)
    {
        currentAbility = ability;
        ability.Activate(this);
    }
}
