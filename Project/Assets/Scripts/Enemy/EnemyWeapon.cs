using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = transform.root.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shield")
        {
            AudioManager.instance.Play("HitShield");
            enemy.hitShield = true;
            Debug.Log("hit shield");
        }
        else if (other.gameObject.tag == "Target")
        {
            Player.PlayerInstance.playerHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            //AudioManager.instance.Play("EnemyHit");
            //Debug.Log("hit player");
            //Player.PlayerInstance.health -= 1;
            Player.PlayerInstance.playerHit = false;
        }
    }
}
