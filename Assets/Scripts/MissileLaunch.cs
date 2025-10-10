using UnityEngine;

public class FlyBallToPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float speed = 12.0f;
    [SerializeField] GameObject player;
    Vector3 playerPosition;
    void Start()
    {
        playerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        WhenReachedTarget(); 
    }
    void WhenReachedTarget()
    {
        
        if (transform.position == playerPosition)
        {
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
        }
    }
}
