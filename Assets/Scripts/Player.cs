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

    public Transform respawnPos = null;

    private float speedBoost = 0;
    private bool isDrifting = false;
    private float originalTurnRate;
    public Animator animator;

    bool Taunting = false;

    public float driftFactor = 0.9f;
    public float driftDrag = 2f;
    public float normalDrag = 0.05f;

    public string name;

    // Start is called before the first frame update
    void Start()
    {
        //AllowMovement = true;
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }

        originalTurnRate = Stats.turnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!AllowMovement)
        {
            return;
        }

        float turn = controller == null ? GetKeyboardInput() : GetControllerInput();

        if (isDrifting)
        {
            ReduceDriftSpeed();
            if (Input.GetKey(KeyCode.D) || controller.leftStick.value.x > 0.1)
            {
                Vector3 driftForce = -transform.right * rb.velocity.magnitude * driftFactor;
                driftForce.y = 0;
                rb.AddForce(driftForce, ForceMode.Acceleration);
            }
            else if (Input.GetKey(KeyCode.A) || controller.leftStick.value.x < -0.1)
            {
                Vector3 driftForce = transform.right * rb.velocity.magnitude * driftFactor;
                driftForce.y = 0;
                rb.AddForce(driftForce, ForceMode.Acceleration);
            }
        }
        else
        {
            if ((Input.GetKey(KeyCode.W) || IsAButtonPressed()))
            {
                Accelerate();
            }

            if (Input.GetKey(KeyCode.S) || IsBButtonPressed())
            {
                DeAccelerate();
            }
        }


        Vector3 vel = transform.forward * currentSpeed;
        vel.y = rb.velocity.y;
        rb.velocity = vel;

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            turn,
            transform.eulerAngles.z);



        if (controller != null)
        {
            if (controller.rightTrigger.isPressed)
            {
                isDrifting = true;
            }
            else
            {
                isDrifting = false;
            }

            if (controller.yButton.isPressed && !Taunting)
            {
                PlayTaunt();
                Taunting = true;
            }
            else if (!controller.yButton.isPressed)
            {
                Taunting = false;
            }

            if (controller.startButton.isPressed)
            {
                Pause();
            }

            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            isDrifting = true;
        }
        if ((Input.GetKeyUp(KeyCode.K)))
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

        if (controller.leftStick.value.x > 0.1)
        {
            turn += (Stats.turnRate * Time.deltaTime * controller.leftStick.value.x);
        }

        if (controller.leftStick.value.x < -0.1)
        {
            turn -= (Stats.turnRate * Time.deltaTime * controller.leftStick.value.x * -1);
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
        if (this.speedBoost != 0)
        {
            return;
        }

        PlayAudioOneShoot(Stats.BoostSound);
        if (animator != null)
        {
            animator.SetTrigger("boost");
        }

        Debug.Log("SpeedBoost yay");
        this.speedBoost = speedBoost * Stats.boostMultiplier;

    }

    public void SetController(Gamepad pad)
    {
        controller = pad;
    }

    public void PlayAudioOneShoot(string audioToPlay)
    {
        if (AudioManager.instance == null)
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
        if (animator != null)
        {
            animator.SetTrigger("boost");
        }



    }

    private bool IsAButtonPressed()
    {
        if (controller == null) return false;
        return controller.aButton.IsPressed();
    }

    private bool IsBButtonPressed()
    {
        if (controller == null) return false;
        return controller.bButton.IsPressed();
    }

    private void Accelerate()
    {
        if (currentSpeed >= Stats.maxSpeed + speedBoost)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, Stats.maxSpeed + speedBoost, Stats.acceleration * Time.deltaTime);
        }

        if (currentSpeed < Stats.maxSpeed + speedBoost)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, Stats.maxSpeed + speedBoost, Stats.acceleration * Time.deltaTime);
        }

    }

    private void DeAccelerate()
    {

        currentSpeed = Mathf.Lerp(currentSpeed, 0, Stats.acceleration * Time.deltaTime);

    }

    private void ReduceDriftSpeed()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, (Stats.maxSpeed + speedBoost) * 0.7f, Stats.acceleration * Time.deltaTime);
    }


    private void Pause()
    {

        if (PauseMenu.GetInstance() != null)
        {
            PauseMenu.GetInstance().SetVisabability(true);
            Time.timeScale = 0;

            if (AudioManager.instance != null)
            {
                AudioManager.instance.Pause();
            }
        }
    }

        public void Repspawn()
        {
            // resets vel
            rb.velocity = Vector3.zero;
            transform.SetPositionAndRotation(respawnPos.position, respawnPos.rotation);
            PlayAudioOneShoot(Stats.LastPlaceSound);
            currentSpeed = 0;
        }

    

}
