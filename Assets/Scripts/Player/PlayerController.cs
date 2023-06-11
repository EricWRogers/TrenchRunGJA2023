using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public Animator anim;
    public GameObject Graphic;
    public float mouseGravity;
    public float turnFactor = 5f;
    public float speed = 3f;
    public float boostSpeed = 10f;
    public float bankSpeed = 3f;
    public float speedChangeSmoothness = 1f;
    public float rotationSpeed = 1f;
    public float yRotationSpeed = 1f;
    public float acceleration = 5f;
    public float maxBankAngle = 30f;
    public float bankRotationSmoothness = 1f;
    public float rotationReturnSpeed = 1f;
    public float rotateRange;
    public float rollTime = .5f;



    private Rigidbody rb;
    private Vector2 movInput, lookInput;
    // Store the previous position of the object
    private Vector3 previousPosition;
    private Vector2 previousMousePosition;
    private Vector2 smoothedRotation = new Vector2();
    private float currentSpeed;
    private bool boosting;

    public bool rolling;
    private float currentRollTime = 0;

    float intX, intY;

    public void InitializePlayer()
    {
        Debug.Log("Initializing");
        if(GameManager.Instance != null)
        {
            GameManager.Instance.RegisterPlayer(this);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<CombatHandler>().Die();
    }

    private void Start()
    {
        previousPosition = this.transform.position;
    }

    private void Update()
    {
        if (rolling)
        {
            currentRollTime += Time.deltaTime;

            if(currentRollTime >= rollTime)
            {
                rolling = false;
                currentRollTime = 0;
            }
        }
    }

    private void OnDestroy()
    {
    }

    public void UpdateMoveInput(Vector2 _input)
    {
        movInput = _input;
    }

    public void UpdateLookInput(Vector2 _input)
    {
        lookInput = _input;
    }


    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void ToggleBoost(bool _boost)
    {
        boosting = _boost;
        anim.SetBool("Boosting", boosting);
    }

    public void DoABarrelRoll(bool _left)
    {
        rolling = true;

        if (_left)
        {
            anim.SetTrigger("RollLeft");
        }
        else
        {
            anim.SetTrigger("RollRight");
        }
    }


    public void FixedUpdate()
    {
        rb.AddForce(new Vector3(0,0,speed * Time.deltaTime));
    }

    public void Rotation()
    {
       
    }


    public void Movement()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mousePos = lookInput;
        Debug.Log($"Mouse input is {lookInput}");
        Vector2 mouseDelta = mousePos - previousMousePosition;

        float inXMouse = -mouseDelta.x * turnFactor;
        float inYMouse = -mouseDelta.y * turnFactor;

        intX += -inXMouse * mouseGravity * Time.deltaTime;
        intY += inYMouse * mouseGravity * Time.deltaTime;

        smoothedRotation = Vector2.Lerp(smoothedRotation, new Vector2(intX, intY), .5f * Time.deltaTime);

        transform.Rotate(new Vector3(yRotationSpeed * smoothedRotation.y, rotationSpeed * smoothedRotation.x, 0) * Time.deltaTime, Space.Self);
        

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, (-movInput.x + -smoothedRotation.x) * maxBankAngle), bankRotationSmoothness * Time.deltaTime);
        Graphic.transform.rotation = Quaternion.Lerp(Graphic.transform.rotation, Quaternion.Euler(Graphic.transform.rotation.eulerAngles.x, Graphic.transform.rotation.eulerAngles.y, (-movInput.x + -smoothedRotation.x) * maxBankAngle), bankRotationSmoothness * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0), rotationReturnSpeed * Time.deltaTime);

        previousMousePosition = lookInput;

        Vector3 moveVec = movInput;

        Vector3 forwardVector = this.transform.forward;
        Vector3 rightVector = this.transform.right;

        forwardVector.y = 0;
        rightVector.y = 0;

        forwardVector.Normalize();
        rightVector.Normalize();

        if(movInput.y < 0)
        {
            anim.SetBool("Breaking", true);
        }
        else
        {
            anim.SetBool("Breaking", false);
        }

        Vector3 desiredVector = (movInput.y * forwardVector) + ((movInput.x * rightVector) * bankSpeed);
        desiredVector = desiredVector + (this.transform.forward.normalized * 2);

        Vector3 movementVector = desiredVector;

        float desiredSpeed;
        if (boosting)
        {
            desiredSpeed = boostSpeed;
        }
        else
        {
            desiredSpeed = speed;
        }

        currentSpeed = Mathf.Lerp(currentSpeed, desiredSpeed, speedChangeSmoothness * Time.deltaTime);

        Vector3 desiredVelocity = movementVector * currentSpeed;
        Vector3 velocityChange = desiredVelocity - rb.velocity;

        // Limit the change in velocity to avoid exponential acceleration
        velocityChange = Vector3.ClampMagnitude(velocityChange, acceleration * Time.deltaTime);


        // Apply the change in velocity using AddForce
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
        // rb.velocity = (movementVector) * speed;
    }
}
