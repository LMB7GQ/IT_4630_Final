using UnityEngine;

public class PlayerMovementMinimal : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float move = 0f;

        if (CompareTag("Player_1"))
            move = Input.GetAxisRaw("Horizontal_WASD");
        else if (CompareTag("Player_2"))
            move = Input.GetAxisRaw("Horizontal_Arrows");

        Vector3 velocity = rb.velocity;
        velocity.x = move * moveSpeed;
        rb.velocity = new Vector3(velocity.x, velocity.y, rb.velocity.z);
    }

    void HandleJump()
    {
        bool jumpPressed = false;

        if (CompareTag("Player_1"))
            jumpPressed = Input.GetKeyDown(KeyCode.W);
        else if (CompareTag("Player_2"))
            jumpPressed = Input.GetKeyDown(KeyCode.UpArrow);

        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    // Ground check via collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}