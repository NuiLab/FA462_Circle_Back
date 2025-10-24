using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    [SerializeField]
    public string npcName;
    private DialogManager dialogManager;
    private bool playerInRange = false;

    public GameObject interactText;
    void Start()
    {
        dialogManager = FindFirstObjectByType<DialogManager>();
        interactText.SetActive(false);
    }

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogManager.ShowLine(npcName, "1", "2");
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
