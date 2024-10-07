using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public string playerName; // also mesh name
    public float maxSpeed;
    public float driftModifer;
    public float acceleration;
    public float turnRaduis = 30;
    public float turnRate = 40;
    public float boostMultiplier;
    public GameObject Object;
    
}
