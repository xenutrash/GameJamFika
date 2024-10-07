
using UnityEngine;
using UnityEngine.InputSystem; 
public class Player : MonoBehaviour
{

    private Gamepad Controller;
    public PlayerStats Stats;
    public float currentSpeed { get; private set; }
    public bool AllowMovment { get; set; } = false; 

    GameObject playerModel; 
    // Start is called before the first frame update
    void Start()
    {

        // Add The Mesh here 
        playerModel = Instantiate(Stats.Object);
        playerModel.transform.parent = gameObject.transform; 


    }

    // Update is called once per frame
    void Update()
    {
        if (!AllowMovment)
        {
            return; 
        }

        float turn = transform.position.y; 
        if(Controller == null)
        {
            Debug.Log("No controller attatched to object"); 
            return; 
        }

        if (Controller.leftShoulder.value > 0.5)
        {
            // Drift 
        }

        if ( Controller.rightStick.value.x > 0.1f)
        {
            // move left
            turn += (Stats.turnRaduis * Time.deltaTime * Controller.rightStick.value.x);

        }

        if( Controller.rightStick.value.x < -0.1f)
        {
            turn -= (Stats.turnRaduis * Time.deltaTime * Controller.rightStick.value.x);
        }

        // Apply movement
        if (currentSpeed < Stats.maxSpeed)
        {
            currentSpeed += Stats.acceleration * Time.deltaTime;
        }

        transform.position = new(transform.position.x + currentSpeed * Time.deltaTime, turn);

    }


    public void SetOwner(Gamepad gamePad)
    {
        Controller = gamePad; 
    }



}
