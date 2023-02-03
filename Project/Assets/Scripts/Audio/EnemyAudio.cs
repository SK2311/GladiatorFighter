using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] footStepClips;
    [SerializeField] private AudioClip attackClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        var clip = GetRandomClip(footStepClips);
        audioSource.PlayOneShot(clip);
    }

    private void EnemyAttack()
    {
        if (Player.PlayerInstance.playerHit)
        {
            audioSource.PlayOneShot(attackClip);
            Player.PlayerInstance.health -= 1;
        }
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
