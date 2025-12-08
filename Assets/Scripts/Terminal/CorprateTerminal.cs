using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CorprateTerminal : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text output;
    public TMP_InputField inputField;

    [Header("Work System")]
    public List<WorkPuzzle> dailyPuzzles = new List<WorkPuzzle>();
    public int currentPuzzleIndex = 0;
    
    private bool taskActive = false;
    private WorkPuzzle activePuzzle;

    IEnumerator InitInput()
    {
        yield return null;
        inputField.ActivateInputField();
        inputField.caretBlinkRate = inputField.caretBlinkRate;
    }

    void Start()
    {
        LoadProgress();
        PrintLine("CORP-OS v3.4.9\nUnauthorized access will be logged.");
        
        if (currentPuzzleIndex > 0)
        {
            PrintLine($"SESSION RESTORED: {currentPuzzleIndex}/{dailyPuzzles.Count} tasks completed");
        }
        
        PrintLine("Type 'help' for a list of commands.\n");
        StartCoroutine(InitInput());
    }

    public void OnSubmit()
    {
        string cmd = inputField.text.Trim();
        inputField.text = "";
        
        if (taskActive)
        {
            ProcessPuzzleInput(cmd);
        }
        else
        {
            ProcessCommand(cmd);
        }
        
        inputField.ActivateInputField();
    }

    void ProcessCommand(string command)
    {
        PrintLine("> " + command);

        switch (command.ToLower())
        {
            case "help":
                PrintLine("AVAILABLE COMMANDS:");
                PrintLine("   status   - system status");
                PrintLine("   id       - user authorization");
                PrintLine("   clear    - clears the screen");
                PrintLine("   logout   - disconnect session");
                PrintLine("   work     - complete assigned tasks");
                PrintLine("   about     - mission statement");
                PrintLine("   enlighten - quote of the day");
                if (currentPuzzleIndex > 0)
                    PrintLine("   progress - view work completion");
                break;

            case "status":
                PrintLine("SYSTEM STATUS: STABLE");
                PrintLine("ANOMALY DETECTED: 0x0004 (background process)");
                break;

            case "id":
                PrintLine("USER: undefined");
                PrintLine("ACCESS LEVEL: suspended");
                PrintLine("FLAGGED: yes");
                break;

            case "clear":
                output.text = "";
                break;

            case "logout":
                SaveProgress();
                SceneManager.LoadScene("Office-Level1");
                break;

            case "work":
                StartWork();
                break;

            case "progress":
                PrintLine($"TASKS COMPLETED: {currentPuzzleIndex}/{dailyPuzzles.Count}");
                if (currentPuzzleIndex >= dailyPuzzles.Count)
                {
                    PrintLine("STATUS: All work complete for today");
                }
                break;

            case "reset":
                ResetProgress();
                PrintLine("PROGRESS RESET: All tasks cleared");
                break;

            case "about":
                PrintLine("WELCOME TO YOUR NEW LIFE.");
                PrintLine("You have the privilege of working for Ouro Co.");
                PrintLine("CONGRATULATIONS! You're Being Watched");
                PrintLine("The Ouro Co. mission is to be Earth's most product-centric company, Earth's best employer, and Earth's physically safest place to work.");
                PrintLine("You're going to love it, whether you like it or not!");
                break;

            case "enlighten":
                PrintLine("It is often safer to be in chains than to be free.");
                PrintLine("7 BILLION PEOPLE: INSTANTLY ENLIGHTENED.");
                break;

            default:
                PrintLine("ERR: UNKNOWN COMMAND");
                break;
        }
    }

    void StartWork()
    {
        if (dailyPuzzles.Count == 0)
        {
            PrintLine("ERR: No tasks assigned");
            return;
        }

        if (currentPuzzleIndex >= dailyPuzzles.Count)
        {
            PrintLine("NOTICE: All work tasks completed for today");
            PrintLine("You may now logout.");
             // HERE GO TO WIN SCREEN
           
            return;
        }

        activePuzzle = dailyPuzzles[currentPuzzleIndex];
        taskActive = true;
        
        PrintLine($"\n=== WORK TASK {currentPuzzleIndex + 1}/{dailyPuzzles.Count} ===");
        PrintLine($"ASSIGNMENT: {activePuzzle.puzzleName}\n");
        activePuzzle.StartPuzzle(this);
        PrintLine("Type 'help' for puzzle commands or 'abort' to cancel\n");
    }
    void ProcessPuzzleInput(string input)
    {
        PrintLine("> " + input);

        if (input.ToLower() == "abort")
        {
            PrintLine("TASK ABORTED: Returning to main terminal\n");
            taskActive = false;
            activePuzzle = null;
            return;
        }

        if (input.ToLower() == "help")
        {
            PrintLine("PUZZLE COMMANDS:");
            PrintLine(activePuzzle.GetHelp());
            PrintLine("\tabort - cancel current task\n");
            return;
        }

        bool completed = activePuzzle.ProcessInput(input, this);
        
        if (completed)
        {
            taskActive = false;
            currentPuzzleIndex++;
            SaveProgress(); // Save to static class after completing a task
            
            if (currentPuzzleIndex >= dailyPuzzles.Count)
            {
                PrintLine("=== ALL WORK COMPLETE ===");
                PrintLine("Excellent work. You may now logout.\n");
                 // HERE GO TO WIN SCREEN
                 Debug.Log($"You win!");

                //clear the static variable before loading the next scene (player can only quit or go back to start from this point forward)
                ResetProgress();
                
                Cursor.lockState = CursorLockMode.None;

                SceneManager.LoadScene("Win-Conclusion", LoadSceneMode.Single); // close all other scenes
            }
            else
            {
                PrintLine($"Type 'work' to continue to next task ({currentPuzzleIndex + 1}/{dailyPuzzles.Count})\n");
            }
            
            activePuzzle = null;
        }
    }

    public void PrintLine(string text)
    {
        output.text += text + "\n";
    }

    void SaveProgress()
    {
        SessionData.terminalProgress = currentPuzzleIndex;
        Debug.Log($"Progress saved: {currentPuzzleIndex}/{dailyPuzzles.Count}");
    }

    void LoadProgress()
    {
        currentPuzzleIndex = SessionData.terminalProgress;
        Debug.Log($"Progress loaded: {currentPuzzleIndex}/{dailyPuzzles.Count}");
    }

    public void ResetProgress()
    {
        currentPuzzleIndex = 0;
        SaveProgress();
    }
}

public static class SessionData
{
    public static int terminalProgress = 0;
}