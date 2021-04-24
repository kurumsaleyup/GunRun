using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using RootMotion;
using RootMotion.Demos;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool lockCursor = true;
    
    private AnimatorController3rdPerson animatorController; // The Animator controller
    private GameObject camera;
    
    private Vector3 lookDirection;
    private Vector3 aimTarget;



    void Start()
    {
        // Cursors
        Cursor.lockState = lockCursor? CursorLockMode.Locked: CursorLockMode.None;
        Cursor.visible = lockCursor? false: true;
        
        animatorController = GetComponent<AnimatorController3rdPerson>();
        camera = Camera.main.gameObject;
        lookDirection =  camera.transform.forward;
        aimTarget = camera.transform.position + (lookDirection * 10f);
    }

    void LateUpdate()
    {
        
        // Read the input
        Vector3 input = inputVector;

        // Should the character be moving? 
        // inputVectorRaw is required here for not starting a transition to idle on that one frame where inputVector is Vector3.zero when reversing directions.
        bool isMoving = inputVector != Vector3.zero || inputVectorRaw != Vector3.zero;
        
        
        if (isMoving)
        {
            // Character look at vector.
            lookDirection =  camera.transform.forward;

            // Aiming target
            aimTarget = camera.transform.position + (lookDirection * 10f);
        }
    

        // Move the character.
        animatorController.Move(input, isMoving, lookDirection, aimTarget);
    }

    // Convert the input axis to a vector
    private static Vector3 inputVector
    {
        get { return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); }
    }

    // Convert the raw input axis to a vector
    private static Vector3 inputVectorRaw
    {
        get { return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); }
    }
    
}