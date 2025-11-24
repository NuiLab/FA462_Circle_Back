using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishTaskStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            // TODO: advance the quest forward now that we've finish this step
            Debug.Log("Is Finished method reached for this task step, destroying this task's prefab.");
            Destroy(this.gameObject); // cleans itself up from scene
        }
    }


}
