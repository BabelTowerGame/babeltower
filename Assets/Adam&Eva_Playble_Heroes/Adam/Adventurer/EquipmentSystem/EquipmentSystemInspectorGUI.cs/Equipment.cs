using UnityEngine;
using System.Collections;

namespace EasyEquipmentSystem
{
	public class Equipment : MonoBehaviour 
	{	
		public enum EquipType { Armor, Weapon };
		public enum EquipArmorSlot {HandRight, HandLeft, BackWeaponR, BackWeaponL, BackShield, Head, Hair, Chest, Shoulders, Arms, Hands, Belt, Legs, Feet, Extra1, Extra2, Extra3};
		public EquipType EquipmentType;
		public EquipArmorSlot EquipmentSlot;
	}
}