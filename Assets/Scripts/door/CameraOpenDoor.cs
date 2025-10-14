using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraDoorScript
{
	public class CameraOpenDoor : MonoBehaviour
	{
		public float DistanceOpen = 5f;
        public GameObject openText;
        public GameObject closeText;


        void Start()
        {
            openText.SetActive(false);
            closeText.SetActive(false);
        }

        void Update()
        {
             RaycastHit hit;
            bool showOpen = false;
            bool showClose = false;

            if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
            {
                // Check if what we're looking at is a door
                var door = hit.transform.GetComponentInParent<DoorScript.Door>();
                if (door != null)
                {
                    if (!door.open)
                    {
                        // Door is closed → show open prompt
                        showOpen = true;

                        if (Input.GetKeyDown(KeyCode.E))
                            door.OpenDoor();
                    }
                    else
                    {
                        // Door is open → show close prompt
                        showClose = true;

                        if (Input.GetKeyDown(KeyCode.E))
                            door.CloseDoor();
                    }
                }
            }

            // Update UI visibility based on what we're looking at
            openText.SetActive(showOpen);
            closeText.SetActive(showClose);
        }
    }

}
