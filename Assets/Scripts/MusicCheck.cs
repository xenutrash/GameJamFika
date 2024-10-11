using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCheck : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private string MusicToPlay; 

    void Start()
    {
        if(AudioManager.instance != null)
        {
            if (AudioManager.instance.GetMusic() == musicClip) return;
            AudioManager.instance.Play(MusicToPlay);
            Debug.Log("Updated the playing song"); 
        }


        /*
        if (MusicManager.Instance.GetMusic() != musicClip)
        {
            MusicManager.Instance.PlayMusic(musicClip);
        }*/
    }
}
