using UnityEditor.Rendering.Universal;
using UnityEngine;

public class TriggerVolScript : MonoBehaviour
{
  
    [SerializeField] GameObject assaultManangerObject;
    void Start()
    {

    }

    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Trigger");
        if (other.gameObject.tag != "Player")
        {
            Debug.Log("Not player in OnTriggerEnter");
            return;
        }
        else
        {
            Debug.Log("Player triggered event...");
        }
        
        Vector3 currentPos = other.gameObject.transform.position;

        assaultManangerObject.gameObject.SetActive(true);
    }
}
