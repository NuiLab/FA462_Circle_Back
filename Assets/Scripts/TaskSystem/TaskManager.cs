using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class TaskMananger : MonoBehaviour
{
    private Dictionary<string, Task> taskMap;

    private int currentPlatyerLevel;

    private void Awake()
    {
        taskMap = CreateTaskMap();
    }

    private void StartTask(string id)
    {
        Debug.Log("Start Task: " + id);
        Task task = GetTaskById(id);
        task.InstantiateCurrentTaskStep(this.transform);
        ChangeTaskState(task.info.id, TaskState.IN_PROGRESS);
    }

    private void AdvanceTask(string id)
    {
        Debug.Log("Advance Task: " + id);
        Task task = GetTaskById(id);

        task.MoveToNextStep();

        if (task.CurrentStepExists())
        {
            task.InstantiateCurrentTaskStep(this.transform);
        }
        else
        {
            ChangeTaskState(task.info.id, TaskState.CAN_FINISH);
        }
    }

    private void FinishTask(string id)
    {
        Debug.Log("Finish Task: " + id);
        Task task = GetTaskById(id);
        ClaimRewards(task);
        ChangeTaskState(task.info.id, TaskState.FINISHED);
    }

    private void ClaimRewards(Task task)
    {
        GameEventsManager.instance.moneyEvents.MoneyGained(task.info.moneyReward);
        GameEventsManager.instance.playerEvents.SeniorityGained(task.info.seniorityReward);
    }
    


    private void OnEnable()
    {
        GameEventsManager.instance.taskEvents.onStartTask += StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask += AdvanceTask;
        GameEventsManager.instance.taskEvents.onFinishTask += FinishTask;
        // subscribe to player events
        GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onStartTask -= StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask -= AdvanceTask;
        GameEventsManager.instance.taskEvents.onFinishTask -= FinishTask;
        GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
    }


    private void Start()
    {
        // broadcast the initial state of all tasks on startup
        foreach (Task task in taskMap.Values)
        {
            GameEventsManager.instance.taskEvents.TaskStateChange(task);
        }
    }

    private void Update()
    {
        // loop throughb all tasks
        foreach(Task task in taskMap.Values)
        {
            if(task.state == TaskState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(task))
            {
                ChangeTaskState(task.info.id, TaskState.CAN_START);
            }
        }
    }

    private void ChangeTaskState(string id, TaskState state)
    {
        Task task = GetTaskById(id);
        task.state = state;
        GameEventsManager.instance.taskEvents.TaskStateChange(task);
    }

    private void PlayerLevelChange(int level)
    {
        currentPlatyerLevel = level;

    }

    private bool CheckRequirementsMet(Task task)
    {
        bool meetsRequirements = true;

        if(currentPlatyerLevel < task.info.levelRequirement)
        {
            meetsRequirements = false;
        }

        foreach (TaskInfoSO prereqTaskInfo in task.info.taskPreReqs)
        {
            if(GetTaskById(prereqTaskInfo.id).state != TaskState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;


    }


    private Dictionary<string, Task> CreateTaskMap()
    {
        TaskInfoSO[] allTasks = Resources.LoadAll<TaskInfoSO>("Tasks");

        Dictionary<string,Task> idToTaskMap = new Dictionary<string, Task>();

        foreach (TaskInfoSO taskInfo in allTasks)
        {
            if (idToTaskMap.ContainsKey(taskInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating task map: " + taskInfo.id);
            }
            idToTaskMap.Add(taskInfo.id, new Task(taskInfo));
        }

        return idToTaskMap;
    }


    private Task GetTaskById(string id)
    {
        Task task = taskMap[id];

        if(task == null)
        {
            Debug.LogError("ID not found in the task map: " + id);
        }

        return task;
    }



}
