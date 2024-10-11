using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    AudioSource audioSourceCached; 

    void Awake()
    {

        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        instance = this;

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " could not be found. Is it spelled correctly?");
            return;

        }

        if (audioSourceCached == null)
        {
            if (!gameObject.TryGetComponent<AudioSource>(out audioSourceCached))
            {
                audioSourceCached = gameObject.AddComponent<AudioSource>();
            }
        }
        Debug.Log("I am playing"); 
        // Set the AudioSource properties from the Sound object
        audioSourceCached.clip = s.clip;
        audioSourceCached.volume = s.volume;
        audioSourceCached.pitch = s.pitch;
        audioSourceCached.loop = s.loop;
        //audioSourceCached.spatialBlend = 0;  // Assuming you want to keep 3D sound

        // Play the sound
        audioSourceCached.Play();
       
    }

    public void Pause()
    {
        if (audioSourceCached == null)
        {
            if (!gameObject.TryGetComponent<AudioSource>(out audioSourceCached))
            {
                audioSourceCached = gameObject.AddComponent<AudioSource>();
            }
        }

        audioSourceCached.Pause();
    }
    
    public void Resume()
    {
        if (audioSourceCached == null)
        {
            if (!gameObject.TryGetComponent<AudioSource>(out audioSourceCached))
            {
                audioSourceCached = gameObject.AddComponent<AudioSource>();
            }
        }

       

        audioSourceCached.UnPause();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name  + " could not be found. Is it spelled correctly?");
            return;

        }
        if(audioSourceCached == null)
        {
            if (!gameObject.TryGetComponent<AudioSource>(out audioSourceCached))
            {
                audioSourceCached = gameObject.AddComponent<AudioSource>();
            }
        }
       
        audioSourceCached.PlayOneShot(s.clip, s.volume);
    }

    public AudioClip GetMusic()
    {

        if (audioSourceCached == null)
        {
            if (!gameObject.TryGetComponent<AudioSource>(out audioSourceCached))
            {
                audioSourceCached = gameObject.AddComponent<AudioSource>();
            }
        }

        return audioSourceCached.clip;
    }

}