using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipWeapon : MonoBehaviour {

	public void equip(Weapon wp){
		PlayerMarker pm = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<PlayerMarker>();
		abilityCoolDown[] abilityButtons = GetComponentsInChildren<abilityCoolDown> ();
		Ability itemAbility = wp.Ab;

		for (int i = 0; i < abilityButtons.Length; i++) {
			if (!abilityButtons [i].isInitialize()) {
				abilityButtons [i].Initialize (itemAbility, pm.gameObject);
			}
		}
	}
}