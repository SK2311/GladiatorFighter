using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : MonoBehaviour
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

    public void Idle()
    {
        animator.SetBool("isStanding", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);

        if (Player.PlayerInstance.playerDied)
        {
            return;
        }
        else if (enemy.isWandering && enemy.CalculateDistanceToPlayer() > enemy.distanceToAttack)
        {
            enemy.state = States.Wander;
            return;
        }
        else if (!enemy.reachedDestination) //if the destination isn't reached, set state to chase
        {
            navMeshAgent.isStopped = false;
            enemy.state = States.Chase;
        }
        else if (enemy.isDead) //if the enemy died, set state to dead
        {
            enemy.state = States.Dead;
        }
        else //else set the state to attack
        {
            enemy.state = States.Attack;
        }
    }
}
