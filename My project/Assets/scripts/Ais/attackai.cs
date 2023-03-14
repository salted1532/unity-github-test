using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class attackai : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public Transform centrePoint;

    public float walkPointRange;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject sword;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool GetAttack = false;
    public float healthdelay;

    seedspawner seedspawn;
    EnemyHealth EH;
    Player Pl;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        Pl = GameObject.Find("Player").GetComponent<Player>();
        EH = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
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
            AttackPlayer();
        }
        if (GetAttack == true)
        {
            GetAttack = false;
            EH.TakeDamage(Pl.PDamge);
            healthdelay = 2;
            Debug.Log("죽음");
            var heading = centrePoint.position - player.position;
            float backing = 0;
            while (backing < 5f)
            {
                transform.Translate(Vector3.back * 0.5f * Time.deltaTime);
                backing += Time.deltaTime;
            }
        }
    }

    private void Patorling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)//목적지 도착 확인
        {
            SearchWalkPoint();
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
    private void SearchWalkPoint()
    {
        Vector3 point;
        if (RandomPoint(centrePoint.position, sightRange, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            agent.SetDestination(point);
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(player.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Vector3 forwardVector = transform.forward;
            Vector3 newPosition = transform.position + forwardVector * 2;
            //공격 모션
            Rigidbody rb = Instantiate(sword, newPosition, Quaternion.identity).GetComponent<Rigidbody>();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetAttack = true;
        }
    }
}
