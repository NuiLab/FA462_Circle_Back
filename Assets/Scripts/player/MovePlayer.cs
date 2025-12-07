using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
   [SerializeField] float speed = 3.0f;
   [SerializeField] float fixedYPosition = 1.51f;
   public bool canMove = true;
   Rigidbody rb;
   Vector3 inputMovement;
   void Start()
   {
       rb = GetComponent<Rigidbody>();
       // We want physics but not tipping over
       rb.freezeRotation = true;
   }
   void Update()
   {
       if (!canMove)
       {
           inputMovement = Vector3.zero;
           return;
       }
       // --- Match your original input logic ---
       float x = Input.GetAxis("Horizontal") * speed;
       float z = Input.GetAxis("Vertical") * speed;
       float y = 0f;
       if (Input.GetKey(KeyCode.Q))
           y = speed;
       else if (Input.GetKey(KeyCode.E))
           y = -speed;
       inputMovement = new Vector3(x, y, z);
   }
   void FixedUpdate()
   {
       if (!canMove)
           return;
       // Apply deltaTime here since we're moving in FixedUpdate
       Vector3 move = inputMovement * Time.fixedDeltaTime;
       // Compute the next position
       Vector3 targetPos = rb.position + move;
       // Lock Y position *exactly like your script*
       targetPos.y = fixedYPosition;
       // Move with physics
       rb.MovePosition(targetPos);
   }
}