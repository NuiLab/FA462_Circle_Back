using UnityEngine;

public class Radio : InteractiveObject
{
    
    public override void interact()
    {
        // call task/event unqiue to the radio
        GameEventsManager.instance.miscEvents.RadioChecked(); 

        Debug.Log("RadioChecked Event Triggered");
        
        // call base class method to update the shared state
        base.interact();
    }

    
}
