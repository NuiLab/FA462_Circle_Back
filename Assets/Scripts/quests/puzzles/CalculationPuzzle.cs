using UnityEngine;

[CreateAssetMenu(fileName = "CalculationPuzzle", menuName = "Work Puzzles/CalculationPuzzle")]
public class CalculationPuzzle : WorkPuzzle
{
    public string question = "System load: 47 + 89 - 23 = ?";
    public int answer = 113;
    public override string GetHelp()
    {
        return "\tCOMMAND: calculate <number> -> submit answer";
    }

    public override bool ProcessInput(string input, CorprateTerminal terminal)
    {
        if(input.ToLower().StartsWith("calculate "))
        {
            string answerStr = input.Substring(10).Trim();

            if(int.TryParse(answerStr, out int userAnswer))
            {
                if(userAnswer == answer)
                {
                    terminal.PrintLine($"SUCCESS: Calcualtion verified({answer})\n");
                    onPuzzleComplete?.Invoke();
                    return true;
                }
                else
                {
                    terminal.PrintLine($"FAILED: {userAnswer} is incorrect. Try again.\n");
                    incrementFailedAttempts();
                    return false;
                    
                }
            }
            else
            {
                terminal.PrintLine("ERROR: Invalid number format\n");
                return false;
            }
        }

        terminal.PrintLine("ERROR: Use 'calculate <answer>' to submit\n");
        return false;
    }

    public override void StartPuzzle(CorprateTerminal terminal)
    {
        terminal.PrintLine("TASK: Verify system calculations");
        terminal.PrintLine(question);
        terminal.PrintLine("Use command 'calculate <answer>' to submit\n");
    }
}
