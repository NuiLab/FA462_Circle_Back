using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CorprateTerminal : MonoBehaviour
{
    public TMP_Text output;
    public TMP_InputField inputField;

    IEnumerator InitInput()
    {
        yield return null;        
        inputField.ActivateInputField();
        inputField.caretBlinkRate = inputField.caretBlinkRate; // force refresh
    }

    void Start()
    {
        PrintLine("Ouro Co. OS v3.4.9\nUnauthorized access will be logged.");
        PrintLine("Type 'help' for a list of commands.\n");
        StartCoroutine(InitInput());
    }

    public void OnSubmit()
    {
        string cmd = inputField.text.Trim();
        inputField.text = "";
        ProcessCommand(cmd);
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
                PrintLine("   work     - complete given tasks");

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
                SceneManager.LoadScene("Office-Level1");
                break;

            case "work":
                //note for later. make a task structur for each days work
                PrintLine("...");
                break;

            default:
                PrintLine("ERR: UNKNOWN COMMAND");
                break;
        }
    }

    void PrintLine(string text)
    {
        output.text += text + "\n";
    }
}
