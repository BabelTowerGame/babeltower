using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Ability/exampleAbility")]
public class exampleAbility : AbilityCast {

	public override void Initialize(GameObject obj){
		Debug.Log ("Initialize");
	}

	public override void TriggerAbility(){
		Debug.Log ("Triggered!");
	}
}
