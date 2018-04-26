using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class equipWeapon : MonoBehaviour {

		public void equip(Weapon wp){

			GameObject obj = GameObject.FindGameObjectWithTag ("Player");
			abilityCoolDown[] abilityButtons = GetComponentsInChildren<abilityCoolDown> ();
			
			Ability itemAbility = AbilityDB.Instance.get (wp.Ab_ID);

			if (wp is Shield) {
				Debug.Log("shield");
				abilityButtons [0].Initialize (itemAbility, obj);
			} else if (wp is Weapon) {
				Debug.Log ("weapon");
				abilityButtons [1].Initialize (itemAbility, obj);
			}
		}
	}
}