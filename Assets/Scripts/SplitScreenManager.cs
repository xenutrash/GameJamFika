using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class SplitScreenManager : MonoBehaviour
{


    readonly List<Camera> cameras = new();
    readonly List<Player> players = new();

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
        cameras.Add(player.attatchedCamera);
        
    }

    public void UpdateCameraView()
    {
        int target = cameras.Count;
        int pos = 0; 
        foreach(var camera in cameras)
        {
            camera.rect = new(); 
                pos++; 
        }

    }
}


