using UnityEngine;
using System.Collections; // Needed for IEnumerator

public class TypingAudio : MonoBehaviour
{
    public AudioSource audioSource; // Assign in Inspector
    public float playDuration = 3.0f;

    private Coroutine _audioCoroutine;


        void Start()
    {
        // Ensure AudioSource and AudioClip are assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource or AudioClip not assigned!");
            return;
        }

    }

        void Update()
        {
            if (Input.anyKeyDown)
            {
                // Stop any currently running audio coroutine before starting a new one
                if (_audioCoroutine != null) 
                {
                    StopCoroutine(_audioCoroutine);
                    // Ensure audio stops immediately if the key is pressed again
                    audioSource.Stop(); 
                }
                
                // Start the new coroutine and store its reference
                _audioCoroutine = StartCoroutine(PlayAudioForDuration(playDuration));
            }
        }

    IEnumerator PlayAudioForDuration(float duration)
    {
        // Play the sound
        audioSource.Play();

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Stop the sound
        audioSource.Stop();
        // OR audioSource.Pause(); if you might want to resume later
    }
}
