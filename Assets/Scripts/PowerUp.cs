
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PowerUp : MonoBehaviour
{
    

    public float durastion;
    public float speedModifier;
    public float respawnTime;

    Player targetPlayer;
    bool activated = false;
    float acumulater;
    public CapsuleCollider col;
    public MeshRenderer meshRenderer;

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

        acumulater += Time.deltaTime;
        if (acumulater > durastion)
        {
            
            targetPlayer.SetSpeedBoost(speedModifier, false);
        }

        if (acumulater > respawnTime)
        {
            col.enabled = true;
            acumulater = 0;
            activated = false;
            meshRenderer.enabled = true; 
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Contains("Player"))
        {
            Debug.Log("not a player"); 
            return; 
        }

        if(!collision.gameObject.TryGetComponent<Player>(out targetPlayer))
        {
            Debug.Log("No player attatched to the player");
            return; 
        }

        activated = true;
        targetPlayer.SetSpeedBoost(speedModifier);
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
