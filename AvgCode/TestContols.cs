using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    public float jumpForce = 5f; // Adjust this to change jump height
    private Rigidbody rb;
    private Vector2 moveInput;
    private bool shouldJump = false;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        // Setting interpolation to 'Interpolate' makes movement smoother
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Jump input should always be caught in Update so you don't miss the key press
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            shouldJump = true;
        }
    }

    void FixedUpdate()
    {
        // 1. Handle Horizontal Movement
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        Vector3 velocity = rb.linearVelocity;

        // Apply speed while preserving the current vertical (y) velocity
        velocity.x = moveDir.x * speed;
        velocity.z = moveDir.z * speed;

        if (moveDir.magnitude < 0.1f)
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }

        // 2. Handle Jumping
        if (shouldJump)
        {
            // Apply upward force. Impulse is best for a sudden jump.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            shouldJump = false;
        }
        else
        {
            // Apply the velocity back to the Rigidbody
            // We only set it here if we aren't jumping to avoid "clamping" the jump force
            rb.linearVelocity = velocity;
        }

        rb.angularVelocity = Vector3.zero;
    }

    // 3. Simple Ground Check
    // This prevents "infinite jumping" in the air.
    void OnCollisionStay(Collision collision)
    {
        // Check if we are touching the floor (or any static geometry)
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}

