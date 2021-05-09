using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{

    public enum States
    {
        Chasing,
        Wander,
        Dead,
        Succeed
    }
    public States zombieState = States.Wander;
    
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private PuppetMaster puppetMaster;
    public int hitCount = 0;
    private Vector3? wanderDestination;

    
    #region Life Cycle

    void Update()
    {
        if (zombieState == States.Dead)
        {
            return;
        }
        
        if (hitCount > 10)
        {
            puppetMaster.state = PuppetMaster.State.Dead;
            _navMeshAgent.enabled = false;
            zombieState = States.Dead;
        }
        if (!wanderDestination.HasValue)
        {
            wanderDestination = RandomNavSphere(transform.position, wanderRadius, -1);
        }
        else if(_navMeshAgent.enabled)
        {
            Debug.DrawLine(transform.position, wanderDestination.Value, Color.magenta, 1f);
            _navMeshAgent.destination = wanderDestination.Value;
            if (Vector3.Distance(transform.position, wanderDestination.Value) < 1f)
            {
                wanderDestination = null;
            }
        }
    }


    #endregion

    
    #region Random Movement

    private Vector3? RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }

        return null;
    }

    #endregion
}
