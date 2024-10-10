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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(respawnPos.position, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(respawnPos.position + respawnPos.forward * 2, 0.5f);
    }
}
