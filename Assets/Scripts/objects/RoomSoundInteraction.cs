using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomSoundInteraction : MonoBehaviour, IPointerClickHandler
{

  
    private AudioSource audioSource;
  
    private bool inRange = false;

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
        {
            // get the audio component (required to have one attached)
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("This GameObject requires a AudioSource component!");
            }
        }


    public void OnPointerClick(PointerEventData eventData)
    {
        
        audioSource.Play();
       
    }




}
