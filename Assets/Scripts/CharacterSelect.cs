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

    List<int> PlayerSelectedIndex = new();

    List<Image> PlayerSelectImage = new(); 

    private int PlayerInGame = 0; 

    public void StartWithPlayers(int PlayerToStartWith)
    {
        // Do things 
        PlayerInGame = PlayerToStartWith;
        GenerateGUI(); 
        for(int i = 0; i < PlayerInGame; i++)
        {
            switch (i)
            {
                case 0:
                    PlayerSelectedIndex[0] = 0;
                    break;
                case 1:
                    PlayerSelectedIndex[1] = CharacterButtons.Count / 2;
                    break;
                case 2:
                    PlayerSelectedIndex[2] = CharacterButtons.Count / 2;
                    break;
                case 3:
                    PlayerSelectedIndex[3] = CharacterButtons.Count -1;
                    break; 
            }


        }
        enabled = true; 
    }

    


    // Start is called before the first frame update
    void Start()
    {
        enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        // handle per player selectsion 

        for(int i = 0; i < PlayerInGame; i++)
        {
            Gamepad gamepad = Gamepad.all[i];
            int SelectedIndex = PlayerSelectedIndex[i];
            //Moving left
            if(gamepad.leftStick.value.x > 0.5f)
            {
                if(SelectedIndex == 0)
                {
                    SelectedIndex = CharacterButtons.Count - 1;
                }else

                if(SelectedIndex >= CharacterButtons.Count)
                {
                    SelectedIndex = 0; 
                }else 
                {
                    SelectedIndex++; 
                }

            }
            // moving right
            if (gamepad.leftStick.value.x > -0.5f)
            {
                if (SelectedIndex == 0)
                {
                    SelectedIndex = CharacterButtons.Count - 1;
                }
                else

                if (SelectedIndex >= CharacterButtons.Count)
                {
                    SelectedIndex = 0;
                }
                else
                {
                    SelectedIndex--;
                }

            }


            SelectedButton[i] = CharacterButtons[SelectedIndex];
            PlayerSelectedIndex[i] = SelectedIndex;
            PlayerSelectImage[i].transform.position = SelectedButton[i].transform.position; 

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
