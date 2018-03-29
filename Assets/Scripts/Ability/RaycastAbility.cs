using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Ability/RaycastAbility")]
public class RaycastAbility : AbilityCast {

	private RayShootTriggerable rst;

	public float weaponRange = 50f;

	public override void Initialize(GameObject obj){
		rst = obj.GetComponent<RayShootTriggerable> ();
		rst.Initialize ();

		rst.weaponRange = this.weaponRange;
	}

	public override void TriggerAbility(){
		rst.Fire ();
	}
}
