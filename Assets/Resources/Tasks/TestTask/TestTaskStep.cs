using UnityEngine;

// Assumes 'QuestStep' is the abstract base class for all steps
// NOTE: This file replaces the logic demonstrated in 'CollectCoinsQuestStep'
public class TestTaskStep : TaskStep
{
    // Reference to the specific interactive object we care about
    private Radio targetRadio;

    private void Awake()
    {
        // Find the Radio object in the scene. 
        // NOTE: If you have multiple radios, you would need to use a specific identifier 
        // (e.g., a SerializeField ID) instead of FindFirstObjectByType.
        targetRadio = FindFirstObjectByType<Radio>();
        if (targetRadio == null)
        {
            Debug.LogError("TestTaskStep cannot find a Radio component in the scene! Ensure the Radio script is attached to the object.");
        }
    }

    private void OnEnable()
    {
        // 1. STATE CHECK: Check if the task is already complete upon loading/enabling.
        // This is crucial for persistence across loads or if the player interacts 
        // before the quest step is formally assigned.
        if (targetRadio != null && targetRadio.hasBeenInteractedWith)
        {
            FinishTaskStep();
            return;
        }

        // 2. EVENT SUBSCRIPTION: Only subscribe if the state check failed
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.miscEvents.onRadioChecked += RadioInteracted;
        }
    }

    private void OnDisable()
    {
        // Safely unsubscribe to prevent errors when this object is destroyed
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.miscEvents.onRadioChecked -= RadioInteracted;
        }
    }

    private void RadioInteracted()
    {
        // This method is called instantly when the Radio's interact() fires the event.
        Debug.Log("Quest Step received Radio Checked event. Finishing step.");
        
        // Since this is a single-interaction task, we finish immediately.
        FinishTaskStep();
    }

    // You can remove or modify this if your QuestStep base requires it.
    // private void UpdateState() { } 
}