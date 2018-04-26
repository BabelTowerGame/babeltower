using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
	[CreateAssetMenu (menuName = "Ability/flameShootAbility")]
	public class flameShootAbility : Ability {

		public float damage;


		private flameShootAbilityTrigger tt;

		public override void Initialize(GameObject obj) {
			tt = obj.GetComponent<flameShootAbilityTrigger> ();
			tt.damage = damage;
		}

		public override void TriggerAbility(bool buttonPressed) {
			tt.launch (buttonPressed);
		}
	}
}
