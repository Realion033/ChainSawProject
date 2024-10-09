using UnityEngine;

public class Levelmusic : MonoBehaviour
{
    public AudioSource audioSource; // Ensure this is assigned in the Inspector
    public MainGameManager mainGameManager; // Ensure this is assigned in the Inspector
    public AudioClip boss; // Assign the boss audio clip in the Inspector

    private bool hasPlayed = false; // Flag to check if the boss music has been played

    private void Update()
    {
        if (mainGameManager.CurrentLevel == 4 && !hasPlayed)
        {
            hasPlayed = true; // Set the flag to prevent retriggering
            audioSource.clip = boss; // Set the audio clip to the boss music
            audioSource.Play(); // Play the boss music
        }
        // Optionally: Handle levels below or above 4 to reset the flag if needed
        else if (mainGameManager.CurrentLevel < 4)
        {
            hasPlayed = false; // Reset the flag when the level is below 4
        }
    }
}
