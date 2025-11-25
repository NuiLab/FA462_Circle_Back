using UnityEngine;

public class Task
{
    // static info
    public TaskInfoSO info;

    // state info
    public TaskState state;
    private int currentTaskStepIndex;

    // every task will begin with the following traits
    public Task(TaskInfoSO taskInfo)
    {
        this.info = taskInfo;
        this.state = TaskState.REQUIREMENTS_NOT_MET;
        this.currentTaskStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentTaskStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentTaskStepIndex < info.taskStepPrefabs.Length);
    }


    public void InstantiateCurrentTaskStep(Transform parentTransform)
    {
        GameObject taskStepPrefab = getCurrentTaskStepPrefab();

         if (taskStepPrefab != null)
        {
            TaskStep taskStep = Object.Instantiate<GameObject>(taskStepPrefab, parentTransform)
                    .GetComponent<TaskStep>();
            taskStep.InitialTaskStep(info.id);
        }
    }

    private GameObject getCurrentTaskStepPrefab()
    {
        GameObject taskStepPrefab = null;
        if (CurrentStepExists())
        {
            taskStepPrefab = info.taskStepPrefabs[currentTaskStepIndex];
        }
        else 
        {
            Debug.LogWarning("Tried to get task step prefab, but stepIndex was out of range indicating that "
                + "there's no current step: TaskId=" + info.id + ", stepIndex=" + currentTaskStepIndex);
        }
        return taskStepPrefab;
    }

}
