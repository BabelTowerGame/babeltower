using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[CreateAssetMenu (menuName = "Ability/explosionAbility")]
	public class explosionAbility : Ability {

		// Use this for initialization
		public float damage = 1f;
		public float range = 10f;
		public float radius = 3f;

		private explosionAbilityTrigger tt;

		public override void Initialize(GameObject obj) {
			tt = obj.GetComponent<explosionAbilityTrigger> ();
			tt.damage = damage;
			tt.range = range;
			tt.radius = radius;
		}

		public override void TriggerAbility(bool buttonPressed) {
			tt.launch (buttonPressed);
		}
	}
}
