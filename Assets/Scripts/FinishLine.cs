using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management or restarting the game

public class FinishLine : MonoBehaviour
{
    [SerializeField] int totalLaps = 3;

    private bool gameIsActive = true;

    public List<GameObject> players;

    public List<Player> MadeItPassTheLine = new();

    public string FinishLineMusic = "EndGame_Music";

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
            MadeItPassTheLine.Add(player);
        }

        if(MadeItPassTheLine.Count >= CrossSceneContainer.PlayersInGame)
        {
            EndRace(MadeItPassTheLine[0]);
        }
    }

    void EndRace(Player player)
    {
        gameIsActive = false;

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        players.AddRange(objectsWithTag);

        int index = 0; 
        foreach(var play in MadeItPassTheLine)
        {
            if(index == 0)
            {
                play.hud.WinnerPlayer("f");
                continue; 
            }

            play.hud.SetPosText(index.ToString());

            //play.EndGame(player.name);

            SpringArm arm = play.springArm;
            arm.target = player.transform;
            arm.useControlRotation = false;

            arm.socketOffset = new Vector3(0, 1, -4);
            arm.transform.rotation = Quaternion.Euler(12, 4, 0);

            index++; 
        }

        if(AudioManager.instance == null)
        {
            return; 
        }

        AudioManager.instance.Play(FinishLineMusic);

        /*
        

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
        */

    }
}

