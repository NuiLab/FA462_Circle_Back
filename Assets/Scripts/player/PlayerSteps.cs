using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    public AudioSource footstepsSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
        }
    }
}
