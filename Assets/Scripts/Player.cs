using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public PlayerStats Stats;
    public float currentSpeed { get; private set; }
    public bool AllowMovement { get; set; } = false;
    private Rigidbody rb;
    public Camera attatchedCamera;
    Gamepad controller; 

    private float speedBoost = 0;
    private bool isDrifting = false;
    private float originalTurnRate;
    public Animator animator;

    bool Taunting = false;

    public float turnRate = 100f;   
    public float driftTurnRate = 50f; 
    public float driftFactor = 0.9f;  
    public float driftDrag = 2f;      
    public float normalDrag = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        AllowMovement = true;
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }

        originalTurnRate = Stats.turnRate;
    }

    // Update is called once per frame
    void Update()
    {

        float currentTurnRate = isDrifting ? driftTurnRate : turnRate;
        if (!AllowMovement)
        {
            return;
        }

       // float turn = controller == null ? GetKeyboardInput() : GetControllerInput();

            if (currentSpeed >= Stats.maxSpeed + speedBoost)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, Stats.maxSpeed + speedBoost, Stats.acceleration * Time.deltaTime);
            }

            if (currentSpeed < Stats.maxSpeed + speedBoost)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, Stats.maxSpeed + speedBoost, Stats.acceleration * Time.deltaTime);
            }

        float turn = transform.eulerAngles.y;
        if (Input.GetKey(KeyCode.D)) 
        {
            turn += (currentTurnRate * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            turn -= (currentTurnRate * Time.deltaTime);
        }

        Vector3 vel = transform.forward * currentSpeed;
        vel.y = rb.velocity.y;
        rb.velocity = vel;

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            turn,
            transform.eulerAngles.z
        );

       //Drifting
        if (isDrifting)
        {


            //Kanske behövs göras så man checkar vilket håll man driftar åt

            Vector3 driftForce = -transform.right * rb.velocity.magnitude * driftFactor;
           // rb.AddForce(driftForce, ForceMode.Acceleration);


            //Nytt som ska provas
            driftForce.y = rb.velocity.y;
            rb.velocity = driftForce;

            
            rb.drag = driftDrag;
        }
        else
        {
            
            rb.drag = normalDrag;
        }




        if (controller != null)
        {
            if (controller.yButton.isPressed && !Taunting)
            {
                PlayTaunt();
                Taunting = true; 
            }
            else if(!controller.yButton.isPressed)
            {
                Taunting = false;
            }

            if (controller.startButton.isPressed)
            {
                if(PauseMenu.GetInstance() != null)
                {
                    PauseMenu.GetInstance().SetVisabability(true);
                    Time.timeScale = 0;

                    if(AudioManager.instance != null)
                    {
                        AudioManager.instance.Pause();
                    }
                }
            }

            if (controller.rightTrigger.IsPressed())
            {
                isDrifting = true;
            }else 
            {
                isDrifting = false;
            }

            return; 
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            isDrifting = true;
        }
        if((Input.GetKeyUp(KeyCode.K)))
        {
            isDrifting = false;
        }
    }

   

    float GetKeyboardInput()
    {
        float turn = transform.eulerAngles.y;

        if (Input.GetKey(KeyCode.D))
        {
            turn += (Stats.turnRate * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            turn -= (Stats.turnRate * Time.deltaTime);
        }

        return turn;
    }

    float GetControllerInput()
    {
        float turn = transform.eulerAngles.y;

        if(controller.leftStick.value.x > 0.1)
        {
            turn += (Stats.turnRate * Time.deltaTime * controller.leftStick.value.x);
        }

        if (controller.leftStick.value.x < -0.1)
        {
            turn -= (Stats.turnRate * Time.deltaTime * controller.leftStick.value.x *-1);
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
        // prevents boosts from being multipiled 
        if(this.speedBoost != 0)
        {
            return; 
        }

        PlayAudioOneShoot(Stats.BoostSound);
        animator.SetTrigger("boost");
        Debug.Log("SpeedBoost yay");
        this.speedBoost = speedBoost * Stats.boostMultiplier;
        
    }

    public void SetController(Gamepad pad)
    {
        controller = pad; 
    }

    public void PlayAudioOneShoot(string audioToPlay)
    {
        if(AudioManager.instance == null)
        {
            Debug.Log("No audio manager in scene"); 
            return; 
        }
        Debug.Log("Trying to play sound now"); 
        AudioManager.instance.PlayOneShot(audioToPlay); 
    }

    private void PlayTaunt()
    {
        PlayAudioOneShoot(Stats.tauntSound);
        animator.SetTrigger("boost");


    }

}
