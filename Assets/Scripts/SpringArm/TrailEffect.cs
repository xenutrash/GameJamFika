
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class TrailEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    VisualEffect trail;
    public Color driftColor = new Color();
    Vector4 defaultColor;

    void Start()
    {
       player = gameObject.GetComponent<Player>();
       trail = gameObject.GetComponentInChildren<VisualEffect>();

       defaultColor = trail.GetVector4("Base Color");
       
       if(trail == null) {
        Debug.LogWarning("Trail is null");
       }

       if(player == null) {
        Debug.LogWarning("Player is null");
       }
    }

    // Update is called once per frame
    void Update()
    {
        ManageSpawnRate();
        ManageDrifting();
    }

    void ManageSpawnRate() {
        float playerSpeed = player.currentSpeed;
        string spawnRate = "Spawn Rate";

        if(playerSpeed > 20) {
            trail.SetFloat(spawnRate, 32);
            
        } else if(playerSpeed > 10) {
            trail.SetFloat(spawnRate, 16);
        } else if(playerSpeed > 5) {
            trail.SetFloat(spawnRate, 8);
        } else {
            trail.SetFloat(spawnRate, 0);
        }

    }

    void ManageDrifting() {
        trail.GetFloat("Dissolve");
        trail.GetVector4("Base Color");
        
        if(player.isDrifting) {
            trail.SetVector4("Base Color", driftColor);
            trail.SetFloat("Dissolve", 0.3f);
        }
        else {
            trail.SetFloat("Dissolve", 0);
            trail.SetVector4("Base Color", defaultColor);
        }
    }
}
