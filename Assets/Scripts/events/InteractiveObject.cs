using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{

    public bool hasBeenInteractedWith = false;
    
    // virtual allows inherited classes make changes to the method
    public virtual void interact()
    {
        Debug.Log($"{gameObject.name} has been interacted with!");
        this.hasBeenInteractedWith = true;
    }
    
}
