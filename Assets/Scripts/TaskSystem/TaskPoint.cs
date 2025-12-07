using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REQUIREMENT CHANGE: Use the base 3D Collider class
[RequireComponent(typeof(Collider))]
public class TaskPoint : MonoBehaviour
{
    [Header("Dialogue (optional)")]
    [SerializeField] private string dialogueKnotName;

    [Header("Task")]
    [SerializeField] private TaskInfoSO TaskInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private bool playerIsNear = false;
    private string TaskId;
    private TaskState currentTaskState;
    
    private void Awake() 
    {
        TaskId = TaskInfoForPoint.id;
        
        // Ensure the required Collider is set to be a trigger
        Collider collider = GetComponent<Collider>();
        if (collider != null && !collider.isTrigger)
        {
            Debug.LogWarning($"Collider on {gameObject.name} (TaskPoint3D) must be set to 'Is Trigger' for this script to work.", this);
            collider.isTrigger = true;
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.taskEvents.onTaskStateChange += TaskStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        // Add safety check for GameEventsManager nulling out during app quit
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.taskEvents.onTaskStateChange -= TaskStateChange;
            GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
        }
    }

    private void SubmitPressed(InputEventContext inputEventContext)
    {
        if (!playerIsNear || !inputEventContext.Equals(InputEventContext.DEFAULT))
        {
            return;
        }

        // if we have a knot name defined, try to start dialogue with it
        if (!dialogueKnotName.Equals("")) 
        {
            GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        }
        // otherwise, start or finish the Task immediately without dialogue
        else 
        {
            // start or finish a Task
            if (currentTaskState.Equals(TaskState.CAN_START) && startPoint)
            {
                GameEventsManager.instance.taskEvents.StartTask(TaskId);
            }
            else if (currentTaskState.Equals(TaskState.CAN_FINISH) && finishPoint)
            {
                GameEventsManager.instance.taskEvents.FinishTask(TaskId);
            }
        }
    }

    private void TaskStateChange(Task Task)
    {
        // only update the Task state if this point has the corresponding Task
        if (Task.info.id.Equals(TaskId))
        {
            currentTaskState = Task.state;
            
        }
    }

    // 3D CHANGE: Changed from OnTriggerEnter2D to OnTriggerEnter
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    // 3D CHANGE: Changed from OnTriggerExit2D to OnTriggerExit
    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}