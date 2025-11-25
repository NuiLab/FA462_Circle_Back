using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;

    private string taskId;

    public void InitialTaskStep(string taskId)
    {
        this.taskId = taskId;
    }

    protected void FinishTaskStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            // the quest forward now that we've finish this step
            GameEventsManager.instance.taskEvents.AdvanceTask(taskId);
            Debug.Log("Is Finished method reached for this task step, destroying this task's prefab.");
            Destroy(this.gameObject); // cleans itself up from scene
        }
    }


}
