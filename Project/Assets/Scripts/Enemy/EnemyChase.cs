using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Chase()
    {
        //set the animation to running
        animator.SetBool("isStanding", false);
        animator.SetBool("isWalking", true);

        if (enemy.isWandering && enemy.CalculateDistanceToPlayer() > enemy.distanceToAttack)
        {
            enemy.state = States.Wander;
            return;
        }

        //if the destination is reached stop the navMesh from moving and set the state to idle
        if (enemy.reachedDestination)
        {
            navMeshAgent.isStopped = true;
            enemy.state = States.Idle;
        }
        else
        {
            SetDestination();
        }
    }

    private void SetDestination()
    {
        transform.LookAt(Player.PlayerInstance.target);
        if (Vector3.Distance(transform.position, Player.PlayerInstance.target.position) >= enemy.minDist)
        {
            transform.position += transform.forward * enemy.moveSpeed * Time.deltaTime;
            enemy.reachedDestination = false;
        }
        else
        {
            enemy.reachedDestination = true;
        }
    }
}
