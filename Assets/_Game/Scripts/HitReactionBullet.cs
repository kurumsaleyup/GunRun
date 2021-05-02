using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class HitReactionBullet : MonoBehaviour
{
    public HitReaction hitReaction;
    public float hitForce = 1f;

    [SerializeField] private Transform gunTip;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update() {
        // On left mouse button...
        if (Input.GetMouseButtonDown(0)) {

            // Raycast to find a ragdoll collider
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(gunTip.position, gunTip.transform.TransformDirection(Vector3.forward), out hit, 100f)) {

                // Use the HitReaction
                hitReaction.Hit(hit.collider, gunTip.transform.TransformDirection(Vector3.forward).normalized * hitForce, hit.point);
            }
        }
    }
}
