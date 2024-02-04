using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraDetector : MonoBehaviour
{
    public static UnityAction triggerPrompt;
    public static UnityAction triggerNoPrompt;


    private Camera _camera;
    private Vector3 centerPoint;
    private Ray ray;
    public bool detected = false;
    
    public RaycastHit hit;

    [SerializeField] private float detectionDistance = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Cache main camera
        _camera = Camera.main;
        //Cache center position
        centerPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Shoots ray every frame
        ray = _camera.ScreenPointToRay(centerPoint);

        CheckForExaminables();
    }

    private void FixedUpdate()
    {
        // Bit shift the index of the layer (3) to get a bit mask
        //Check against only detectable objects
        int layerMask = 1 << 3; 

        //Fires a raycast from the center point of the camera. Returns if hit or not. 
        detected = Physics.Raycast(ray, out hit, detectionDistance, layerMask);
    }

    void CheckForExaminables()
    {
        if (detected)
        {
            IExaminable examinable = hit.collider.GetComponent<IExaminable>();

            if (examinable != null)
            {
                triggerPrompt?.Invoke();
            }
        }
        else
        {
            triggerNoPrompt?.Invoke();
        }
        
    }
    
}
