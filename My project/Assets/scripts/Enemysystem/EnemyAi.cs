using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public Transform centrePoint;

    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool GetAttack = false;
    public float healthdelay;

    Enemy enemy1 = new Enemy();
    Enemy enemy2 = new Enemy();
    Enemy enemy3 = new Enemy();

    seedspawner seedspawn;
    EnemyHealth EH;
    Player Pl;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        Pl = GameObject.Find("Player").GetComponent<Player>();
        EH = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        seedspawn = GameObject.Find("SeedSpawner").GetComponent<seedspawner>();
        enemy1.Shield = 10;
        enemy1.AttackDamge = 30;
        enemy1.Hp = 100;
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
            //공격 모션
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
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
    public  void Die()
    {
        seedspawn.enemyrate -= 1;
    }

}

public class Enemy
{
    public int Shield;
    public int AttackDamge;
    public int Hp;
}
