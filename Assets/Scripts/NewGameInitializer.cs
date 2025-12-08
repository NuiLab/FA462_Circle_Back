using UnityEngine;

public class NewGameInitializer : MonoBehaviour
{
    // also referenced in DialogManager.cs
    private const string InstructionsShownKey = "InstructionsScene";
    void Awake()
    {

        if (PlayerPrefs.HasKey(InstructionsShownKey))
        {
            PlayerPrefs.DeleteKey(InstructionsShownKey);
            PlayerPrefs.Save(); 
            
            Debug.Log("Reset: Scene entry instructions flag reset.");
        }
    }
}