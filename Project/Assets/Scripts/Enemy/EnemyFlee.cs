using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlee : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public float minDistance;

    [SerializeField] private float wanderTime;
    [SerializeField] private float minWanderTime;
    [SerializeField] private float maxWanderTime;

    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        wanderTime = Random.Range(minWanderTime, maxWanderTime);
    }

    public void Flee()
    {
        if (!enemy.isDead)
        {
            if (Vector3.Distance(transform.position, Player.PlayerInstance.target.position) <= minDistance)
            {
                animator.SetBool("isStanding", false);
                animator.SetBool("isWalking", true);

                //player to close to enemy, move away
                Vector3 directionToPlayer = transform.position - Player.PlayerInstance.transform.position;
                Vector3 newPosition = transform.position + directionToPlayer;
                navMeshAgent.SetDestination(newPosition);
            }
            else
            {
                //let them wander
                Wander();
            }
        }
    }

    private void Wander()
    {
        animator.SetBool("isStanding", false);
        animator.SetBool("isWalking", true);

        GenerateNewPosition();
    }

    private void GenerateNewPosition()
    {
        float newX = Random.Range(enemy.enemyWander.minXValue, enemy.enemyWander.maxXValue);
        float newZ = Random.Range(enemy.enemyWander.minZValue, enemy.enemyWander.maxZValue);

        //calculate new position
        newPosition = new Vector3(newX, 0, newZ);

        //move to new position
        navMeshAgent.SetDestination(newPosition);
    }
}
