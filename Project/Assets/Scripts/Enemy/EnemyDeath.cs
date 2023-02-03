using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void EnemyDied()
    {
        capsuleCollider.enabled = false;
        //destroy the rigidbody, set the animation to died and destroy gameObject after a set time
        Destroy(rb);
        navMeshAgent.enabled = false;
        animator.SetBool("isStanding", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("died", true);
        Destroy(gameObject, enemy.destroyObjectTime);
    }
}
