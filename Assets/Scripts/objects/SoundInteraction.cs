using UnityEngine;

public class SoundInteraction : MonoBehaviour
{

  
    private AudioSource audioSource;
  
    private bool playerInRange = false;

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInRange = false;
    }

    void Awake()
    {
        // get the audio component (required to have one attached)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("This GameObject requires a AudioSource component!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Play();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            audioSource.Stop();
        }
    }
}
