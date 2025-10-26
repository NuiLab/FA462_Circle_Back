using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float speed = 3.0f;
    [SerializeField] float fixedYPosition = 1.51f;
    public bool canMove = true;
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        
        Vector3 currentPosition = transform.position;
        currentPosition.y = fixedYPosition;
        transform.position = currentPosition;

        float speedFactor = speed * Time.deltaTime;
        float x = speedFactor * Input.GetAxis("Horizontal");
        float z = speedFactor * Input.GetAxis("Vertical");
        float y = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            y = speedFactor;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            y = speedFactor * -1;
        }

        transform.Translate(x, y, z);
        
    }
}
