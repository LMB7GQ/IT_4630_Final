using UnityEngine;

public class Door : MonoBehaviour
{
    public enum ActivationMode { AND, OR }

    [Header("Power Sources that control this door")]
    public PowerSource[] requiredSources;
    public ActivationMode activationMode = ActivationMode.AND;

    [Header("Door visual")]
    public Renderer doorRenderer;
    public Color closedColor = Color.red;
    public Color openColor = Color.green;

    private bool isOpen = false;
    private Collider doorCollider;

    void Start()
    {
        doorCollider = GetComponent<Collider>();
        if (doorCollider == null)
        {
            Debug.LogError("Door requires a Collider!");
        }
        UpdateDoorState();
    }

    void Update()
    {
        UpdateDoorState();
    }

    void UpdateDoorState()
    {
        bool powered = AreSourcesPowered();

        if (powered && !isOpen)
            OpenDoor();
        else if (!powered && isOpen)
            CloseDoor();
    }

    void OpenDoor()
    {
        isOpen = true;

        if (doorCollider != null)
            doorCollider.isTrigger = true; // allow players to pass through

        UpdateVisual();
    }

    void CloseDoor()
    {
        isOpen = false;

        if (doorCollider != null)
            doorCollider.isTrigger = false; // block players

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (doorRenderer != null)
            doorRenderer.material.color = isOpen ? openColor : closedColor;
    }

    bool AreSourcesPowered()
    {
        if (requiredSources.Length == 0)
            return false;

        switch (activationMode)
        {
            case ActivationMode.AND:
                foreach (PowerSource src in requiredSources)
                    if (!src.IsPowered()) return false;
                return true;

            case ActivationMode.OR:
                foreach (PowerSource src in requiredSources)
                    if (src.IsPowered()) return true;
                return false;
        }

        return false;
    }
}
