using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    void Update()
    {
        transform.Rotate(direction, speed);
    }
}
