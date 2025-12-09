using UnityEngine;

public class Character1Battery : MonoBehaviour
{
    public bool hasBattery = true;               // Player starts with a battery
    private BatterySocket currentSocket = null;  // Socket player is in trigger with

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && currentSocket != null)
        {
            if (hasBattery && !currentSocket.hasBattery)
            {
                // Place battery
                currentSocket.InsertBattery();
                hasBattery = false;
            }
            else if (!hasBattery && currentSocket.hasBattery)
            {
                // Pick battery back up
                currentSocket.RemoveBattery();
                hasBattery = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            BatterySocket socket = other.GetComponentInParent<BatterySocket>();
            if (socket != null)
                currentSocket = socket;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            BatterySocket socket = other.GetComponentInParent<BatterySocket>();
            if (socket != null && currentSocket == socket)
                currentSocket = null;
        }
    }
}
