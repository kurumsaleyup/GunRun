using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using RootMotion;
using RootMotion.Demos;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool lockCursor = true;

    private AnimatorController3rdPerson animatorController; // The Animator controller
    private CinemachineRecomposer composer;
    private CinemachineBrain cinemachineBrain;
    private Coroutine cameraZoom;
    private GameObject camera;

    private Vector3 lookDirection;
    private Vector3 aimTarget;


    void Start()
    {
        // Cursors
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;

        animatorController = GetComponent<AnimatorController3rdPerson>();
        cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        composer = FindObjectOfType<CinemachineFreeLook>().GetComponent<CinemachineRecomposer>();
        camera = Camera.main.gameObject;
        lookDirection = camera.transform.forward;
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
            lookDirection = camera.transform.forward;

            // Aiming target
            aimTarget = camera.transform.position + (lookDirection * 10f);
        }

        if (Input.GetMouseButton(1))
        {
            // Character look at vector.
            lookDirection = camera.transform.forward;

            // Aiming target
            aimTarget = camera.transform.position + (lookDirection * 10f);
        }


        if (Input.GetMouseButtonDown(1))
        {
            if (cameraZoom != null)
            {
                StopCoroutine(cameraZoom);
            }
            cameraZoom = StartCoroutine(ComposerUpdate(0.5f, 7, -5, 0.63f));
        }
        else if ((Input.GetMouseButtonUp(1)))
        {
            if (cameraZoom != null)
            {
                StopCoroutine(cameraZoom);
            }
            cameraZoom = StartCoroutine(ComposerUpdate(0.5f, 0, 0, 1f));
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

    IEnumerator ComposerUpdate(float duration, float pan, float tilt, float zoom)
    {
        var startPan = composer.m_Pan;
        var startTilt = composer.m_Tilt;
        var startZoom = composer.m_ZoomScale;
        var elapsedTime = 0f;
        while (true)
        {
            var progress = Mathf.Clamp01(elapsedTime / duration);
            composer.m_Pan = Mathf.Lerp(startPan, pan, progress);
            composer.m_Tilt = Mathf.Lerp(startTilt, tilt, progress);
            composer.m_ZoomScale = Mathf.Lerp(startZoom, zoom, progress);

            if (progress >= 1f)
            {
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}