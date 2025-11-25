using System.Collections.Generic;
using UnityEngine;

public class TaskMananger : MonoBehaviour
{
    private Dictionary<string, Task> taskMap;

    private void Awake()
    {
        taskMap = CreateTaskMap();
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
}
