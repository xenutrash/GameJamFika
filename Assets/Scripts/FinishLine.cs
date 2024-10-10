using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management or restarting the game

public class FinishLine : MonoBehaviour
{
    [SerializeField] int totalLaps = 3;

    private int player1Lap = 0;
    private int player2Lap = 0;
    private int player3Lap = 0;
    private int player4Lap = 0;
    private bool gameIsActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (gameIsActive)
        {

            if (other.CompareTag("Player"))
            {
                Player playerScript = other.GetComponent<Player>();

                switch (playerScript.name)
                {
                    case "Player1":
                        player1Lap += 1;
                        CheckIfWinner(playerScript, player1Lap);
                        break;

                    case "Player2":
                        player2Lap += 1;
                        CheckIfWinner(playerScript, player2Lap);
                        break;

                    case "Player3":
                        player3Lap += 1;
                        CheckIfWinner(playerScript, player3Lap);
                        break;

                    case "Player4":
                        player4Lap += 1;
                        CheckIfWinner(playerScript, player4Lap);
                        break;
                }
            }
        }
    }

    private void CheckIfWinner(Player player, int lap)
    {
        if (lap >= totalLaps)
        {
            EndRace(player);
        }
    }

    void EndRace(Player player)
    {
        gameIsActive = false;
        if (PauseMenu.GetInstance() != null)
        {
            PauseMenu.GetInstance().SetVisabability(true);
            Time.timeScale = 0;

            if (AudioManager.instance != null)
            {
                AudioManager.instance.Pause();
            }
        }
    }
}

