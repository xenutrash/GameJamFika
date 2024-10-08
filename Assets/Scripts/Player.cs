using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public PlayerStats Stats;
    public float currentSpeed { get; private set; }
    public bool AllowMovement { get; set; } = false;
    private Rigidbody rb;
    public CamerController camerController;

    private float speedBoost = 0;
    private bool isDrifting = false;
    private float originalTurnRate;

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
        if (!AllowMovement)
        {
            return;
        }

        float turn = GetKeyboardInput();

        if (!isDrifting)
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

        Vector3 vel = transform.forward * currentSpeed;
        vel.y = rb.velocity.y;
        rb.velocity = vel;

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            turn,
            transform.eulerAngles.z
        );

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartDrifting();
        }
        if((Input.GetKeyUp(KeyCode.K)))
        {
            StopDrifting();
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

    void StartDrifting()
    {
        if (!isDrifting)
        {
            isDrifting = true;
            Stats.turnRate *= 2f;
            currentSpeed *= 0.9f;

            rb.drag = rb.drag * 0.5f; 
        }
    }

    void StopDrifting()
    {
        if (isDrifting)
        {
            isDrifting = false;
            Stats.turnRate = originalTurnRate;
            rb.drag = 3.0f;

        }
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
