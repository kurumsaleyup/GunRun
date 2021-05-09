using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class HitReactionBullet : MonoBehaviour
{
    public HitReaction hitReaction;
    public float hitForce = 1f;
    
    private RectTransform _crosshair;
    private PlayerMovement playerMovement;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        _crosshair = FindObjectOfType<CrosshairController>().GetComponent<RectTransform>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update() {
        // On left mouse button...
        /*if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            
            var pos = camera.ScreenToWorldPoint(_crosshair.transform.position);
            // Raycast to find a ragdoll collider
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(pos, camera.transform.forward, out hit, 100f)) {

                // Use the HitReaction
                hitReaction.Hit(hit.collider, camera.transform.forward * hitForce, hit.point);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            var gunTip = playerMovement.gunTip;
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, 100f)) {

                // Use the HitReaction
                hitReaction.Hit(hit.collider, gunTip.forward * hitForce, hit.point);
            }
        }
        
        //test
        /*if (Input.GetMouseButton(0))
        {
           var  pos = camera.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;
           if (Physics.Raycast(pos, out hit, 100f))
           {
               hitReaction.Hit(hit.collider, camera.transform.forward * hitForce, hit.point);
           }
        }#1#*/
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        var hitCollider = other.contacts[0].thisCollider;
        var point = other.GetContact(0).point;
        hitReaction.Hit(hitCollider, other.rigidbody.velocity * hitForce, point);
    
    }
}
