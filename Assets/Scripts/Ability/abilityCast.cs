using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tob;
using System;


public class abilityCast : MonoBehaviour {

	public GameObject plasma;
	public GameObject flameShoot;
	public GameObject flame;
	public GameObject explosion;

	// Cast ability on specific position
	public void cast(string abilityName, Vector3 position, Quaternion rotation, float destroyTime){
		GameObject effect = Instantiate (getAbility(abilityName), position, rotation);
		Destroy (effect, destroyTime);

		sendMessage (position, abilityName);

	}

	// Cast ability on monster position
	public void cast(string abilityName, Monster ms, float destroyTime){
		GameObject effect = Instantiate (getAbility(abilityName), ms.transform.position, ms.transform.rotation);

		effect.transform.parent = ms.transform;

		Destroy (effect, destroyTime);

		sendMessage (ms, abilityName);
	}

	GameObject getAbility(string name){
		switch (name) {
		case "plasma":
			return plasma;
		case "flame":
			return flame;
		case "flameShoot":
			return flameShoot;
		case "explosion":
			return explosion;
		}
		return null;
	}

	int getAbilityID(string name){
		switch (name) {
		case "plasma":
			return 4;
		case "flame":
			return 2;
		case "flameShoot":
			return 3;
		case "explosion":
			return 1;
		}
		return -1;
	}

	GameObject getAbilityByID(int ID){
		switch (ID) {
		case 1:
			return plasma;
		case 2:
			return flame;
		case 3:
			return flameShoot;
		case 4:
			return explosion;
		}
		return null;
	}


	// Cast on specific position
	void sendMessage(Vector3 pos, string name){

		NetworkService ns = NetworkService.Instance;

		PlayerEvent playerEvent = new PlayerEvent ();

		PlayerCastEvent pce = new PlayerCastEvent ();

		Tob.Event e = new Tob.Event ();

		pce.Id = getAbilityID (name).ToString();

		Tob.Vector v = new Tob.Vector ();
		v.X = pos.x;
		v.Y = pos.y;
		v.Z = pos.z;
		pce.TargetPosition = v;

		playerEvent.Cast = pce;

		e.Topic = Tob.EventTopic.PlayerEvent;

		e.P = playerEvent;

		ns.SendEvent (e);

		// TODO: Send message to server with ability information
		// send(x,y,z,name, destroyTime);
	}

	// Cast on monster
	void sendMessage(Monster ms, string name){
		float x = ms.transform.position.x;
		float y = ms.transform.position.y;
		float z = ms.transform.position.z;

		NetworkService ns = NetworkService.Instance;

		PlayerEvent playerEvent = new PlayerEvent ();

		PlayerCastEvent pce = new PlayerCastEvent ();

		Tob.Event e = new Tob.Event ();

		pce.Id = getAbilityID (name).ToString();
		pce.TargetId = ms.ID.ToString();

		playerEvent.Cast = pce;

		e.Topic = Tob.EventTopic.PlayerEvent;

		e.P = playerEvent;

		ns.SendEvent (e);
	}

	void onAbilityCast(PlayerEvent e){
		

		AbilityDB db = AbilityDB.Instance;

		int abilityID = int.Parse (e.Cast.Id);

		if (e.Cast.TargetId != null) {
			// Cast on monster position
			GameObject[] mst = GameObject.FindGameObjectsWithTag ("Monster");

			for (int i = 0; i < mst.Length; i++) {
				Monster ms = mst [i].GetComponent<Monster> ();
				if (ms.ID == int.Parse(e.Cast.TargetId)) {
					GameObject effect = Instantiate (getAbilityByID(abilityID), ms.transform.position, ms.transform.rotation);

					effect.transform.parent = ms.transform;

					Destroy (effect, 1.5f);
				}
			}
		} else if (e.Cast.TargetPosition != null) {
			// Cast on mouse position
			Vector3 pos = new Vector3(e.Cast.TargetPosition.X, e.Cast.TargetPosition.Y, e.Cast.TargetPosition.Z);

			GameObject effect = Instantiate (getAbilityByID(abilityID), pos, Quaternion.identity);
			Destroy (effect, 1.5f);
		}
	}

}

