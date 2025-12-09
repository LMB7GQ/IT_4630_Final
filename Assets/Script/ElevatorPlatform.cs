using UnityEngine;

public class Elevator : MonoBehaviour
{
    public enum ActivationMode { AND, OR }

    [Header("Power Sources that activate this elevator")]
    public PowerSource[] requiredSources;
    public ActivationMode activationMode = ActivationMode.AND; // Default is AND

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
        bool powered = AreSourcesPowered();

        // Update color
        if (elevatorRenderer != null)
            elevatorRenderer.material.color = powered ? activeColor : idleColor;

        if (powered)
        {
            // Set initial target if elevator was frozen
            if (targetPosition == transform.position)
            {
                targetPosition = pointA.position;
                movingToB = true;
            }

            MoveElevator();
        }
    }

    void MoveElevator()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

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

    bool AreSourcesPowered()
    {
        if (requiredSources.Length == 0)
            return false;

        switch (activationMode)
        {
            case ActivationMode.AND:
                foreach (PowerSource src in requiredSources)
                {
                    if (!src.IsPowered())
                        return false;
                }
                return true;

            case ActivationMode.OR:
                foreach (PowerSource src in requiredSources)
                {
                    if (src.IsPowered())
                        return true;
                }
                return false;
        }

        return false; // fallback
    }
}
