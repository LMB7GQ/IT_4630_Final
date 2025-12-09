using UnityEngine;

public class Button : MonoBehaviour
{
    [Tooltip("Tag that can activate the button")]
    public string requiredTag = "Lego_Block";

    [HideInInspector]
    public bool isPressed = false;

    private GameObject currentBlock = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag) && currentBlock == null)
        {
            currentBlock = other.gameObject;
            isPressed = true;
        }
    }

    private void Update()
    {
        // Check if the block was destroyed or removed
        if (currentBlock == null)
        {
            isPressed = false;
        }
    }
}
