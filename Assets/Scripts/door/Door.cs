using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


	public class Door : MonoBehaviour
	{
		public bool open;
		public float smooth = 1.0f;
		float DoorOpenAngle = -90.0f;
		float DoorCloseAngle = 0.0f;
		public AudioSource asource;
		public AudioClip openDoor, closeDoor;
		public Quaternion defaultRotation;
		public Quaternion openRotation;
		[SerializeField] string scene;

		// Use this for initialization
		void Start()
		{
			asource = GetComponent<AudioSource>();
			defaultRotation = transform.localRotation;
			openRotation = Quaternion.Euler(0, DoorOpenAngle, 0) * defaultRotation;
		}

		// Update is called once per frame
		void Update()
		{
			Quaternion target = open ? openRotation : defaultRotation;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5f * smooth);
		}

		public void OpenDoor()
		{
			open = !open;
			asource.clip = open ? openDoor : closeDoor;
			asource.Play();
			Debug.Log("Loading " + scene);
			SceneManager.LoadScene(scene);
		}

		public void CloseDoor()
        {
			open = false;
			asource.clip = open ? openDoor : closeDoor;
			asource.Play();
        }
	}
}