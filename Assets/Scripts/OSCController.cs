using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCController : MonoBehaviour
{
    //This script RECEIVES osc values 

    public OSC osc;
    public string address_Threshold;
    public string address_Threshold2;
    public string address_Threshold3;
    public string address_Outline;
    /*public string address_Color1;
    public string address_Color2;
    public string address_Color3;*/

    public Material lavaMaterial; // Assign the shader material in the Unity Inspector


    // Start is called before the first frame update
    void Start()
    {
        osc.SetAddressHandler(address_Threshold, OnThresholdReceived);
        osc.SetAddressHandler(address_Threshold2, OnThreshold2Received);
        osc.SetAddressHandler(address_Threshold3, OnThreshold3Received);
        osc.SetAddressHandler(address_Outline, OnOutlineToleranceReceived);
        /*osc.SetAddressHandler(address_Color1, OnColor1Received);
        osc.SetAddressHandler(address_Color2, OnColor2Received);
        osc.SetAddressHandler(address_Color3, OnColor3Received);*/
    }

    void OnThresholdReceived(OscMessage message)
    {
        //Debug.Log(message.values);
        float threshold = message.GetFloat(0);
        //Debug.Log(threshold);
        lavaMaterial.SetFloat("_Threshold", Mathf.Clamp01(threshold));
    }

    void OnThreshold2Received(OscMessage message)
    {
        float threshold2 = message.GetFloat(0);
        //Debug.Log(threshold2);
        lavaMaterial.SetFloat("_Threshold2", Mathf.Clamp01(threshold2));
    }

    void OnThreshold3Received(OscMessage message)
    {
        float threshold3 = message.GetFloat(0);
        //Debug.Log(threshold3);
        lavaMaterial.SetFloat("_Threshold3", Mathf.Clamp01(threshold3));
    }

    void OnOutlineToleranceReceived(OscMessage message)
    {
        float outlineTolerance = message.GetFloat(0);
        //Debug.Log(outlineTolerance);
        lavaMaterial.SetFloat("_OutlineTolerance", Mathf.Clamp(outlineTolerance, 0f, 0.1f));
    }

    /*void OnColor1Received (OscMessage message)
    {
        Color color = new Color(message.GetFloat(0), message.GetFloat(1), message.GetFloat(2), 1f);
        lavaMaterial.SetColor("_Color", color);
    }

    void OnColor2Received(OscMessage message)
    {
        Color color = new Color(message.GetFloat(0), message.GetFloat(1), message.GetFloat(2), 1f);
        lavaMaterial.SetColor("_Color", color);
    }

    void OnColor3Received(OscMessage message)
    {
        Color color = new Color(message.GetFloat(0), message.GetFloat(1), message.GetFloat(2), 1f);
        lavaMaterial.SetColor("_Color", color);
    }*/
}
