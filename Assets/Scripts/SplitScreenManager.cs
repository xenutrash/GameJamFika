using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreenManager : MonoBehaviour
{


    readonly List<Camera> cameras = new();
    readonly List<Player> players = new();
    public GameObject playerToSpawn;
    public GameObject playerCameraRig;
    [SerializeField]
    GameObject[] spawnPoints;
    public Vector3 SpringArmOffset; 

    int index = 0; 

    private void Start()
    {
        
    }


    private void FixedUpdate()
    {
        if(players.Count > Gamepad.all.Count)
        {
            Vector3 pos = spawnPoints[index].transform.position;
            Quaternion rotastion = spawnPoints[index].transform.rotation;
            Gamepad pad = Gamepad.all[index];

            GameObject spawnedPlayer = Instantiate<GameObject>(playerToSpawn);
            spawnedPlayer.transform.SetPositionAndRotation(pos, rotastion);
            Player player = spawnedPlayer.GetComponent<Player>();

            GameObject spawnedCamera = Instantiate<GameObject>(playerToSpawn);
            spawnedCamera.transform.SetPositionAndRotation(pos + SpringArmOffset, rotastion); 
            Camera camera = spawnedCamera.GetComponentInChildren<Camera>();
           
            player.SetController(pad);
            player.attatchedCamera = camera; 
            AddPlayer(player);
            UpdateCameraView(); 
            index++; 
        }



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
        if(cameras.Count <= 1)
        {
            return; 
        }
        foreach(var camera in cameras)
        {
            float x = 0.5f; 
            if(pos < 1)
            {
                x = 1; 
            }
            camera.rect = new(
               x , 1,
                (1 / target), target > 2 ? 1 / 4 : 1); 
                
                pos++;
        }

    }
}


