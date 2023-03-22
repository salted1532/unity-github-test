using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tunnelAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public Transform centrePoint;
    public Vector3 OnGroundpoint;

    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject attackpoint;
    public float waitTimer = 1f;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool GetAttack = false;
    public float healthdelay;

    seedspawner seedspawn;
    EnemyHealth EH;
    Player Pl;
    Rigidbody rb;
    public EnemySAO E_data;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        Pl = GameObject.Find("Player").GetComponent<Player>();

        EH = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //seedspawn = GameObject.Find("SeedSpawner").GetComponent<seedspawner>();
    }

    private void Update()
    {
        rb.velocity = Vector3.zero;
        //�÷��̾� �����ϰ� �����ϱ�
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
    }

    private void Patorling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)//������ ���� Ȯ��
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                SearchWalkPoint();
                waitTimer = 0f;
            }
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result) //���� ��ġ ����
    {

        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        randomPosition += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas))
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
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //���� ���
            Rigidbody rb = Instantiate(attackpoint, player.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            alreadyAttacked = true;
            agent.Warp(new Vector3(-40,1,0));
            Invoke(nameof(OnGround), 3f);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void OnGround()
    {
        agent.Warp(OnGroundpoint);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var heading = centrePoint.position - player.position;
            float backing = 0;
            while (backing < 5f)
            {
                transform.Translate(Vector3.back * 0.5f * Time.deltaTime);
                backing += Time.deltaTime;
            }
            EH.TakeDamage(Pl.PDamge);
            Debug.Log("������ ����");
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        seedspawn.enemyrate -= 1;
    }
}
