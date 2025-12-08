using UnityEngine;

[CreateAssetMenu(fileName = "DecryptionPuzzle", menuName = "Work Puzzles/DecryptionPuzzle")]
public class DecryptionPuzzle : WorkPuzzle
{
    public string encryptedCode = "FRQWURO";
    public string decryptedAnswer = "CONTROL";
    public int shift = 3;
    public override string GetHelp()
    {
        return "\tCOMMAND: decrypt <code> -> attempt decryption\n" +
               "\tCOMMAND: hint -> get a hint";
    }
    public override bool ProcessInput(string input, CorprateTerminal terminal)
    {
        if (input.ToLower().StartsWith("decrypt "))
        {
            string attempt = input.Substring(8).Trim().ToUpper();
            if (attempt == decryptedAnswer)
            {
                terminal.PrintLine("SUCCESS: Code decrypted correctly!");
                terminal.PrintLine($"RESULT: {decryptedAnswer}\n");
                onPuzzleComplete?.Invoke();
                return true;
            }
            else
            {
                terminal.PrintLine($"FAILED: '{attempt}' is incorrect. Try again.\n");
                incrementFailedAttempts();
                return false;
            }
        }
        else if (input.ToLower() == "hint")
        {
            terminal.PrintLine($"HINT: Shift each letter back by {shift} position(s).");
            terminal.PrintLine("A B C D E F G H I J K L M N O P Q R S T U V W X Y Z\n");
            return false;
        }
        
        terminal.PrintLine("ERROR: Use 'decrypt <code>' or type 'hint' for help\n");
        return false;
    }

    public override void StartPuzzle(CorprateTerminal terminal)
    {
        terminal.PrintLine($"TASK: Decrypt the following code");
        terminal.PrintLine($"ENCRYPTED: {encryptedCode}");
        terminal.PrintLine($"HINT: Caesar cipher detected. Try 'decrypt <code>'\n");
    }
}
