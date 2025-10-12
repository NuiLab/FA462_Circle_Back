using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraDoorScript
{
	public class CameraOpenDoor : MonoBehaviour
	{
		public float DistanceOpen = 3f;
		public GameObject text;

		void Start()
        {
			text.SetActive(true);
        }

		void Update()
		{
			RaycastHit hit;
			bool showText = false;

			if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
			{
				var door = hit.transform.GetComponent<DoorScript.Door>();
				if (door != null)
				{
					showText = true;
					if (Input.GetKeyDown(KeyCode.E))
						door.OpenDoor();
				}
			}

			if (text.activeSelf != showText)
				text.SetActive(showText);
		}

	}
}
