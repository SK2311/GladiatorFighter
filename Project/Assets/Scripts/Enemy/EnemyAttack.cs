using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
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

    public void Attack()
    {
        if (Player.PlayerInstance.playerDied)
        {
            Debug.Log("player died");
            animator.SetBool("isStanding", true);
            animator.SetBool("isAttacking", false);
            return;
        }

        if (enemy.isWandering && enemy.CalculateDistanceToPlayer() > enemy.distanceToAttack)
        {
            enemy.state = States.Wander;
            return;
        }

        //check if the destination is still reached else stop attacking and move to the new destination
        if (!enemy.reachedDestination)
        {
            navMeshAgent.isStopped = false;
            enemy.state = States.Chase;
            enemy.waitForAttackTime = enemy.GetRandomAttackTime();
        }

        //check if the shield is hit, then play the hit animation
        if (enemy.hitShield)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("hitShield", true);
            enemy.waitForAttackTime = enemy.GetRandomAttackTime();

            enemy.hitShield = false;
        }
        else //else start the attack counter
        {
            animator.SetBool("hitShield", false);

            if (enemy.waitForAttackTime > 0)
            {
                enemy.waitForAttackTime -= Time.deltaTime;
            }
            else
            {
                //if the timer is 0 play the attack animation and reset the timer
                animator.SetBool("isAttacking", true);
                animator.SetBool("isStanding", false);
                enemy.state = States.Idle;
                enemy.waitForAttackTime = enemy.GetRandomAttackTime();
            }
        }
    }
}
