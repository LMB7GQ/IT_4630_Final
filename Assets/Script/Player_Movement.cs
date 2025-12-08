using UnityEngine;

public class PlayerMovementMinimal : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    private Rigidbody rb;
    private bool isGrounded;

    // Store the last direction for block placement
    private float lastMoveDir = 1f; // 1 = facing right, -1 = facing left

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleFacingDirection();
    }

    void HandleMovement()
    {
        float move = 0f;

        if (CompareTag("Player_1"))
            move = Input.GetAxisRaw("Horizontal_WASD");
        else if (CompareTag("Player_2"))
            move = Input.GetAxisRaw("Horizontal_Arrows");

        // Store last direction (only if moving)
        if (move != 0)
            lastMoveDir = Mathf.Sign(move);

        Vector3 velocity = rb.velocity;
        velocity.x = move * moveSpeed;
        rb.velocity = new Vector3(velocity.x, velocity.y, rb.velocity.z);
    }

    void HandleFacingDirection()
    {
        // Flip the player by rotating Y
        if (lastMoveDir == 1)
            transform.rotation = Quaternion.Euler(0, 0, 0);      // Facing right
        else if (lastMoveDir == -1)
            transform.rotation = Quaternion.Euler(0, 180, 0);    // Facing left
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            isGrounded = false;
    }

    // Helper for other scripts (like lego box placement)
    public float GetFacingDirection()
    {
        return lastMoveDir;
    }
}
