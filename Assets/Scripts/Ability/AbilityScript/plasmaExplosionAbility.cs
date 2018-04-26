using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[CreateAssetMenu (menuName = "Ability/plasmaExplosionAbility")]
	public class plasmaExplosionAbility : Ability {

		public float damage = 1f;
		public float range = 10;

		private plasmaExplosionAbilityTrigger tt;

		public override void Initialize(GameObject obj) {
			tt = obj.GetComponent<plasmaExplosionAbilityTrigger> ();
			tt.damage = damage;
			tt.range = range;
		}

		public override void TriggerAbility(bool buttonPressed) {
			tt.launch (buttonPressed);
		}
	}
}