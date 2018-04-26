using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[CreateAssetMenu (menuName = "Ability/flameAbility")]
	public class flameAbility : Ability {

		public float damage = 2;
		public float range = 10f;

		private flameAbilityTrigger tt;
		public override void Initialize(GameObject obj) {
			tt = obj.GetComponent<flameAbilityTrigger> ();
			tt.damage = damage;
			tt.range = range;
		}

		public override void TriggerAbility(bool buttonPressed) {
			tt.launch (buttonPressed);
		}
	}
}
