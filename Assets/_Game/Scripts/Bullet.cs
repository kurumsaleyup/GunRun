using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float appliedHitForce = 3f;
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        var hitForce = other.gameObject.GetComponentInParent<HitReaction>();
        if (hitForce !=null)
        {
            var hitCollider = other.collider;
            var point = other.GetContact(0).point;
            hitForce.Hit(hitCollider, rb.velocity.normalized * appliedHitForce, point);
            Debug.Log("force applied");
        }
    
    }
}
