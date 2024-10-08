using System.Collections;
using UnityEngine;

public class Hitstop : MonoBehaviour
{
    public float hitStopDuration = 0.1f; // Duration of the hitstop effect

    private bool isHitStopping = false;  // Check if hitstop is active
    private float originalTimeScale;     // Store the original timescale before hitstop

    // Call this method to trigger the hitstop effect
    public void DoHitStop()
    {
        if (!isHitStopping)
        {
            StartCoroutine(HitStopCoroutine());
        }
    }

    private IEnumerator HitStopCoroutine()
    {
        // isHitStopping = true;

        // // Save the original timescale
        // originalTimeScale = Time.timeScale;

        // // Set timescale to zero (completely stopping time)
        // Time.timeScale = 0f;

        // // Wait for the specified duration in real time (ignores timescale)
        // yield return new WaitForSecondsRealtime(hitStopDuration);

        // // Reset timescale to the original value
        // Time.timeScale = originalTimeScale;

        // isHitStopping = false;
        yield return null;
    }
}