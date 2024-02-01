using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    private Camera _camera;
    private Vector3 centerPoint;
    private Ray ray;
    public bool detected = false;
    public RaycastHit hit;

    //Just for testing purposes 
    public static string DebugName;

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

        if (detected)
        {
            //store the name of the object
            //string name = hit.collider.gameObject.name;
            //Store into static var for testing 
            //DebugName = name;

            //Console logs and debug lines
            //print(name);
           // Debug.DrawRay(_camera.transform.position, _camera.transform.forward*detectionDistance);
        }
        else
        {
            //clear the out hit information
            DebugName = "";
        }
    }

    private void FixedUpdate()
    {
        // Bit shift the index of the layer (3) to get a bit mask
        //Check against only detectable objects
        int layerMask = 1 << 3; 

        //Fires a raycast from the center point of the camera. Returns if hit or not. 
        detected = Physics.Raycast(ray, out hit, detectionDistance, layerMask);
    }
}
