using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public abstract class WorkPuzzle : ScriptableObject
{
    public string puzzleName;
    public string introText;
    public UnityEvent onPuzzleComplete;

    [NonSerialized]
    public static UnityEvent<int> OnTotalFailedAttemptsIncremented = new UnityEvent<int>();

    public static int failedAttempts = 0;

    private int conditionToLose = 3;
    public abstract void StartPuzzle(CorprateTerminal terminal);
    public abstract bool ProcessInput(string input, CorprateTerminal terminal);
    public abstract string GetHelp();


    // this event can be subscribed to to let other classes know each time the count has been incremented
 

    protected void incrementFailedAttempts()
    {
        failedAttempts++;
        Debug.Log($"Total failed attempts: {failedAttempts}");
        WorkPuzzle.OnTotalFailedAttemptsIncremented.Invoke(failedAttempts);
        checkAttempts();
    }


    protected void checkAttempts()
    {
        
        if (failedAttempts > conditionToLose)
        {
            Debug.Log($"you suck");

        // Set the cursor to be free (CursorLockMode.None)
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("Game-Over", LoadSceneMode.Single); // close all other scenes
        }
        else
        {
            Debug.Log($"still not passed max");
            return;
        }
    }
}
