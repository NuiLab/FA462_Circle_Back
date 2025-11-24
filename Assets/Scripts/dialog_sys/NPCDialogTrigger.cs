using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    [SerializeField]
    public string npcName;
    private DialogManager dialogManager;
    private bool playerInRange = false;

    // reference to the base InteractiveObject component on this same GameObject
    private InteractiveObject interactiveObject; 



    public GameObject interactText;
    void Start()
    {
        dialogManager = FindFirstObjectByType<DialogManager>();
        interactText.SetActive(false);

        // added by Jocelyn 11/24/25 for the task system (testing)
        // get InteractiveObject component attached to this GameObject
        interactiveObject = GetComponent<InteractiveObject>();

        if (interactiveObject == null)
        {
            Debug.LogError("NPCDialogTrigger requires a component (unique object script) that inherits from InteractiveObject on the same GameObject.", this);
        }
        
    }

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogManager.ShowLine(npcName, "1", "2");
            
            if (interactiveObject != null)
            {
                interactiveObject.interact(); 
            }
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
