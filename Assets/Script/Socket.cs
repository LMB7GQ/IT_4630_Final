using UnityEngine;

public class BatterySocket : PowerSource
{
    [Header("Visuals")]
    public Renderer cubeRenderer;           // The visual cube showing battery presence
    public Color inactiveColor = Color.gray;
    public Color activeColor = Color.yellow;

    [Header("Battery Logic")]
    [HideInInspector]
    public bool hasBattery = false;         // True if battery is placed

    void Start()
    {
        UpdateVisual();
    }

    // Called by player when inserting the battery
    public void InsertBattery()
    {
        if (hasBattery) return;

        hasBattery = true;
        UpdateVisual();
    }

    // Called by player when removing the battery
    public void RemoveBattery()
    {
        if (!hasBattery) return;

        hasBattery = false;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (cubeRenderer != null)
        {
            cubeRenderer.material.color = hasBattery ? activeColor : inactiveColor;
        }
    }

    // ✅ This is what Elevator uses to check if it's powered
    public override bool IsPowered()
    {
        return hasBattery;
    }
}
