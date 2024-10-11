using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management or restarting the game

public class FinishLine : MonoBehaviour
{
    [SerializeField] int totalLaps = 3;

    private bool gameIsActive = true;

    public List<GameObject> players;

    private void OnTriggerEnter(Collider other)
    {
        if (gameIsActive)
        {

            if (other.CompareTag("Player"))
            {
                Player playerScript = other.GetComponent<Player>();

                playerScript.IncreaseLaps(totalLaps);

                CheckIfWinner(playerScript);
            }
        }
    }

    private void CheckIfWinner(Player player)
    {
        if (player.GetLaps() > totalLaps)
        {
            EndRace(player);
        }
    }

    void EndRace(Player player)
    {
        gameIsActive = false;

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        players.AddRange(objectsWithTag);

        for (int i = 0; i < players.Count; i++)
        {

            Player playerCompare = players[i].GetComponent<Player>();

            if (playerCompare != player)
            {
                playerCompare.EndGame(player.name);

                SpringArm arm = playerCompare.springArm;
                arm.target = player.transform;
                arm.useControlRotation = false;

                arm.socketOffset = new Vector3(0, 1, -4);
                arm.transform.rotation = Quaternion.Euler(12, 4, 0);
            }
           
        }

    }
}

