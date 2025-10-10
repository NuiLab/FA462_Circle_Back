using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] float minViewDistance = 25f; // minimum amount to look around character/limits range
    [SerializeField] Transform playerBody;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;


    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, minViewDistance); // restricts range of rotation

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
