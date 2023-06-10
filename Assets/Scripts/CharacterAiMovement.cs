using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAiMovement : MonoBehaviour
{
    private NavMeshAgent mNavMeshAgent;
    private float mNextDestinationTime = 0f;
    private float mDestinationInterval = 10f;
    private float mWanderRadius = 10f;

    private void Awake()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetRandomDestination();
    }

    private void Update()
    {
        if (Time.time >= mNextDestinationTime)
        {
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * mWanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, mWanderRadius, 1);
        Vector3 finalPosition = hit.position;

        mNavMeshAgent.SetDestination(finalPosition);
        mNextDestinationTime = Time.time + mDestinationInterval;
    }
}
