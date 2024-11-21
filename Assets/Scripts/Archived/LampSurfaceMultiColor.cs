using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSurfaceMultiColor : MonoBehaviour
{
    public Material mat;
    public GameObject blobContainerGroup1;
    public GameObject blobContainerGroup2;
    public float blobIntensityFactor = 0.1f;

    private List<Blob> blobsGroup1 = new List<Blob>();
    private List<Blob> blobsGroup2 = new List<Blob>();

    // Start is called before the first frame update
    void Start()
    {
        // Collect all Blob objects for Group 1
        foreach (Blob b in blobContainerGroup1.GetComponentsInChildren<Blob>())
        {
            blobsGroup1.Add(b);
        }

        // Collect all Blob objects for Group 2
        foreach (Blob b in blobContainerGroup2.GetComponentsInChildren<Blob>())
        {
            blobsGroup2.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set Ball positions for Group 1
        for (int i = 0; i < 10; i++)
        {
            Vector4 ballVec = Vector4.zero;
            if (blobsGroup1.Count > i)
            {
                ballVec.x = blobsGroup1[i].transform.localPosition.x + 0.5f;
                ballVec.y = blobsGroup1[i].transform.localPosition.y + 0.5f;
                ballVec.z = blobsGroup1[i].transform.localScale.x * blobIntensityFactor;
            }

            // Set ball vector for Group 1 in the shader
            mat.SetVector("_Ball" + i + "_Group1", ballVec);
        }

        // Set Ball positions for Group 2
        for (int i = 0; i < 10; i++)
        {
            Vector4 ballVec = Vector4.zero;
            if (blobsGroup2.Count > i)
            {
                ballVec.x = blobsGroup2[i].transform.localPosition.x + 0.5f;
                ballVec.y = blobsGroup2[i].transform.localPosition.y + 0.5f;
                ballVec.z = blobsGroup2[i].transform.localScale.x * blobIntensityFactor;
            }

            // Set ball vector for Group 2 in the shader
            mat.SetVector("_Ball" + i + "_Group2", ballVec);
        }
    }
}
