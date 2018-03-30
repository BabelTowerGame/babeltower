using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {
	public List<GameObject> pickupList;
	public GameObject window;
	//testing
	int flag = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pickup") && window.activeSelf) {
			Debug.Log ("close the gui!!!");
			closeGui ();
		} else if (Input.GetButtonDown ("Pickup") && !window.activeSelf) {
			Debug.Log ("open the gui!!!");
			//update then show the window
			//updateGui();
			openGui ();
		}
		if (window.activeSelf) {
			//update too fast
			//flag = 1;
			if (!IsInvoking ()) {
				InvokeRepeating ("updateGui", 1, 1); 
			}
			//updateGui ();
		} else if (!window.activeSelf) {
			CancelInvoke();
		}
	}

	void openGui(){
		window.SetActive (true);
		updateGui ();
	}

	void closeGui(){
		window.SetActive (false);
	}

	void updateGui(){
		pickupList.Clear ();
		foreach (Collider item in Physics.OverlapSphere (transform.position, 10.0f)) {
			if (item.tag == "Items") {
				pickupList.Add (item.gameObject);
			}
		}
		if (pickupList.Count > 0) {
			Debug.Log ("Found: " + pickupList.Count);
		} else {
			Debug.Log ("Nothing found.");
		}
	}
}
