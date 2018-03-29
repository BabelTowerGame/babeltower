using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Hitting monster, cancel collision witht them");
		if (collision.gameObject.tag == "Monster")
		{
			Debug.Log ("Hitting monster, cancel collision witht them");
			Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
		}
	 }
	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;
		if (hit.gameObject.tag == "Monster") {
			Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<Collider>());
		}
	}
}
