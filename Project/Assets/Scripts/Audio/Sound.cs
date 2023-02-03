using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;

    [HideInInspector]
    public AudioSource audioSource;

    public bool loop;
    public float spatialBlend;
    public float volume;
}
