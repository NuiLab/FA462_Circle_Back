using UnityEngine;

public class Script : MonoBehaviour
{
    [SerializeField] float fanSpeed = 60;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0,  fanSpeed * Time.deltaTime);
    }
}
