using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : MonoBehaviour {

	void Awake(){
		Character c = CharacterManager.character;

		Debug.Log (transform.position);

		Vector3 point = new Vector3 (Random.Range (1000, 1300), 1000, Random.Range (1000, 1300));

		RaycastHit hit;
		// note that the ray starts at 100 units

		if (Physics.Raycast(point, -Vector3.up, out hit)) { 
			Debug.Log ("hit");
			Debug.Log (hit.point);
		}

		Vector3 spawn = hit.point;

		spawn.y += 10;

		if (c.appearance.gender == Appearance.Gender.Male) {
			GameObject go = GameObject.Instantiate (Resources.Load ("Prefabs/PlayerModels/MaleCharacterController"), spawn, Quaternion.identity) as GameObject;
			go.GetComponent<EasyEquipmentSystem.EquipmentSystem> ().onHairColorChanged (c.appearance.hairColor);
		}

	}
}
