using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{

    [SerializeField]
    Characters characters;

    public List<CharacterGUI> CharacterGUIs = new();
    public List<Image> PlayerImages = new(); 


    public List<int> PlayerSelectedIndex = new();

    private List<CharacterGUI> SelectedCharacter = new(); 


    private int PlayerInGame = 0;

    float Acumulator = 0;
    float TimeBeforeActAgain = 0.1f; 



    public void StartWithPlayers(int PlayerToStartWith)
    {
        // Do things 
        PlayerInGame = PlayerToStartWith;
        GenerateGUI();
        
       
        for(int i = 0; i < PlayerInGame; i++)
        {
            PlayerSelectedIndex.Add(0);
            SelectedCharacter.Add(null);
            switch (i)
            {
                case 0:
                    PlayerSelectedIndex[0] = 0;
                    PlayerImages[PlayerSelectedIndex[i]].sprite = CharacterGUIs[PlayerSelectedIndex[i]].CharacterImage.sprite;
                    break;
                case 1:
                    PlayerSelectedIndex[1] = 1;
                    PlayerImages[PlayerSelectedIndex[i]].sprite = CharacterGUIs[PlayerSelectedIndex[i]].CharacterImage.sprite;
                    break;
                case 2:
                    PlayerSelectedIndex[2] = 2;
                    PlayerImages[PlayerSelectedIndex[i]].sprite = CharacterGUIs[PlayerSelectedIndex[i]].CharacterImage.sprite;
                    break;
                case 3:
                    PlayerSelectedIndex[3] = 3;
                    PlayerImages[PlayerSelectedIndex[i]].sprite = CharacterGUIs[PlayerSelectedIndex[i]].CharacterImage.sprite;
                    break; 
            }


        }

    }

    
    


    // Start is called before the first frame update
    void Start()
    {
        StartWithPlayers(CrossSceneContainer.PlayersInGame); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // handle per player selectsion 
        Acumulator += Time.deltaTime;
        for(int i = 0; i < PlayerInGame; i++)
        {
            if (Gamepad.all.Count <= i) break; 
            Gamepad gamepad = Gamepad.all[i];
            int SelectedIndex = PlayerSelectedIndex[i];
            

            //Moving left
            if(gamepad.leftStick.value.x >= 0.87 &&  Acumulator >= TimeBeforeActAgain)
            {
              SelectedIndex++; 
            }

            // moving right
            if (gamepad.leftStick.value.x <= -0.87 && Acumulator >= TimeBeforeActAgain)
            {
                SelectedIndex--; 
            }
            Debug.Log(SelectedIndex);

            if (SelectedIndex < 0)
            {
                SelectedIndex = CharacterGUIs.Count - 1;
            }
            else if (SelectedIndex >= CharacterGUIs.Count)
            {
                SelectedIndex = 0;
            }

            if (gamepad.aButton.isPressed)
            {
                SelectedCharacter[i] = CharacterGUIs[SelectedIndex];
                PlayerImages[i].color = Color.green;
                switch (i)
                {
                    case 0:
                        CrossSceneContainer.Player1SelectedCharacter = SelectedCharacter[i].Character.NameOfCharacter;
                        break;
                    case 1:
                        CrossSceneContainer.Player2SelectedCharacter = SelectedCharacter[i].Character.NameOfCharacter;
                        break;
                    case 2:
                        CrossSceneContainer.Player3SelectedCharacter = SelectedCharacter[i].Character.NameOfCharacter;
                        break;
                    case 3:
                        CrossSceneContainer.Player4SelectedCharacter = SelectedCharacter[i].Character.NameOfCharacter;
                        break;
                }



                if (CheckStartCondistion())
                {
                    enabled = false;
                    LoadMainGame();
                    break;
                }
               
            }



            //if (SelectedIndex == PlayerSelectedIndex[i]) continue;
            Debug.Log(PlayerImages.Count);
            Debug.Log(SelectedIndex);
            
            PlayerSelectedIndex[i] = SelectedIndex;
            PlayerImages[i].sprite = CharacterGUIs[SelectedIndex].CharacterImage.sprite; 

        }

        if (Acumulator >= TimeBeforeActAgain)
        {
            Acumulator = 0;
        }


    }

    private void GenerateGUI()
    {
        int Added = 0; 
        foreach(var character in characters.characterContainers)
        {

            if(Added >= CharacterGUIs.Count)
            {
                break; 
            }
            Debug.Log(Added);
            CharacterGUIs[Added].CharacterImage.sprite = character.CharacterImage;
            CharacterGUIs[Added].CharacterText.text = character.NameOfCharacter;
            CharacterGUIs[Added].Character = character;

            Added++;

        }

    }

    private bool CheckStartCondistion()
    {
        foreach(var SelChar in SelectedCharacter)
        {
            if (SelChar == null) return false; 

        }
        return true; 
    }

    private void LoadMainGame()
    {
        SceneManager.LoadScene(1);
    }
}
