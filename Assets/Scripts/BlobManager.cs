using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobManager : MonoBehaviour
{
    public int randomAdjustmentRange = 5; // The range of random adjustments

    void Update()
    {
        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomizeAllBlobs();
        }
    }

    void RandomizeAllBlobs()
    {
        // Find all Blob instances in the scene
        Blob[] allBlobs = FindObjectsOfType<Blob>();

        // Randomize each Blob's parameters
        foreach (Blob blob in allBlobs)
        {
            RandomizeBlobParameters(blob);
        }
    }

    void RandomizeBlobParameters(Blob blob)
    {
        blob.gravity += Random.Range(-randomAdjustmentRange, randomAdjustmentRange);
        blob.maxYSpeed += Random.Range(-randomAdjustmentRange, randomAdjustmentRange);
        blob.heatSourceIntensity += Random.Range(-randomAdjustmentRange, randomAdjustmentRange);
        blob.passiveCoolingRate += Random.Range(-randomAdjustmentRange * 0.1f, randomAdjustmentRange * 0.1f);
        blob.temperatureForce += Random.Range(-randomAdjustmentRange * 0.01f, randomAdjustmentRange * 0.01f);
        blob.temperatureMinScale += Random.Range(-randomAdjustmentRange * 0.01f, randomAdjustmentRange * 0.01f);
        blob.temperatureMaxScale += Random.Range(-randomAdjustmentRange * 0.01f, randomAdjustmentRange * 0.01f);
    }
}
