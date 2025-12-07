using UnityEngine;

[CreateAssetMenu(fileName = "FetchPuzzle", menuName = "Work Puzzles/FetchPuzzle")]
public class FetchPuzzle : WorkPuzzle
{
    public string target;
    public string answer;
    public string helpText;
    public override string GetHelp()
    {
        return "\tHINT: " + helpText;
    }

    public override bool ProcessInput(string input, CorprateTerminal terminal)
    {
        if(input.ToLower().StartsWith("submit "))
        {
            string submition = input.Substring(7).Trim().ToLower();

            if(submition == answer)
            {
                terminal.PrintLine($"SUCCESS: code verified({submition})\n");
                onPuzzleComplete?.Invoke();
                return true;
            }
            else
            {
                terminal.PrintLine($"FAILED: {submition} is incorrect. Try again.\n");
                return false;
            }

        }

        terminal.PrintLine("ERROR: Use 'submit <code>' to proceed\n");
        return false;
    }

    public override void StartPuzzle(CorprateTerminal terminal)
    {
        terminal.PrintLine($"TASK: Retrive information from {target}");
        terminal.PrintLine("Use 'submit <code>' to proceed");
    }
}
