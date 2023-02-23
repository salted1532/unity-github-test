using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;


    public Transform centrePoint;


    public Vector3 chargepoint;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float range;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public bool isinwall;
    public bool charging;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(charging == false)
        {
            //플레이어 관측하고 공격하기
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerInSightRange && !playerInAttackRange)
            {
                Patorling();
            }
            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            if (playerInSightRange && playerInAttackRange)
            {
                SetChargePosition();
            }
        }
        if (isinwall == true)
        {
            ChargePlayer();
        }
    }

    private void Patorling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            SearchWalkPoint();
        }
    }
    private void SearchWalkPoint()
    {
        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            agent.SetDestination(point);
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void SetChargePosition()
    {
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //공격 모션
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10000f);
            alreadyAttacked = true;

        }
    }
    private void ChargePlayer()
    {
        charging = true;
        agent.SetDestination(chargepoint);
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        charging = false;
    }
}
