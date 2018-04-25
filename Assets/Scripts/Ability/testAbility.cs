using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[CreateAssetMenu (menuName = "Ability/testAbility")]
	public class testAbility : Ability {

		public float damage = 2;
		public float range = 10f;

		private testAbilityTrigger tt;
		public override void Initialize(GameObject obj) {
			tt = obj.GetComponent<testAbilityTrigger> ();
			tt.damage = damage;
			tt.range = range;
		}

		public override void TriggerAbility(bool buttonPressed) {
			tt.launch (buttonPressed);
		}
	}
}
