using System;
using System.Collections;
using System.Collections.Generic;
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
    private List<bool> _CanMoveController = new( new bool[10]);

    private List<CharacterGUI> SelectedCharacter = new(); 



    private int PlayerInGame = 0;

    float Acumulator = 0;
    float TimeBeforeActAgain = 0.2f; 



    public void StartWithPlayers(int PlayerToStartWith)
    {
        // Do things 
        PlayerInGame = PlayerToStartWith;
        GenerateGUI();
       


        for (int i = 0; i < PlayerInGame; i++)
        {
            PlayerSelectedIndex.Add(i);
            _CanMoveController[i] = true;
            StartCoroutine(WaitForInputForController(i, 0.3f));
            SelectedCharacter.Add(null);
            PlayerImages[PlayerSelectedIndex[i]].sprite = CharacterGUIs[PlayerSelectedIndex[i]].CharacterImage.sprite;
        }

    }

   
    // Start is called before the first frame update
    void Start()
    {
        StartWithPlayers(CrossSceneContainer.PlayersInGame); 
    }

    // Update is called once per frame
    void Update()
    {
        // handle per player selectsion 
        Acumulator += Time.deltaTime;
        for(int i = 0; i < PlayerInGame; i++)
        {
            if (Gamepad.all.Count <= i) break; 
            Gamepad gamepad = Gamepad.all[i];
 
            int SelectedIndex = PlayerSelectedIndex[i];
          
            if(_CanMoveController.Count < i)
            {
                Debug.Log("something is cringe"); 
                continue; 
            }

            if (!_CanMoveController[i])
            {
                Debug.Log("The controller is not allowed to move "); 
                continue; 
            }
           
            bool hasMoved = false;

            //Moving left
            if(gamepad.leftStick.value.x >= 0.87 )
            {
              SelectedIndex++; 
                hasMoved = true;
            }

            // moving right
            if (gamepad.leftStick.value.x <= -0.87 )
            {
                SelectedIndex--; 
                hasMoved = true;
            }
           

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
                hasMoved = true;
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

            if (hasMoved)
            {
                StartCoroutine(WaitForInputForController(i));
            }



            //if (SelectedIndex == PlayerSelectedIndex[i]) continue;            
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

    IEnumerator WaitForInputForController(int index, float TimeToWait = 0.4f)
    {
        if( _CanMoveController.Count < index)
        {
            Debug.Log("No player with that index"); 
            yield break;  
        }
        if (_CanMoveController[index] == false)
        {
            yield break; 
        }
  
        _CanMoveController[index] = false;
        yield return new WaitForSeconds(TimeToWait);

        _CanMoveController[index] = true;

    }

}
