using UnityEngine;

[CreateAssetMenu(fileName = "SequencePuzzle", menuName = "Work Puzzles/SequencePuzzle")]
public class SequencePuzzle : WorkPuzzle
{
    public string[] correctSequence = {"alpha", "beta", "gamma"};
    private int currentStep = 0;
    public override string GetHelp()
    {
        return "\tCOMMAND: authorize <name> -> authorize protocol\n" +
               "\tCOMMAND: hint -> get next protocol";
    }

    public override bool ProcessInput(string input, CorprateTerminal terminal)
    {
        if(input.ToLower().StartsWith("authorize "))
        {
            string protocol = input.Substring(10).Trim().ToLower();

            if(protocol == correctSequence[currentStep])
            {
                currentStep++;
                terminal.PrintLine($"AUTHORIZED: {protocol.ToUpper()} [{currentStep}/{correctSequence.Length}]");

                if(currentStep >= correctSequence.Length)
                {
                    terminal.PrintLine("SUCCESS: All protocols authorized.\n");
                    onPuzzleComplete?.Invoke();
                    return true;
                }
                terminal.PrintLine("");
                return false;
            }
            else
            {
                terminal.PrintLine($"FAILED: Incorrect protocol. Resetting sequence...\n");
                currentStep = 0;
                incrementFailedAttempts();
                return false;
            }
        }
        else if (input.ToLower() == "hint")
        {
            terminal.PrintLine($"HINT: Try protocol '{correctSequence[currentStep]}'\n");
            return false;
        }
        
        terminal.PrintLine("ERROR: Use 'authorize <protocol>' or type 'hint'\n");
        return false;
    }

    public override void StartPuzzle(CorprateTerminal terminal)
    {
        currentStep = 0;
        terminal.PrintLine("TASK: Authorize security protocols in correct order");
        terminal.PrintLine($"PROTOCOLS NEEDED: {correctSequence.Length}");
        terminal.PrintLine($"PROTOCOLS AVAILABLE: {string.Join(",", correctSequence)}");
        terminal.PrintLine("Use 'authorize <protocol>' to proceed\n");
    }
}
