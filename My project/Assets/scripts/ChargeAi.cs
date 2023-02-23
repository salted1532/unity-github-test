using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform centrePoint;
    public LayerMask whatIsPlayer;
    public Vector3 chargepoint;
    public GameObject projectile;
    public float sightRange, chargeRange, timeBetweenAttacks;
    public bool playerInSightRange, playerInAttackRange, isinwall, charging;

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
            playerInAttackRange = Physics.CheckSphere(transform.position, chargeRange, whatIsPlayer);
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
        if (agent.remainingDistance <= agent.stoppingDistance)//목적지 도착 확인
        {
            SearchWalkPoint();
        }
    }
    private void SearchWalkPoint() //랜덤 위치 생성
    {
        Vector3 point;
        if (RandomPoint(centrePoint.position, sightRange, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            agent.SetDestination(point);
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result) //랜덤 위치 생성
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
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

        if (!charging)
        {
            //공격 모션
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10000f);
        }
    }
    private void ChargePlayer()
    {
        charging = true;
        agent.SetDestination(chargepoint);
        agent.speed = 10f;
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Invoke(nameof(ResetCharge), timeBetweenAttacks);
        }

    }
    private void ResetCharge()
    {
        charging = false;
        isinwall = false;
        agent.speed = 3.5f;
    }
}
