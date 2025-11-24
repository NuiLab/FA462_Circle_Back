using System;

public class MiscEvents
{
    public event Action onRadioChecked;
    public void RadioChecked() 
    {
        if (onRadioChecked != null) 
        {
            onRadioChecked();
        }
    }
}