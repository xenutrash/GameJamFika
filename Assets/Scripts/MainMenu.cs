using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class MainMenu : MonoBehaviour {
    // [SerializeField] AudioClip[] soundClips;
    // [SerializeField] AudioSource audioSource;
    public Button defaultButton;
    public Button[] playerButtons;
    public GameObject selectMenu;
    public GameObject mainMenu; 
   
    public void PlayGame() {

        

    }


    public void StartButton()
    {
        mainMenu.SetActive(false);
        selectMenu.SetActive(true);

        int AmountOfControllers = Gamepad.all.Count;

        if(AmountOfControllers > playerButtons.Length)
        {
            AmountOfControllers = playerButtons.Length;

        }
        for (int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].enabled = false;
        }

        for (int i = 0; i < AmountOfControllers; i++)
        {
            playerButtons[i].enabled = true;
        }
    }

    public void BackButton()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].enabled = false;
        }
        selectMenu.SetActive(false);
        mainMenu.SetActive(true);
       
    }

    public void SetPlayerCharacter(int playerIndex, string nameOfCharacter)
    {
        switch (playerIndex)
        {
            case 1:
                CrossSceneContainer.Player1SelectedCharacter = nameOfCharacter;
                break;
            case 2:
                CrossSceneContainer.Player2SelectedCharacter = nameOfCharacter;
                break;
            case 3:
                CrossSceneContainer.Player3SelectedCharacter = nameOfCharacter;
                break;
            case 4:
                CrossSceneContainer.Player4SelectedCharacter = nameOfCharacter;
                break; 
        }
        
        

    }


    public void StartWithPlayers(int players)
    {

        CrossSceneContainer.PlayersInGame = players; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Quit"); //Added this message bcs this wont work in unity
    }

   void Start()
    {
        if(defaultButton == null)
        {
            Debug.Log("No default button added"); 
            return; 
        }
        defaultButton.Select();
    }


    

    // public void PlayRandomSound() {
    //     if (soundClips.Length > 0) {
    //         // Slumpa fram ett index i soundClips-arrayen
    //         int randomIndex = Random.Range(0, soundClips.Length);

    //         // Hämta ett slumpat ljudklipp
    //         AudioClip clip = soundClips[randomIndex];

    //         // Spela ljudet
    //         audioSource.PlayOneShot(clip);
    //     }
    //     else {
    //         Debug.LogError("Inga ljudklipp är tillagda i soundClips-arrayen.");
    //     }
    // }
}