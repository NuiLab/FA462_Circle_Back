using System.Collections.Generic;
using UnityEngine;

public class TaskMananger : MonoBehaviour
{
    private Dictionary<string, Task> taskMap;

    private void Awake()
    {
        taskMap = CreateTaskMap();
    }

    private void StartTask(string id)
    {
        Debug.Log("Start Task: " + id);
    }

    private void AdvanceTask(string id)
    {
        Debug.Log("Advance Task: " + id);
    }

    private void FinishTask(string id)
    {
        Debug.Log("Finish Task: " + id);
    }


    private void OnEnable()
    {
        GameEventsManager.instance.taskEvents.onStartTask += StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask += AdvanceTask;
        GameEventsManager.instance.taskEvents.onFinishTask += FinishTask;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onStartTask -= StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask -= AdvanceTask;
        GameEventsManager.instance.taskEvents.onFinishTask -= FinishTask;
    }


    private void Start()
    {
        // broadcast the initial state of all tasks on startup
        foreach (Task task in taskMap.Values)
        {
            GameEventsManager.instance.taskEvents.TaskStateChange(task);
        }
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
