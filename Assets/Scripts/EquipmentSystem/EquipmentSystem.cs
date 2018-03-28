using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EasyEquipmentSystem
{
	public class EquipmentSystem : MonoBehaviour {

		public GameObject[] BodyParts;
		public GameObject[] UsableArmor;

		public Equipment[] equipParts;
		public FullBody[] fullBodies;
		public BodyPart[] bodyParts;

		public bool useBodyParts;
		public bool hideOther;

		public string[] disableModes = new string[] {"Disable", "RendererOff"};
		public int disableMode;

		// Body Choices
		public string[] bodyHeadChoices = new string[] {"None"};
		public string[] bodyChestChoices = new string[] {"None"};
		public string[] bodyArmsChoices = new string[] {"None"};
		public string[] bodyHandsChoices = new string[] {"None"};
		public string[] bodyLegsChoices = new string[] {"None"};
		public string[] bodyFeetChoices = new string[] {"None"};

		// Equipment Choices
		public string[] armorHandRightChoices = new string[] {"None"};
		public string[] armorHandLeftChoices = new string[] {"None"};
		public string[] armorBackWeaponRChoices = new string[] {"None"};
		public string[] armorBackWeaponLChoices = new string[] {"None"};
		public string[] armorBackShieldChoices = new string[] {"None"};
		public string[] armorHeadChoices = new string[] {"None"};
		public string[] armorHairChoices = new string[] {"None"};
		public string[] armorShouldersChoices = new string[] {"None"};
		public string[] armorHandsChoices = new string[] {"None"};
		public string[] armorChestChoices = new string[] {"None"};
		public string[] armorBeltChoices = new string[] {"None"};
		public string[] armorLegsChoices = new string[] {"None"};
		public string[] armorFeetChoices = new string[] {"None"};
		public string[] armorExtra1Choices = new string[] {"None"};
		public string[] armorExtra2Choices = new string[] {"None"};
		public string[] armorExtra3Choices = new string[] {"None"};

		// Picked body part
		public int chosenBodyHeadIndex;
		public int chosenBodyChestIndex;
		public int chosenBodyArmsIndex;
		public int chosenBodyHandsIndex;
		public int chosenBodyLegsIndex;
		public int chosenBodyFeetIndex;

		// Picked equipment
		public int chosenHandRightIndex;
		public int chosenHandLeftIndex;
		public int chosenBackWeaponRIndex;
		public int chosenBackWeaponLIndex;
		public int chosenBackShieldIndex;
		public int chosenHeadIndex;
		public int chosenHairIndex;
		public int chosenShouldersIndex;
		public int chosenHandsIndex;
		public int chosenChestIndex;
		public int chosenBeltIndex;
		public int chosenLegsIndex;
		public int chosenFeetIndex;
		public int chosenExtra1Index;
		public int chosenExtra2Index;
		public int chosenExtra3Index;


		// Update pool for body parts
		public void UpdateChoicesBody()
		{
			bodyHeadChoices = UpdateBodyChoices (BodyPart.BSlot.Head);
			bodyChestChoices = UpdateBodyChoices (BodyPart.BSlot.Chest);
			bodyArmsChoices = UpdateBodyChoices (BodyPart.BSlot.Arms);
			bodyHandsChoices = UpdateBodyChoices (BodyPart.BSlot.Hands);
			bodyLegsChoices = UpdateBodyChoices (BodyPart.BSlot.Legs);
			bodyFeetChoices = UpdateBodyChoices (BodyPart.BSlot.Feet);
		}

		// Update pool for equipment
		public void UpdateChoicesEquipment()
		{
			armorHandRightChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.HandRight);
			armorHandLeftChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.HandLeft);
			armorBackWeaponRChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.BackWeaponR);
			armorBackWeaponLChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.BackWeaponL);
			armorBackShieldChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.BackShield);

			armorHeadChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Head);
			armorHairChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Hair);
			armorShouldersChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Shoulders);
			armorHandsChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Hands);
			armorChestChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Chest);
			armorBeltChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Belt);
			armorLegsChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Legs);
			armorFeetChoices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Feet);

			armorExtra1Choices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Extra1);
			armorExtra2Choices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Extra2);
			armorExtra3Choices = UpdateEquipmentChoices (Equipment.EquipArmorSlot.Extra3);
		}

		// Show only selected equipment
		public void EnableSingleEquipmentPieces()
		{
			EnablePickedEquipment (chosenHandRightIndex, armorHandRightChoices);
			EnablePickedEquipment (chosenHandLeftIndex, armorHandLeftChoices);
			EnablePickedEquipment (chosenBackWeaponRIndex, armorBackWeaponRChoices);
			EnablePickedEquipment (chosenBackWeaponLIndex, armorBackWeaponLChoices);
			EnablePickedEquipment (chosenBackShieldIndex, armorBackShieldChoices);

			EnablePickedEquipment (chosenHeadIndex, armorHeadChoices);
			EnablePickedEquipment (chosenHairIndex, armorHairChoices);
			EnablePickedEquipment (chosenChestIndex, armorChestChoices);
			EnablePickedEquipment (chosenShouldersIndex, armorShouldersChoices);
			EnablePickedEquipment (chosenHandsIndex, armorHandsChoices);
			EnablePickedEquipment (chosenBeltIndex, armorBeltChoices);
			EnablePickedEquipment (chosenLegsIndex, armorLegsChoices);
			EnablePickedEquipment (chosenFeetIndex, armorFeetChoices);

			EnablePickedEquipment (chosenExtra1Index, armorExtra1Choices);
			EnablePickedEquipment (chosenExtra2Index, armorExtra2Choices);
			EnablePickedEquipment (chosenExtra3Index, armorExtra3Choices);

		}

		// Show only selected body parts
		public void EnableBodyParts()
		{
			EnableActiveBodyPart (chosenBodyHeadIndex, bodyHeadChoices);
			EnableActiveBodyPart (chosenBodyChestIndex, bodyChestChoices);
			EnableActiveBodyPart (chosenBodyArmsIndex, bodyArmsChoices);
			EnableActiveBodyPart (chosenBodyHandsIndex, bodyHandsChoices);
			EnableActiveBodyPart (chosenBodyLegsIndex, bodyLegsChoices);
			EnableActiveBodyPart (chosenBodyFeetIndex, bodyFeetChoices);
		}

		// Show/hide body parts
		public void ShowBodyParts( bool state )
		{
			foreach (BodyPart b in bodyParts) 
			{
				SetPartVisibility(b.gameObject, state);
			}
		}

		// Show/hide full body
		public void ShowFullBody( bool state )
		{
			foreach (FullBody b in fullBodies) 
			{
				SetPartVisibility(b.gameObject, state);
			}
		}

		// Show/hide equip
		public void ShowEquip( bool state )
		{
			foreach (Equipment e in equipParts) 
			{
				SetPartVisibility(e.gameObject, state);
			}
		}

		// Updates all objects including body parts and equipment
		public void UpdateParts()
		{
			fullBodies = gameObject.GetComponentsInChildren<FullBody>(true);
			bodyParts = gameObject.GetComponentsInChildren<BodyPart>(true);
			equipParts = gameObject.GetComponentsInChildren<Equipment>(true);

			ShowBodyParts (true);
			ShowFullBody (true);
			ShowEquip(true);
		}

		// Updates all available equipment choices for a particular slot 
		string[] UpdateEquipmentChoices( Equipment.EquipArmorSlot slot )
		{
			// Get all armor equipment for slot and store in list
			int choiceCount = 0;
			List<Equipment> equiList = new List<Equipment> ();

			foreach (Equipment e in equipParts) 
			{
				if ( ( e.EquipmentType == Equipment.EquipType.Armor || e.EquipmentType == Equipment.EquipType.Weapon ) && e.EquipmentSlot == slot) 
				{
					choiceCount += 1;
					equiList.Add(e);
					e.gameObject.SetActive(true);
					e.GetComponent<Renderer>().enabled = true;

					if (hideOther)
					{
						SetPartVisibility(e.gameObject, false);
					}
				}
			}
			
			// Create an array of size of head choices
			string [] choiceArray = new string[choiceCount+1];
			
			// Set index 0 to NONE
			choiceArray [0] = "None";
			
			// Set names for other choices
			int c = 1;
			foreach (Equipment e in equiList) 
			{
				choiceArray [c] = e.gameObject.name;
				c += 1;
			}
			
			return choiceArray;
		}

		// This only displays the equipment object that has been selected
		void EnablePickedEquipment( int pickedIndex, string[] choiceArray  )
		{

			// Find gameobject for selected index (index 0 is excluded!)
			if ( pickedIndex > 0) 
			{
				string searchString;
				
				try
				{
					// Try to get object name for index
					searchString = choiceArray [pickedIndex];
				}
				catch( System.IndexOutOfRangeException ex )
				{
					Debug.LogWarning ("[Equipmentsystem] Could not find deleted object! ("+ ex+")");
					return;
				}

				// Find object in parts array and make it visible
				foreach (Equipment e in equipParts) 
				{
					if (e.gameObject.name == searchString) 
						SetPartVisibility(e.gameObject, true);
				}

			}
		}

		// Updates all available choices for a particular body part slot 
		string[] UpdateBodyChoices( BodyPart.BSlot slot )
		{
			// Get all armor equipment for slot and store in list
			int choiceCount = 0;
			List<BodyPart> bodyList = new List<BodyPart> ();
			
			foreach (BodyPart b in bodyParts) 
			{
				if ( b.Slot == slot) 
				{
					choiceCount += 1;
					bodyList.Add(b);
					b.gameObject.SetActive(true);
					b.GetComponent<Renderer>().enabled = true;

					if (hideOther)
					{
						SetPartVisibility(b.gameObject, false);
					}
				}
			}
			
			// Create an array of size of body part choices
			string [] choiceArray = new string[choiceCount+1];
			
			// Set index 0 to NONE
			choiceArray [0] = "None";
			
			// Set names for other choices
			int c = 1;
			foreach (BodyPart b in bodyList) 
			{
				choiceArray [c] = b.gameObject.name;
				c += 1;
			}
			
			return choiceArray;
		}

		// This only displays the body part object that has been selected
		void EnableActiveBodyPart( int pickedIndex, string[] choiceArray  )
		{
			
			// Find gameobject for selected index (index 0 is excluded!)
			if ( pickedIndex > 0) 
			{
				string searchString;
				
				try
				{
					// Try to get object name for index
					searchString = choiceArray [pickedIndex];
				}
				catch( System.IndexOutOfRangeException ex )
				{
					Debug.LogWarning ("[Equipmentsystem] Could not find deleted object! ("+ ex+")");
					return;
				}

				// Find object in parts array and make it visible
				foreach (BodyPart b in bodyParts) 
				{
					if (b.gameObject.name == searchString) 
						SetPartVisibility(b.gameObject, true);
				}
				
			}
		}

		// Sets visibility of an object, either using set active or disabling renderers
		void SetPartVisibility( GameObject go, bool state)
		{
			if (go != null) 
			{
				if (disableMode == 0)
					go.SetActive (state);
				else
				{
					go.GetComponent<Renderer> ().enabled = state;

					Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
					foreach (Renderer ren in renderers) {
						ren.enabled = state;
					}

				}
			}
		}
		
	}
}