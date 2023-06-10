using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public float speed = 3f;

    private Vector2 movInput, lookInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

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
    }


    public void Movement()
    {
        Vector3 moveVector = new Vector3();

        moveVector.x = movInput.x * speed;
        moveVector.y = movInput.y * speed;
        moveVector.z = speed;
        //rb.AddForce(new Vector3(0,0,speed * Time.deltaTime));
        rb.velocity = moveVector * Time.deltaTime;
    }
}
