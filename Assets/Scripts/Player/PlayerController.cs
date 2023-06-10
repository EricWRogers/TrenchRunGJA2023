using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public float mouseGravity;
    public float speed = 3f;
    public float rotationSpeed = 1f;
    public float rotationReturnSpeed = 1f;
    public float rotateRange;


    private Vector2 movInput, lookInput;
    // Store the previous position of the object
    private Vector3 previousPosition;

    float intX, intY;

    public void InitializePlayer()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.RegisterPlayer(this);
        }
    }

    private void Awake()
    {
        GameManager.onGMReady += InitializePlayer;

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        previousPosition = this.transform.position;
    }

    private void OnDestroy()
    {
        GameManager.onGMReady -= InitializePlayer;
    }

    public void UpdateMoveInput(Vector2 _input)
    {
        movInput = _input;
    }

    public void UpdateLookInput(Vector2 _input)
    {
        lookInput = _input;
    }


    public void FixedUpdate()
    {
        Movement();
        //Rotation();
    }

    public void Rotation()
    {
       
    }


    public void Movement()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mousePos = new Vector2(lookInput.x, lookInput.y);
        float distanceFromCenter = Vector2.Distance(mousePos, screenCenter) / 100;
        float inXMouse = -(screenCenter.x - mousePos.x) / 700;
        float inYMouse = -(screenCenter.y - mousePos.y) / 700;

        intX = Mathf.Lerp(intX, inXMouse, distanceFromCenter * mouseGravity);
        intY = Mathf.Lerp(intY, -inYMouse, distanceFromCenter * mouseGravity);

        if (intX > rotateRange)
        {
            transform.Rotate(new Vector3(0, rotationSpeed * intX, 0) * Time.deltaTime, Space.Self);
        }
        if (intX < -rotateRange)
        {
            transform.Rotate(new Vector3(0, -rotationSpeed * -intX, 0) * Time.deltaTime, Space.Self);
        }
        if (intY > rotateRange)
        {
            transform.Rotate(new Vector3(rotationSpeed * intY, 0, 0) * Time.deltaTime, Space.Self);
        }
        if (intY < -rotateRange)
        {
            transform.Rotate(new Vector3(-rotationSpeed * -intY, 0, 0) * Time.deltaTime, Space.Self);
        }


        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0), rotationReturnSpeed * Time.deltaTime);


        Vector3 moveVec = movInput;

       // transform.forward = lookInput;

        rb.velocity = (transform.forward + moveVec) * speed;
    }
}
