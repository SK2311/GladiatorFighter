using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public States state;
    public Animator animator;

    public bool reachedDestination;
    public bool hitShield;
    public bool isDead;
    public bool isWandering;
    public bool isFleeing;

    public float waitForAttackTime = 3f;
    public float minWaitForAttackTime = 1f;
    public float maxWaitForAttackTime = 2f;
    public float destroyObjectTime = 2f;

    public float moveSpeed = 4;
    private float minMoveSpeed = 2;
    private float maxMoveSpeed = 5;
    public float minDist = 1.5f;
    public int maxDist = 3;

    public float distanceToAttack;

    [HideInInspector] public EnemyAttack enemyAttack;
    [HideInInspector] public EnemyChase enemyChase;
    [HideInInspector] public EnemyIdle enemyIdle;
    [HideInInspector] public EnemyDeath enemyDeath;
    [HideInInspector] public EnemyWander enemyWander;
    [HideInInspector] public EnemyFlee enemyFlee;

    private int numberOfChilds;

    void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        enemyChase = GetComponent<EnemyChase>();
        enemyIdle = GetComponent<EnemyIdle>();
        enemyDeath = GetComponent<EnemyDeath>();
        enemyWander = GetComponent<EnemyWander>();
        enemyFlee = GetComponent<EnemyFlee>();

        animator = GetComponent<Animator>();

        state = States.Idle;

        reachedDestination = false;
        hitShield = false;
        isDead = false;

        if (isWandering)
        {
            state = States.Wander;
        }

        if (isFleeing)
        {
            state = States.Flee;
        }

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        numberOfChilds = gameObject.transform.childCount;
    }

    private void Update()
    {
        if (isDead)
        {
            state = States.Dead;
        }

        if (Vector3.Distance(transform.position, Player.PlayerInstance.target.position) > maxDist)
        {
            reachedDestination = false;
        }

        if (transform.childCount > numberOfChilds)
        {
            AudioManager.instance.Play("PlayerHit");
            isDead = true;
            foreach (Transform arrow in transform)
            {
                if (arrow.tag == "Arrow")
                {
                    Destroy(arrow.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Player.PlayerInstance.playerDied || PauseMenu.GameIsPaused)
        {
            if (collision.gameObject.tag == "Weapon")
            {
                AudioManager.instance.Play("PlayerHit");
                isDead = true;
            }
        }

        if (collision.gameObject.tag == "Arena")
        {
            enemyWander.wanderTime = 0;
        }
    }

    public float CalculateDistanceToPlayer()
    {
        float distance = Vector3.Distance(transform.position, Player.PlayerInstance.transform.position);
        return distance;
    }

    public float GetRandomAttackTime()
    {
        float time = Random.Range(minWaitForAttackTime, maxWaitForAttackTime);
        return time;
    }

    public void SetEnemyType()
    {
        int wanderValue = Random.Range(0, 1000);
        if (wanderValue % 2 == 0)
        {
            isWandering = true;
        }

        if (!isWandering)
        {
            int fleeValue = Random.Range(0, 1000);
            if (fleeValue % 2 == 0)
            {
                isFleeing = true;
            }
        }
    }
}
