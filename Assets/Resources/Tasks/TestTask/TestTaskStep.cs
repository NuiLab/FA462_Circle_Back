using UnityEngine;

public class TestTaskStep : TaskStep
{
    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onRadioChecked += radioChecked;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onRadioChecked -= radioChecked;
    }

    private void radioChecked()
    {
        Debug.Log("Quest Step received Radio Checked event. Finishing step.");
        FinishTaskStep();
    }
}
