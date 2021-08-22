using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class AllAudio
{
    public string name;
    
    public AudioClip clip;

    [Range(0f, 1f)]public float vol;

    public bool looping;

    [HideInInspector] public AudioSource source;

    public bool mute;
}
