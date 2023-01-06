using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    Rigidbody rb;
    public float jumpForce;
    bool readyToJump;
    float horizontalInput = 0f;
    float verticalInput = 0f;
    public float raycastRange;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Move();
        if(readyToJump && grounded)
        {
          Jump();
        }
    }
    // Update is called once per frame
    void Update()
    {
        Inputs();
        GroundedCheck();
    }
    private void Move()
    {
        Vector3 targetVelocity = new Vector3(horizontalInput, 0, verticalInput);
        targetVelocity = targetVelocity.normalized;
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;
        Vector3 velocityChange = targetVelocity - rb.velocity;
        velocityChange.y = 0;
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }
    private void GroundedCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, raycastRange * 0.5f + 0.2f);
    }
    private void Inputs()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        readyToJump = Input.GetKey(KeyCode.Space);
    }
}
