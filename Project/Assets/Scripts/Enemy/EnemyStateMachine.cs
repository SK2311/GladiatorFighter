using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Idle,
    Chase,
    Attack,
    Wander,
    Flee,
    Dead
}

public class EnemyStateMachine : MonoBehaviour
{
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //stateMachine to call the correct methods when the state is changed
        if (enemy.state == States.Idle)
        {
            enemy.enemyIdle.Idle();
        }
        else if (enemy.state == States.Chase)
        {
            enemy.enemyChase.Chase();
        }
        else if (enemy.state == States.Attack)
        {
            enemy.enemyAttack.Attack();
        }
        else if (enemy.state == States.Wander)
        {
            enemy.enemyWander.Wander();
        }
        else if (enemy.state == States.Dead)
        {
            enemy.enemyDeath.EnemyDied();
        }
        else if (enemy.state == States.Flee)
        {
            enemy.enemyFlee.Flee();
        }
    }
}
