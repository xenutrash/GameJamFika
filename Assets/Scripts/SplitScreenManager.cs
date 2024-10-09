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
        foreach (Gamepad pad in Gamepad.all)
        {
            SpawnPlayer();
            UpdateCameraView(); 
            index++;
        }
  
    }


    private void FixedUpdate()
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
        if(cameras.Count <= 1)
        {
            return; 
        }
        foreach(var camera in cameras)
        {
            Debug.Log("Camera lol");
            float x = 0.5f; 
            if(pos < 1)
            {
                x = 0; 
            }

            camera.rect = new(
               x , 0,
                0.5f,1); 
                
                pos++;
        }

    }

    private void SpawnPlayer()
    {
        Debug.Log("Spawning thing");
        Vector3 pos = spawnPoints[index].transform.position;
        Quaternion rotastion = spawnPoints[index].transform.rotation;
        Gamepad pad = Gamepad.all[index];

        GameObject spawnedPlayer = Instantiate<GameObject>(playerToSpawn);
        spawnedPlayer.transform.SetPositionAndRotation(pos, rotastion);
        Player player = spawnedPlayer.GetComponent<Player>();

        GameObject spawnedCamera = Instantiate<GameObject>(playerCameraRig);
        spawnedCamera.transform.SetPositionAndRotation(pos + SpringArmOffset, rotastion);
        SpringArm springArm = spawnedCamera.GetComponent<SpringArm>();
        springArm.target = spawnedPlayer.transform;
        Camera camera = spawnedCamera.GetComponentInChildren<Camera>();

        player.SetController(pad);
        player.attatchedCamera = camera;
        AddPlayer(player);

    }
}


