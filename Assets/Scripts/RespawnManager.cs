using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RespawnManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Not a player");
        }

        Player player = other.gameObject.GetComponent<Player>();
        Debug.Log("Respawning player " + other.gameObject.tag);
        player.Repspawn();

    }
}
