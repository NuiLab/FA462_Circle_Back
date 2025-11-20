using UnityEngine;

public class TerminalSingleton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private static TerminalSingleton instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);   // prevent duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
