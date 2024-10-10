using System.Collections.Generic;
using UnityEngine;

public class Twister : MonoBehaviour
{
    public float respawnTime;

    public ParticleSystem p1, p2;

    public bool activated = false;
    public bool swapExecuted = false; // Flag to prevent double swapping
    public float acumulater;
    public CapsuleCollider col;
    public MeshRenderer meshRenderer;
    GameObject player;

    ParticleSystem confetti;

    // Start is called before the first frame update
    void Start()
    {
        acumulater = respawnTime;

        if (col == null)
        {
            col = GetComponent<CapsuleCollider>();
        }
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        confetti = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!meshRenderer.enabled)
        {
            acumulater += Time.deltaTime;
        }

        // Respawn the Twister object after cooldown
        if (acumulater > respawnTime)
        {
            Debug.Log("Respawned");
            col.enabled = true;
            meshRenderer.enabled = true;
            swapExecuted = false; // Reset swap flag when respawned
            activated = false;
            acumulater = 0;
            return;
        }

        // Swap logic only runs when activated and the swap hasn't been executed
        if (!activated || swapExecuted)
        {
            return;
        }

        if (activated && !swapExecuted) // Ensure it only swaps once
        {
            SwapPlayers();
        }
    }

    void SwapPlayers()
    {
        GameObject otherPlayer = GetRandomPlayer(player);

        if (otherPlayer == null)
        {
            activated = false;
            return;
        }

        Vector3 otherPosition = otherPlayer.transform.position;
        Vector3 playerPosition = player.transform.position;

        if (otherPosition != null && playerPosition != null)
        {
            // Swap positions
            player.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            player.transform.position = otherPosition;
            otherPlayer.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            otherPlayer.transform.position = playerPosition;

            // Play particle systems at the swap positions
            p1.transform.position = playerPosition;
            p2.transform.position = otherPosition;

            p1.Play();
            p2.Play();

            swapExecuted = true; // Mark the swap as done
            activated = false; // Reset activation flag
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

        if (collision.gameObject.tag.Contains("Player"))
        {
            player = collision.gameObject;
            activated = true;
            col.enabled = false;
            meshRenderer.enabled = false;
        }

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
