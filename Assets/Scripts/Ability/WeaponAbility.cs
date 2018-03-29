using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "WeaponAbility")]
public class WeaponAbility : ScriptableObject {

	public string weaponName = "example";

	public AbilityCast[] weaponAbility;
}
