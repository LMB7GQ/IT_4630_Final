using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCheck : MonoBehaviour
{
  


    // Tags for the two objects we are waiting for
    public string objectPlayer_1 = "Player_1";
    public string objectPlayer_2 = "Player_2";
    public int nextLevelBuildIndex; // Set this in the Inspector

    private bool object1InZone = false;
    private bool object2InZone = false;

    private void OnTriggerEnter(Collider other)
    {
        // Use CompareTag for performance and safety
        if (other.CompareTag(objectPlayer_1))
        {
            object1InZone = true;
        }
        if (other.CompareTag(objectPlayer_2))
        {
            object2InZone = true;
        }
        CheckBothObjectsInZone();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(objectPlayer_1))
        {
            object1InZone = false;
        }
        if (other.CompareTag(objectPlayer_2))
        {
            object2InZone = false;
        }
    }

    private void CheckBothObjectsInZone()
    {
        // Load the next level only when both conditions are true
        if (object1InZone && object2InZone)
        {
            Debug.Log("Both objects in zone! Loading next level.");
            // Make sure the next scene index is correct in the Build Settings
            SceneManager.LoadScene(nextLevelBuildIndex);
        }
    }
}
