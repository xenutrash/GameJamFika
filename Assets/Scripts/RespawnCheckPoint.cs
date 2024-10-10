using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RespawnCheckPoint : MonoBehaviour
{

    [SerializeField]
    private Transform respawnPos;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Not a player");
        }

        Player player = other.gameObject.GetComponent<Player>();
        Debug.Log("Setting respawn point for player " + other.gameObject.tag);
        player.respawnPos = respawnPos;

    }
}
