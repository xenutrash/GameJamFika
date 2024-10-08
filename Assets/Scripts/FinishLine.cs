using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management or restarting the game

public class FinishLine : MonoBehaviour
{
    [SerializeField] int totalLaps = 3;  

    private int player1Lap = 0; 
    private int player2Lap = 0;  

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player1"))
        {
            player1Lap++;  
            Debug.Log("Player 1 Lap " + player1Lap);

            if (player1Lap >= totalLaps)
            {
                EndRace(1);
            }
        }
        else if (other.CompareTag("Player2"))
        {
            player2Lap++;  
            Debug.Log("Player 2 Lap " + player2Lap);

            
            if (player2Lap >= totalLaps)
            {
                EndRace(2);  
            }
        }
    }

    void EndRace(int winner)
    {
        Debug.Log("Player " + winner + " wins");
    }
}
