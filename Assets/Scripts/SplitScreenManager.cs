
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Awake()
    {
        int playersInGame = CrossSceneContainer.PlayersInGame;

        Debug.Log(CrossSceneContainer.PlayersInGame); 

        for(int i = 0; i < playersInGame; i++)
        {
            SpawnPlayer();
           
            index++;
        }
        UpdateCameraView();

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

        if(cameras.Count == 2)
        {
            cameras[0].rect = new(0,0,0.5f,1);
            cameras[1].rect = new(0.5f, 0, 0.5f, 1); 
            return;
        }

        foreach (var camera in cameras)
        {
            switch (pos)
            {
                case 0:
                    camera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                    break; 
                case 1:
                    camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    break;
                case 2:
                    camera.rect = new Rect(0, 0, 0.5f, 0.5f);
                    break; 
                case 3:
                    camera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                    break; 
                default:
                    break;

            }
            pos++;
                    
                    
        }
    }

    private void SpawnPlayer()
    {
        Debug.Log("Spawning thing");
        spawnPoints[index].transform.GetPositionAndRotation(out Vector3 pos, out Quaternion rotastion);
        Gamepad pad = Gamepad.all[index];

        GameObject spawnedPlayer = Instantiate<GameObject>(playerToSpawn);
        spawnedPlayer.transform.SetPositionAndRotation(pos, rotastion);
        Player player = spawnedPlayer.GetComponent<Player>();

        GameObject spawnedCamera = Instantiate<GameObject>(playerCameraRig);
        spawnedCamera.transform.SetPositionAndRotation(pos + SpringArmOffset, rotastion);
        SpringArm springArm = spawnedCamera.GetComponent<SpringArm>();
        springArm.target = spawnedPlayer.transform;
        Camera camera = spawnedCamera.GetComponentInChildren<Camera>();

        if(index == 0) // makes sure only one audio listiner is in the scene 
        {
            spawnedCamera.transform.GetChild(0).AddComponent<AudioListener>();
        }

        player.SetController(pad);
        player.attatchedCamera = camera;
        AddPlayer(player);

    }
}


