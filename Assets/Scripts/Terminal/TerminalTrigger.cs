using UnityEngine;
using UnityEngine.SceneManagement;


public class TerminalTrigger : MonoBehaviour
{
    [SerializeField]
    private bool playerInRange = false;
    public GameObject interactText;
    void Start()
    {
        interactText.SetActive(false);
    }

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Terminal");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
        interactText.SetActive(true);
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
        interactText.SetActive(false);
    }
}
