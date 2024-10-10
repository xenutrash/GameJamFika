using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{

    [SerializeField]
    Characters characters;

    List<Button> CharacterButtons = new();
    List<Button> SelectedButton = new();

    private int PlayerInGame = 0; 

    public void StartWithPlayers(int PlayerToStartWith)
    {
        // Do things 
        PlayerInGame = PlayerToStartWith;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // handle per player selectsion 

        for(int i = 0; i < PlayerInGame; i++)
        {
            Gamepad gamepad = Gamepad.all[i];
            if(gamepad.leftStick.value.x > 0.5f)
            {

            }


        }


    }

    private void GenerateGUI()
    {
        foreach(var character in characters.characterContainers)
        {
            // Spawn here 

        }


    }

}
