using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
 
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
   
    }
    
    // Clamping Euler angles
    private float ClampAngle (float angle, float min, float max) {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp (angle, min, max);
    }
}
