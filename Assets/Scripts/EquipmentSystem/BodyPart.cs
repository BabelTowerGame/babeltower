using UnityEngine;
using System.Collections;

namespace EasyEquipmentSystem
{
	public class BodyPart : MonoBehaviour 
	{
		public enum BSlot {Head, Chest, Arms, Hands, Legs, Feet};
		public BSlot Slot;
	}
}