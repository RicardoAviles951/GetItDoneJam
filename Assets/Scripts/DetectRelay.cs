using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectRelay : MonoBehaviour
{
    private VisualElement root;
    private Label infoText;
    private Label abilityText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //Get root element of UI document 
        root = GetComponent<UIDocument>().rootVisualElement;
        //Get reference to Label element
        infoText = root.Q<Label>("Label_infoText");
        abilityText = root.Q<Label>("currentAbility");
    }

    // Update is called once per frame
    void Update()
    {
        //Set Text of object hit
        //Update method good for testing, better to use an event/delegate 
        infoText.text = CameraDetector.DebugName;
        abilityText.text = AbilityManager.DebugAbility;
    }
}
