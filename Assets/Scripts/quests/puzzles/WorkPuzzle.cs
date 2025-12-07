using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WorkPuzzle : ScriptableObject
{
    public string puzzleName;
    public string introText;
    public UnityEvent onPuzzleComplete;

    public abstract void StartPuzzle(CorprateTerminal terminal);
    public abstract bool ProcessInput(string input, CorprateTerminal terminal);
    public abstract string GetHelp();
}
