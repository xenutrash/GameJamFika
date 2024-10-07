
using UnityEngine;
using UnityEngine.InputSystem; 
public class Player : MonoBehaviour
{

    private Gamepad Controller;
    public PlayerStats Stats;
    public float currentSpeed { get; private set; }
    public bool AllowMovement { get; set; } = false; 

    GameObject playerModel; 
    // Start is called before the first frame update
    void Start()
    {

        // Add The Mesh here 
        //playerModel = Instantiate(Stats.Object);
        //playerModel.transform.parent = gameObject.transform;
        AllowMovement = true; 
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!AllowMovement)
        {
            return; 
        }

        float turn = Controller == null ? GetKeyboardInput() : GetControllerInput();

        transform.position = new(transform.position.x, transform.position.y, transform.position.z + currentSpeed * Time.deltaTime);
        transform.transform.eulerAngles = new Vector3(
        transform.eulerAngles.x,
        turn,
        transform.eulerAngles.z
    );

        // Apply movement
        if (currentSpeed < Stats.maxSpeed)
        {
            currentSpeed += Stats.acceleration * Time.deltaTime;
        }


    }


    public void SetOwner(Gamepad gamePad)
    {
        Controller = gamePad; 
    }


    float GetKeyboardInput()
    {

        float turn = transform.eulerAngles.y;
        if (Input.GetKey(KeyCode.D))
        {
            turn += (Stats.turnRate * Time.deltaTime);
            Debug.Log(turn);
            if (turn > Stats.turnRaduis && turn < 360 - Stats.turnRaduis - 3)
            {
                turn = Stats.turnRaduis; 
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            turn -= (Stats.turnRate * Time.deltaTime);
            Debug.Log(turn); 
            if(turn < 360 - Stats.turnRaduis && turn > 360 - Stats.turnRaduis -3)
            {
                turn = Stats.turnRaduis * -1; 
            }

        }

        return turn; 
    }


    float GetControllerInput()
    {
        float turn = transform.localPosition.y;

        if (Controller.leftShoulder.value > 0.5)
        {
            // Drift 
        }

        if (Controller.rightStick.value.x > 0.1f)
        {
            // move left
            turn += (Stats.turnRate * Time.deltaTime * Controller.rightStick.value.x);

        }

        if (Controller.rightStick.value.x < -0.1f)
        {
            turn -= (Stats.turnRate * Time.deltaTime * Controller.rightStick.value.x);
        }

        return turn; 
    }

}
