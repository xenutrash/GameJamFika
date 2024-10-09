
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
        int playersInGame = CrossSceneContainer.PlayersInGame;

        if(playersInGame > Gamepad.all.Count)
        {
            playersInGame = Gamepad.all.Count;
        }

        for(int i = 1; i < playersInGame; i++)
        {
            SpawnPlayer();
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

        if(cameras.Count == 2)
        {
            cameras[0].rect = new(0,0, 0.5f,1);
            cameras[1].rect = new(0, 0.5f, 0.5f, 1); 
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
            /*
                // Top left
                cam1.rect = new Rect(0, 0.5f, 0.5f, 0.5f);

                // Top right
                cam2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);

                // Bottom left
                cam3.rect = new Rect(0, 0, 0.5f, 0.5f);

                // Bottom right
                cam4.rect = new Rect(0.5f, 0, 0.5f, 0.5f);



                continue; 
         Debug.Log("Camera lol");
         float x = 0.5f;

         if(pos % 2 !=  1)
         {
          x = 0; 
         }


                        camera.rect = new(
                           x ,y,
                            pos > 1 ? 0 : 0.5f,pos > 2 ? 0.5f : 1);
            */

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


