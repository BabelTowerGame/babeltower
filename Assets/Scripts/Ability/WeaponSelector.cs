using System.Collections;
using UnityEngine;

public class WeaponSelector : MonoBehaviour {

	public WeaponAbility[] weapon;

	public void selectWeapon(int weaponID){
		WeaponMarker weaponMarker = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<WeaponMarker> ();
		abilityCoolDown[] coolDownButtons = GetComponentsInChildren<abilityCoolDown> ();
		WeaponAbility selectWeapon = weapon [weaponID];
		for (int i = 0; i < coolDownButtons.Length; i++) {
			coolDownButtons[i].Initialize(selectWeapon.weaponAbility[i], weaponMarker.gameObject);
		}
	}
}
