﻿using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
        [SerializeField]
        private float maxcameradistance = 20.0f;
        // the height we want the camera to be above the target
        [SerializeField]
		private float height = 5.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

		// Use this for initialization
		void Start() {
            GameObject playerObject = GameObject.FindWithTag("Player");
            this.target = playerObject.transform;
        }

        void Update() {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                if (distance < maxcameradistance) {
                    distance += 0.4f;
                    if (distance > 2.0f) {
                        height += 0.2f;
                    }
                }
            } 
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f){
                
                distance -= 0.4f;
                if (height > 2.0f) {
                    height -= 0.2f;
                }
            }
        }

        // Update is called once per frame
        void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			// Calculate the current rotation angles
			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

			// Always look at the target
			transform.LookAt(target);
		}
	}
}