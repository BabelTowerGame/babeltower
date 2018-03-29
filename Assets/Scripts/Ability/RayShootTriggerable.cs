using System.Collections;
using UnityEngine;

public class RayShootTriggerable : MonoBehaviour {

	public Transform gun;

	private LineRenderer laserLine;

	public Camera camera;

	[HideInInspector]public float weaponRange = 50f;

	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

	public void Initialize () {
		laserLine = GetComponent<LineRenderer> ();
	}

	public void Fire () {
		
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);

		StartCoroutine (Shot ());

		laserLine.SetPosition (0, gun.position);
		if (Physics.Raycast (ray, out hit)) {
			Transform objectHit = hit.transform;

			if (Vector3.Distance (gun.position, objectHit.position) < weaponRange) {
				transform.LookAt (objectHit);

				laserLine.SetPosition (1, objectHit.position);
			} else {
				laserLine.SetPosition (1, gun.position + (gun.forward * 5));
			}


			
		} else {
			laserLine.SetPosition (1, gun.position + (gun.up * weaponRange));
		}

	}

	private IEnumerator Shot(){
		laserLine.enabled = true;

		yield return shotDuration;

		laserLine.enabled = false;
	}
}
