using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Ability/BulletAbility")]
public class BulletAbility : AbilityCast {

	private BulletShotTriggerable bst;
	public GameObject bulletPrefab;

	public override void Initialize(GameObject obj){
		bst = obj.GetComponent<BulletShotTriggerable> ();
		bst.bulletPrefab = bulletPrefab;
	}

	public override void TriggerAbility(){
		bst.Throw ();
	}
}