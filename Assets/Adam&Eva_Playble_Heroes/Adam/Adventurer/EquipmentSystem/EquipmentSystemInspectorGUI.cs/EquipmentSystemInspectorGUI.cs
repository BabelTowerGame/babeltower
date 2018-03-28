using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace EasyEquipmentSystem
{
	[CustomEditor(typeof(EquipmentSystem))]
	public class EquipmentSystemInspectorGUI : Editor {

		EquipmentSystem equipSystem;

		public override void OnInspectorGUI () 
		{
			// Set< reference to equipment system
			equipSystem = (EquipmentSystem)target;

			// Update total parts count
			equipSystem.UpdateParts();

			// Hiding-mode selector
			equipSystem.disableMode = EditorGUILayout.Popup ("Hiding Mode", equipSystem.disableMode, equipSystem.disableModes);

			// Hiding-mode toggle
			equipSystem.hideOther = EditorGUILayout.Toggle ("Hide non-selected", equipSystem.hideOther);

			// Hiding mode info text
			EditorGUILayout.HelpBox ("'Hide non-selected' automatically hides all objects which are not active in the system, using the above hiding mode.", MessageType.Info);
			EditorGUILayout.Space ();



			// BODY PARTS
			// ======================================================================================

			// Use body parts option
			equipSystem.useBodyParts = EditorGUILayout.Toggle ("Use Body Parts", equipSystem.useBodyParts);

			// If body parts are not used, we assume a full body is available
			if (!equipSystem.useBodyParts) 
			{
				// Hide body parts
				equipSystem.ShowBodyParts(false);

				// No full body found?
				if (equipSystem.fullBodies.Length < 1)
					EditorGUILayout.HelpBox ("No full body found!", MessageType.Warning);

				// Multiple full bodies found?
				if (equipSystem.fullBodies.Length > 1)
					EditorGUILayout.HelpBox ("Multiple full bodies found!", MessageType.Warning);
			} 
			// IF body parts are used
			else 
			{
				// Update pool of choices
				equipSystem.UpdateChoicesBody();

				// Show number of parts found in editor
				EditorGUILayout.LabelField ("    Parts found", equipSystem.bodyParts.Length.ToString ());
				EditorGUILayout.Space ();

				// Are there any body parts?
				if (equipSystem.bodyParts.Length < 1)
					EditorGUILayout.HelpBox ("No body part found!", MessageType.Warning);

				// Display body part categories in inspector
				equipSystem.chosenBodyHeadIndex  = EditorGUILayout.Popup ("    Head",  equipSystem.chosenBodyHeadIndex,  equipSystem.bodyHeadChoices);
				equipSystem.chosenBodyChestIndex = EditorGUILayout.Popup ("    Chest", equipSystem.chosenBodyChestIndex, equipSystem.bodyChestChoices);
				equipSystem.chosenBodyArmsIndex  = EditorGUILayout.Popup ("    Arms",  equipSystem.chosenBodyArmsIndex,  equipSystem.bodyArmsChoices);
				equipSystem.chosenBodyHandsIndex = EditorGUILayout.Popup ("    Hands", equipSystem.chosenBodyHandsIndex, equipSystem.bodyHandsChoices);
				equipSystem.chosenBodyLegsIndex  = EditorGUILayout.Popup ("    Legs",  equipSystem.chosenBodyLegsIndex,  equipSystem.bodyLegsChoices);
				equipSystem.chosenBodyFeetIndex  = EditorGUILayout.Popup ("    Feet",  equipSystem.chosenBodyFeetIndex,  equipSystem.bodyFeetChoices);

				// Hide full body
				equipSystem.ShowFullBody(false);

				// show only
				equipSystem.EnableBodyParts ();
			}
			
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();



			// EQUIPMENT
			// ======================================================================================

			// Display in inspector
			EditorGUILayout.LabelField ("Equip. parts found", equipSystem.equipParts.Length.ToString ());
			EditorGUILayout.Space ();

			// Update pool of choices
			equipSystem.UpdateChoicesEquipment();

			// Update indexes of picked equipment objects
			equipSystem.chosenHandRightIndex = EditorGUILayout.Popup ("Weapon Hand R", equipSystem.chosenHandRightIndex, equipSystem.armorHandRightChoices);
			equipSystem.chosenHandLeftIndex = EditorGUILayout.Popup ("Weapon Hand L", equipSystem.chosenHandLeftIndex, equipSystem.armorHandLeftChoices);
			equipSystem.chosenBackWeaponRIndex = EditorGUILayout.Popup ("Weapon Back R", equipSystem.chosenBackWeaponRIndex, equipSystem.armorBackWeaponRChoices);
			equipSystem.chosenBackWeaponLIndex = EditorGUILayout.Popup ("Weapon Back L", equipSystem.chosenBackWeaponLIndex, equipSystem.armorBackWeaponLChoices);
			equipSystem.chosenBackShieldIndex = EditorGUILayout.Popup ("Shield Back", equipSystem.chosenBackShieldIndex, equipSystem.armorBackShieldChoices);
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			equipSystem.chosenHeadIndex = EditorGUILayout.Popup ("Head", equipSystem.chosenHeadIndex, equipSystem.armorHeadChoices);
			equipSystem.chosenHairIndex = EditorGUILayout.Popup ("Hair", equipSystem.chosenHairIndex, equipSystem.armorHairChoices);
			equipSystem.chosenShouldersIndex = EditorGUILayout.Popup ("Shoulders", equipSystem.chosenShouldersIndex, equipSystem.armorShouldersChoices);
			equipSystem.chosenHandsIndex = EditorGUILayout.Popup ("Hands (Gloves)", equipSystem.chosenHandsIndex, equipSystem.armorHandsChoices);
			equipSystem.chosenChestIndex = EditorGUILayout.Popup ("Chest", equipSystem.chosenChestIndex, equipSystem.armorChestChoices);
			equipSystem.chosenBeltIndex = EditorGUILayout.Popup ("Belt", equipSystem.chosenBeltIndex, equipSystem.armorBeltChoices);
			equipSystem.chosenLegsIndex = EditorGUILayout.Popup ("Legs", equipSystem.chosenLegsIndex, equipSystem.armorLegsChoices);
			equipSystem.chosenFeetIndex = EditorGUILayout.Popup ("Feet", equipSystem.chosenFeetIndex, equipSystem.armorFeetChoices);

			equipSystem.chosenExtra1Index = EditorGUILayout.Popup ("Extra 1", equipSystem.chosenExtra1Index, equipSystem.armorExtra1Choices);
			equipSystem.chosenExtra2Index = EditorGUILayout.Popup ("Extra 2", equipSystem.chosenExtra2Index, equipSystem.armorExtra2Choices);
			equipSystem.chosenExtra3Index = EditorGUILayout.Popup ("Extra 3", equipSystem.chosenExtra3Index, equipSystem.armorExtra3Choices);

			// show only
			equipSystem.EnableSingleEquipmentPieces ();

		}
	}
}