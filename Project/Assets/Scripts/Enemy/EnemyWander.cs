using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWander : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Vector3 newPosition;

    public float wanderTime;
    [SerializeField] private float minWanderTime;
    [SerializeField] private float maxWanderTime;

    public float minXValue;
    public float maxXValue;
    public float minZValue;
    public float maxZValue;

    public float walkRadius;

    [SerializeField] private float destinationReachedTreshold;
    private bool hitArena = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        wanderTime = Random.Range(minWanderTime, maxWanderTime);
    }

    public void Wander()
    {
        if (enemy.CalculateDistanceToPlayer() <= enemy.distanceToAttack)
        {
            enemy.state = States.Chase;
        }

        if (!enemy.isDead)
        {
            animator.SetBool("isStanding", false);
            animator.SetBool("isWalking", true);

            if (wanderTime < 0 || CheckIfDestinationReached() || hitArena)
            {
                hitArena = false;
                GenerateNewPosition();
                wanderTime = Random.Range(minWanderTime, maxWanderTime);
            }
            else
            {
                wanderTime -= Time.deltaTime;
            }
        }
    }

    private void GenerateNewPosition()
    {
        float newX = Random.Range(minXValue, maxXValue);
        float newZ = Random.Range(minZValue, maxZValue);

        //calculate new position
        newPosition = new Vector3(newX, 0, newZ);

        //move to new position
        navMeshAgent.SetDestination(newPosition);
    }

    private bool CheckIfDestinationReached()
    {
        var reachedDestination = false;
        float distanceToTarget = Vector3.Distance(transform.position, newPosition);

        if (distanceToTarget < destinationReachedTreshold)
        {
            reachedDestination = true;
        }

        return reachedDestination;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arena")
        {
            Debug.Log("hit arena");
            hitArena = true;
        }
    }
}
