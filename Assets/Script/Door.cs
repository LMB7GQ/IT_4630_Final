using UnityEngine;

public class PoweredDoor : MonoBehaviour
{
    public PowerSource[] requiredSources;
    public GameObject doorVisual; // drag your wall sprite here

    void Update()
    {
        bool allPowered = true;

        foreach (PowerSource src in requiredSources)
        {
            if (!src.IsPowered())
            {
                allPowered = false;
                break;
            }
        }

        doorVisual.SetActive(!allPowered); // powered → hide
    }
}
