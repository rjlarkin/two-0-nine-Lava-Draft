using UnityEngine;

public class ColorControl : MonoBehaviour
{
    public OSC osc;
    public string address_Gate;
    public string address_Threshold;
    public string address_Threshold2;
    public string address_Threshold3;
    public string address_Outline;

    public Material lavaMaterial; // Assign the shader material in the Unity Editor
    public float transitionDuration = 5f; // Duration for transitions

    private Color currentColor1, currentColor2, currentColor3;
    private Color targetColor1, targetColor2, targetColor3;
    private float currentThreshold, currentThreshold2, currentThreshold3, currentOutlineTolerance;
    private float targetThreshold, targetThreshold2, targetThreshold3, targetOutlineTolerance;
    private float transitionProgress = 0f;
    private bool isTransitioning = false;

    void Start()
    {
        // Set random initial colors and properties for the start of the game
        SetInitialProperties();
        //osc.SetAddressHandler(address_Outline, OnOutlineToleranceSent);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press space to start a new transition
        {
            SetNewTargetProperties();
            isTransitioning = true;
            transitionProgress = 0f;

            OscMessage message = new OscMessage();
            message.address = address_Gate;
            message.values.Add(0);
            osc.Send(message);
        }

        if (isTransitioning)
        {
            transitionProgress += Time.deltaTime / transitionDuration;
            if (transitionProgress >= 1f)
            {
                transitionProgress = 1f;
                isTransitioning = false;
            }

            // Interpolate between current and target properties

            //lavaMaterial.SetColor("_Color", Color.Lerp(currentColor1, targetColor1, transitionProgress));
            //lavaMaterial.SetColor("_Color2", Color.Lerp(currentColor2, targetColor2, transitionProgress));
            //lavaMaterial.SetColor("_Color3", Color.Lerp(currentColor3, targetColor3, transitionProgress));
            //lavaMaterial.SetFloat("_Threshold", Mathf.Lerp(currentThreshold, targetThreshold, transitionProgress));
            //lavaMaterial.SetFloat("_Threshold2", Mathf.Lerp(currentThreshold2, targetThreshold2, transitionProgress));
            //lavaMaterial.SetFloat("_Threshold3", Mathf.Lerp(currentThreshold3, targetThreshold3, transitionProgress));
            //lavaMaterial.SetFloat("_OutlineTolerance", Mathf.Lerp(currentOutlineTolerance, targetOutlineTolerance, transitionProgress));

            lavaMaterial.SetColor("_Color", Color.Lerp(currentColor1, targetColor1, transitionProgress));
            lavaMaterial.SetColor("_Color2", Color.Lerp(currentColor2, targetColor2, transitionProgress));
            lavaMaterial.SetColor("_Color3", Color.Lerp(currentColor3, targetColor3, transitionProgress));


            float thresholdValue = Mathf.Lerp(currentThreshold, targetThreshold, transitionProgress);
            lavaMaterial.SetFloat("_Threshold", thresholdValue);

            float threshold2Value = Mathf.Lerp(currentThreshold2, targetThreshold2, transitionProgress);
            lavaMaterial.SetFloat("_Threshold2", threshold2Value);

            float threshold3Value = Mathf.Lerp(currentThreshold3, targetThreshold3, transitionProgress);
            lavaMaterial.SetFloat("_Threshold3", threshold3Value);

            float outlineToleranceValue = Mathf.Lerp(currentThreshold3, targetThreshold3, transitionProgress);
            lavaMaterial.SetFloat("_OutlineTolerance", outlineToleranceValue);

            OscMessage message = new OscMessage();
            message.address = address_Gate;
            message.values.Add(1);
            osc.Send(message);

            OscMessage message1 = new OscMessage();
            message1.address = address_Threshold;
            message1.values.Add(thresholdValue);
            osc.Send(message1);

            OscMessage message2 = new OscMessage();
            message2.address = address_Threshold2;
            message2.values.Add(threshold2Value);
            osc.Send(message2);

            OscMessage message3 = new OscMessage();
            message3.address = address_Threshold3;
            message3.values.Add(threshold3Value);
            osc.Send(message3);

            OscMessage message4 = new OscMessage();
            message4.address = address_Outline;
            message4.values.Add(outlineToleranceValue);
            osc.Send(message4);


            // Update current properties when the transition is done
            if (!isTransitioning)
            {
                currentColor1 = targetColor1;
                currentColor2 = targetColor2;
                currentColor3 = targetColor3;
                currentThreshold = targetThreshold;
                currentThreshold2 = targetThreshold2;
                currentThreshold3 = targetThreshold3;
                currentOutlineTolerance = targetOutlineTolerance;
            }
        }
    }

    void SetInitialProperties()
    {
        // Generate initial random colors and properties
        currentColor1 = new Color(Random.value, Random.value, Random.value, 1.0f);
        currentColor2 = new Color(Random.value, Random.value, Random.value, 1.0f);
        currentColor3 = new Color(Random.value, Random.value, Random.value, 1.0f);

        currentThreshold = Random.Range(0f, 1f);
        currentThreshold2 = Random.Range(0f, 1f);
        currentThreshold3 = Random.Range(0f, 1f);
        currentOutlineTolerance = Random.Range(0f, 0.1f);

        // Ensure thresholds are in order
        if (currentThreshold > currentThreshold2)
        {
            (currentThreshold, currentThreshold2) = (currentThreshold2, currentThreshold);
        }
        if (currentThreshold2 > currentThreshold3)
        {
            (currentThreshold2, currentThreshold3) = (currentThreshold3, currentThreshold2);
        }
        if (currentThreshold > currentThreshold2)
        {
            (currentThreshold, currentThreshold2) = (currentThreshold2, currentThreshold);
        }

        // Set the initial properties on the material
        lavaMaterial.SetColor("_Color", currentColor1);
        lavaMaterial.SetColor("_Color2", currentColor2);
        lavaMaterial.SetColor("_Color3", currentColor3);
        lavaMaterial.SetFloat("_Threshold", currentThreshold);
        lavaMaterial.SetFloat("_Threshold2", currentThreshold2);
        lavaMaterial.SetFloat("_Threshold3", currentThreshold3);
        lavaMaterial.SetFloat("_OutlineTolerance", currentOutlineTolerance);
    }

    void SetNewTargetProperties()
    {
        // Generate new random target colors and properties for transitions
        targetColor1 = new Color(Random.value, Random.value, Random.value, 1.0f);
        targetColor2 = new Color(Random.value, Random.value, Random.value, 1.0f);
        targetColor3 = new Color(Random.value, Random.value, Random.value, 1.0f);

        targetThreshold = Random.Range(0f, 1f);
        targetThreshold2 = Random.Range(0f, 1f);
        targetThreshold3 = Random.Range(0f, 1f);
        targetOutlineTolerance = Random.Range(0f, 0.1f);

        // Ensure thresholds are in order
        if (targetThreshold > targetThreshold2)
        {
            (targetThreshold, targetThreshold2) = (targetThreshold2, targetThreshold);
        }
        if (targetThreshold2 > targetThreshold3)
        {
            (targetThreshold2, targetThreshold3) = (targetThreshold3, targetThreshold2);
        }
        if (targetThreshold > targetThreshold2)
        {
            (targetThreshold, targetThreshold2) = (targetThreshold2, targetThreshold);
        }
    }
}
