using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class equipWeapon : MonoBehaviour {

		public void equip(Weapon wp){
			PlayerMarker pm = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<PlayerMarker>();
			abilityCoolDown[] abilityButtons = GetComponentsInChildren<abilityCoolDown> ();
			Ability itemAbility = wp.Ab;
			if (wp is Weapon) {
				abilityButtons [0].Initialize (itemAbility, pm.gameObject);
			} else if (wp is Shield) {
				abilityButtons [1].Initialize (itemAbility, pm.gameObject);
			}
		}
	}
}