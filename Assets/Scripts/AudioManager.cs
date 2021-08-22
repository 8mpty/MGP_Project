using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AllAudio[] sounds;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
        foreach (AllAudio s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.vol;
            s.source.loop = s.looping;
            s.source.mute = s.mute;
        }
    }

    void Start()
    {
        Play("BGM");
    }

    public void Play(string name)
    {
        AllAudio s = Array.Find(sounds, AllAudio => AllAudio.name == name);

        if(s == null)
        {
            Debug.LogWarning("Sound of " + " ' " + name + " ' " + " is not available.");
            return;
        }

        s.source.Play();
    }

    public void PauseBGM(string name)
    {

        AllAudio s = Array.Find(sounds, AllAudio => AllAudio.name == name);
        
        if (s == null)
        {
            Debug.LogWarning("Sound of " + " ' " + name + " ' " + " is not available.");
            return;
        }
        s.source.Pause();
    }

    public void UnPauseBGM(string name)
    {

        AllAudio s = Array.Find(sounds, AllAudio => AllAudio.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound of " + " ' " + name + " ' " + " is not available.");
            return;
        }
        s.source.UnPause();
    }


}
