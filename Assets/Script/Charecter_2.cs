using UnityEngine;

public class Character_2_Special : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform spawnPoint;

    private GameObject placedBlock;
    private bool blockPlaced = false;

    // Reference to the Cube child to scale it
    public Transform cubeVisual;

    // Scales
    private Vector3 fullScale = new Vector3(0.3f, 0.6f, 0.3f);
    private Vector3 shrunkScale = new Vector3(0.3f, 0.4f, 0.3f);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!blockPlaced)
            {
                TryPlaceBlock();
            }
            else
            {
                TryPickUpBlock();
            }
        }
    }

    void TryPlaceBlock()
    {
        placedBlock = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);
        blockPlaced = true;

        // SHRINK the player cube
        cubeVisual.localScale = shrunkScale;
    }

    void TryPickUpBlock()
    {
        if (placedBlock == null) return;

        float distance = Vector3.Distance(transform.position, placedBlock.transform.position);

        // Must be near the block to pick it up
        if (distance <= 1.2f)
        {
            Destroy(placedBlock);
            blockPlaced = false;

            // Restore full size
            cubeVisual.localScale = fullScale;
        }
    }
}
