
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(CapsuleCollider))]
public class Twister : MonoBehaviour
{
    public float respawnTime;

    bool activated = false;
    float acumulater;
    public CapsuleCollider col;
    public MeshRenderer meshRenderer;
    GameObject player;

    ParticleSystem confetti;

    // Start is called before the first frame update
    void Start()
    {
        if(col == null)
        {
            col = GetComponent<CapsuleCollider>(); 
        }
        if(meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>(); 
        }
       confetti = GetComponentInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            return; 
        }

        if (activated)
        {
            GameObject otherPlayer = GetRandomPlayer(player);

            if (otherPlayer == null)
            {
                activated = false;
                return;
            }

            Vector3 other = otherPlayer.transform.position;
            Vector3 here = player.transform.position;

            if (other != null && here != null)
            {
                player.transform.position = other;
                otherPlayer.transform.position = here;
                
                activated = false;
            }
        }

        acumulater += Time.deltaTime;

        if (acumulater > respawnTime)
        {
            Debug.Log("Respawned");
            col.enabled = true;
            acumulater = 0;
            meshRenderer.enabled = true; 
        }
    }

    GameObject GetRandomPlayer(GameObject excludedPlayer)
    {
        // Get the list of players from the SplitScreenManager
        List<Player> players = GameObject.Find("SplitScreenManager").GetComponent<SplitScreenManager>().players;

        // Make sure there are at least two players to choose from
        if (players.Count <= 1)
        {
            Debug.LogWarning("Not enough players to choose from.");
            return null;
        }

        GameObject randomPlayer = null;

        // Keep trying to find a different player, excluding the player that entered the collider
        do
        {
            int randomIndex = Random.Range(0, players.Count);
            randomPlayer = players[randomIndex].gameObject;
        }
        while (randomPlayer == excludedPlayer); // Exclude the colliding player

        return randomPlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the "Player" tag
        if (!collision.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Not a player");
            return;
        }

        // Check if the colliding object has a Player component
        if (!collision.gameObject.TryGetComponent<Player>(out Player targetPlayer))
        {
            Debug.Log("No Player component attached to the object with Player tag.");
            return;
        }

        player = collision.gameObject;
        activated = true;
        col.enabled = false;
        meshRenderer.enabled = false;

        try
        {
            confetti.Play();
        }
        catch (System.Exception)
        {
            return;
        }
    }
}
