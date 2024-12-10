using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutController : MonoBehaviour
{
    public OSC osc;
    public string address_Blackout;
    public CanvasGroup blackoutCanvas; // Assign the Canvas Group component from the Canvas.
    private bool isBlackout = false; // Tracks the current state of the blackout.

    void Start()
    {
        osc.SetAddressHandler(address_Blackout, OnBlackoutReceived);
    }

    public void OnBlackoutReceived(OscMessage message)
    {
        if (isBlackout)
        {
            // If currently blacked out, fade to clear
            StartCoroutine(FadeTo(0f));
        }
        else
        {
            // If currently clear, fade to black
            StartCoroutine(FadeTo(1f));
        }

        // Toggle the blackout state
        isBlackout = !isBlackout;
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float duration = 0.01f; // Adjust for fade speed
        float startAlpha = blackoutCanvas.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            blackoutCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }

        blackoutCanvas.alpha = targetAlpha; // Ensure it reaches the target
    }
}
