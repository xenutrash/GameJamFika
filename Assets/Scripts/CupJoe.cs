using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupJoe : MonoBehaviour {
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip boopOneClip; 
    [SerializeField] private AudioClip boopTwoClip; 
    [SerializeField] private AudioClip boopThreeClip; 
    [SerializeField] private AudioClip boopFourClip; 

    public void PlayFirstBoopSound(){
        audioS.PlayOneShot(boopOneClip);     
    }

    public void PlaySecondBoopSound(){
        audioS.PlayOneShot(boopTwoClip);     
    }

    public void PlayThirdBoopSound(){
        audioS.PlayOneShot(boopThreeClip);     
    }

    public void PlayFourthBoopSound(){
        audioS.PlayOneShot(boopFourClip);     
    }
}
