using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        WheelCollider collider = GetComponent<WheelCollider>();
        gameObject.transform.Rotate(collider.rpm, 0, 0);
         
    }
}
