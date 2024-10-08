
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    private Gamepad Controller;
    public PlayerStats Stats;
    
    public float currentSpeed { get; private set; }
    public bool AllowMovement { get; set; } = false;
    private Rigidbody rb;
    public CamerController camerController;

    public float turnRate;
    private bool isDrifting;
    [SerializeField] private float extraDrift;

    float speedBoost = 0; 

    GameObject playerModel; 
    // Start is called before the first frame update
    void Start()
    {

        // Add The Mesh here 
        //playerModel = Instantiate(Stats.Object);
        //playerModel.transform.parent = gameObject.transform;
        AllowMovement = true;
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
        //camerController.SetTarget(transform);


    }

    // Update is called once per frame
    void Update()
    {
        if (!AllowMovement)
        {
            return; 
        }

        if (isDrifting)
        {
            turnRate = Stats.turnRate + extraDrift;
           
        }
        else
        {
            turnRate = Stats.turnRate;
        }

        float turn = Controller == null ? GetKeyboardInput() : GetControllerInput();

        // Apply movement
        if (currentSpeed >= Stats.maxSpeed + speedBoost)
        {
            if (isDrifting)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, Stats.maxSpeed + speedBoost - 3, Stats.acceleration + speedBoost * Time.deltaTime);
            }
            else
            {
               currentSpeed = Mathf.Lerp(currentSpeed, Stats.maxSpeed + speedBoost, Stats.acceleration + speedBoost * Time.deltaTime);
            }
        }

        if (currentSpeed < Stats.maxSpeed + speedBoost)
        {
            if (isDrifting)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, Stats.maxSpeed + speedBoost - 3, Stats.acceleration + speedBoost * Time.deltaTime);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, Stats.maxSpeed + speedBoost, Stats.acceleration + speedBoost * Time.deltaTime);
            }
        }

        Vector3 vel = transform.forward * currentSpeed; 
        vel.y = rb.velocity.y;
        rb.velocity = vel;
        //transform.position = new(transform.position.x, transform.position.y, transform.position.z + currentSpeed * Time.deltaTime);
        
        transform.transform.eulerAngles = new Vector3(
        transform.eulerAngles.x,
        turn,
        transform.eulerAngles.z
    );




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
            turn += (turnRate * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            turn -= (turnRate * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            isDrifting = true;
        }

        if(Input.GetKeyUp(KeyCode.K)){
            isDrifting = false;
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

    public void SetSpeedBoost(float speedBoost, bool apply = true)
    {
      
        if (!apply)
        {
            this.speedBoost = 0;
            return; 
        }
        Debug.Log("SpeedBoost yay");
        this.speedBoost = speedBoost * Stats.boostMultiplier; 

    }

}
