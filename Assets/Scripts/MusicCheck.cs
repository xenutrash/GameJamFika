using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCheck : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;

    void Start()
    {
        if (MusicManager.Instance.GetMusic() != musicClip)
        {
            MusicManager.Instance.PlayMusic(musicClip);
        }
    }
}
