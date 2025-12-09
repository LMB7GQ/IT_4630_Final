using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Buttons that activate this elevator")]
    public Button[] requiredButtons;

    [Header("Movement")]
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    [Header("Elevator visual")]
    public Renderer elevatorRenderer;
    public Color idleColor = Color.red;
    public Color activeColor = Color.green;

    private Vector3 targetPosition;
    private bool movingToB = false;

    void Start()
    {
        // Start at current position
        targetPosition = transform.position;

        // Set initial color
        if (elevatorRenderer != null)
            elevatorRenderer.material.color = idleColor;
    }

    void Update()
    {
        bool buttonsPressed = AreButtonsPressed();

        // Update color
        if (elevatorRenderer != null)
            elevatorRenderer.material.color = buttonsPressed ? activeColor : idleColor;

        if (buttonsPressed)
        {
            // Set initial target if elevator was frozen
            if (targetPosition == transform.position)
            {
                targetPosition = pointA.position;
                movingToB = true; // after reaching A, go to B
            }

            MoveElevator();
        }
        // Else do nothing, elevator frozen
    }

    void MoveElevator()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Check if reached target
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Switch target
            if (movingToB)
                targetPosition = pointB.position;
            else
                targetPosition = pointA.position;

            movingToB = !movingToB;
        }
    }

    bool AreButtonsPressed()
    {
        foreach (Button b in requiredButtons)
        {
            if (!b.isPressed)
                return false;
        }
        return requiredButtons.Length > 0;
    }
}
