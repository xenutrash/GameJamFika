
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
    GameManager gameManager;
    [SerializeField]
    Characters characters;

    int index = 0; 

    private void Start()
    {
        int playersInGame = CrossSceneContainer.PlayersInGame;

        Debug.Log(CrossSceneContainer.PlayersInGame); 

        for(int i = 0; i < playersInGame; i++)
        {
            SpawnPlayer();
            index++;
        }
        UpdateCameraView();
        gameManager = GameManager.GetInstance();
        gameManager.StartGame(this);

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
        Gamepad pad = null; 
        if(Gamepad.all.Count > index)
        {
            pad = Gamepad.all[index];
        }

        GameObject prefabToSpawn = characters.defaultCharacter;
        switch (index)
        {
            case 0:
                prefabToSpawn = characters.GetPrefab(CrossSceneContainer.Player1SelectedCharacter);
                break;
            case 1:
                prefabToSpawn = characters.GetPrefab(CrossSceneContainer.Player2SelectedCharacter);
                break;
            case 2:
                prefabToSpawn = characters.GetPrefab(CrossSceneContainer.Player3SelectedCharacter);
                break;
            case 3:
                prefabToSpawn = characters.GetPrefab(CrossSceneContainer.Player4SelectedCharacter);
                break;
            default:
                break;

        }

        GameObject spawnedPlayer = Instantiate<GameObject>(prefabToSpawn);
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
            Debug.Log("added an audio listiner"); 
        }

        player.SetController(pad);
        player.attatchedCamera = camera;
        player.AllowMovement = false;
        AddPlayer(player);

    }



    public void EnablePlayerMovement()
    {

        foreach(Player player in players)
        {
            player.AllowMovement = true;
        }
    }
}


