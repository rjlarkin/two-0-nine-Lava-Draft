using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSurface : MonoBehaviour
{
    //Two Materials
    public Material mat;
            //public Material mat1;

    //Two Sets of Blobs
    public GameObject blobContainer;
            //public GameObject blobContainer1;

    //Two Intensity Factors
    public float blobIntensityFactor = 0.1f;
            // public float blobIntensityFactor1 = 0.1f;

    //Separate Lists for Each Type of Blob
    private List<Blob> blobs = new List<Blob>();
            //private List<Blob1> blobs1 = new List<Blob1>();

    // Start is called before the first frame update
    void Start()
    {
        //First type of blob (Blob)
        foreach (Blob b in blobContainer.GetComponentsInChildren<Blob>())
        {
            blobs.Add(b);
        }

        //Second type of blob (Blob1)
        /*foreach (Blob1 b in blobContainer1.GetComponentsInChildren<Blob1>())
        {
            blobs1.Add(b);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector4 ballVec = Vector4.zero;
            if (blobs.Count > i)
            {
                ballVec.x = blobs[i].transform.localPosition.x + 0.5f;
                ballVec.y = blobs[i].transform.localPosition.y + 0.5f;
                ballVec.z = blobs[i].transform.localScale.x * blobIntensityFactor;
            }

            mat.SetVector("_Ball" + i, ballVec);
        }

        /*for (int i = 0; i < 10; i++)
        {
            Vector4 ballVec = Vector4.zero;
            if (blobs1.Count > i)
            {
                ballVec.x = blobs[i].transform.localPosition.x + 0.5f;
                ballVec.y = blobs[i].transform.localPosition.y + 0.5f;
                ballVec.z = blobs[i].transform.localScale.x * blobIntensityFactor;
            }

            mat1.SetVector("_Ball" + i, ballVec);
        }*/
    }
}
