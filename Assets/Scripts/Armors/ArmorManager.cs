using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;
using UnityEngine.UI;


namespace EasyEquipmentSystem{

	public class ArmorManager : MonoBehaviour {

			
		public BagManager bagmanager;
		public Character player;
		public GameObject armorObject;
		public UIEquipSlot[] armorslots = new UIEquipSlot[4];
		[SerializeField] private GameObject ArmorContent;
		public UIEquipSlot[] weaponslots = new UIEquipSlot[2];
		[SerializeField] private GameObject WeaponContent;
		public EquipmentSystem equipSys;

		public Text health_text;
		public Text damage_text;
		public Text defense_text;
		public Text level_text;


		// Use this for initialization
		void Start () {
			player = bagmanager.player;
			int i = 0;
			foreach (Transform child in ArmorContent.transform) {  
	//			Debug.Log ("Child name is" + child.name);
				armorslots [i++] = child.gameObject.GetComponent<UIEquipSlot> ();  
			}
			i = 0;
			foreach (Transform child in WeaponContent.transform) {  
	//			Debug.Log ("Child name is" + child.name);
				weaponslots [i++] = child.gameObject.GetComponent<UIEquipSlot> ();  
			}
		}
		
		// Update is called once per frame
		void Update () {
			//check if we need open or close the gui of bag
			if (Input.GetButtonDown("Character") && armorObject.activeSelf) {
				closeGui ();
			} else if (Input.GetButtonDown("Character") && !armorObject.activeSelf) {
				openGui ();
			}
		}

		public void updateGui(){
			//check player.equips, then show the icons
			if (player.equips.head != null) {
				armorslots [0].newAssign (player.equips.head, null);
			}
			if (player.equips.chest != null) {
				armorslots [1].newAssign (player.equips.chest, null);
			}
			if (player.equips.legs != null) {
				armorslots [2].newAssign (player.equips.legs, null);
			}
			if (player.equips.shoes != null) {
	//			Debug.Log ("now shoes = " + player.equips.shoes);
				armorslots [3].newAssign (player.equips.shoes, null);
	//			Debug.Log ("Now equip: " + player.equips.shoes.Name);
			}
			if (player.equips.weapon != null) {
				weaponslots [0].newAssign (player.equips.weapon, null);
				equipSys.chosenHandRightIndex = 1;

			}
			if (player.equips.shield != null) {
				weaponslots [1].newAssign (player.equips.shield, null);
				equipSys.chosenHandLeftIndex = 4;
			}


			equipSys.UpdateChoicesEquipment ();



			float defense = getDefense ();
			float damage = getDamage ();
			int level = getLevel ();
			int health = getHealth ();

			defense_text.text = defense.ToString();
			damage_text.text = damage.ToString();
			level_text.text = level.ToString();
			health_text.text = health.ToString();
		}
			
		//function that contorls gui to show/hide
		void openGui(){
			//updateGui ();
			armorObject.SetActive (true);

			float defense = getDefense ();
			float damage = getDamage ();
			int level = getLevel ();
			int health = getHealth ();

			defense_text.text = defense.ToString();
			damage_text.text = damage.ToString();
			level_text.text = level.ToString();
			health_text.text = health.ToString();
		}

		void closeGui(){
			armorObject.SetActive (false);
		}

		float getDefense(){
			float defense = 2.0f;
			if (player.equips.head != null) {
				defense += 1 * player.equips.head.Defense;
			}
			if (player.equips.chest != null) {
				defense += 2 * player.equips.chest.Defense;
			}
			if (player.equips.legs != null) {
				defense += 2 * player.equips.legs.Defense;
			}

			Debug.Log ("Current Defense = " + defense);
			return defense;
		}

		float getDamage(){
			float damage = 3.0f;
			if (player.equips.weapon != null) {
				damage += 3 * player.equips.weapon.Damage;
			}
			if (player.equips.shield != null) {
				damage += 1 * player.equips.shield.Damage;
			}

			Debug.Log ("Current Damage = " + damage);
			return damage;
		}

		int getLevel(){
			int level = 10;
			if (player.equips.weapon != null) {
				level += 3 * player.equips.weapon.QualityLevel;
			}

			if (player.equips.shield != null) {
				level += 2 * player.equips.shield.QualityLevel;
			}

			if (player.equips.head != null) {
				level += 1 * player.equips.head.QualityLevel;
			}

			if (player.equips.chest != null) {
				level += 1 * player.equips.chest.QualityLevel;
			}

			if (player.equips.legs != null) {
				level += 1 * player.equips.legs.QualityLevel;
			}

			if (player.equips.shoes != null) {
				level += 1 * player.equips.shoes.QualityLevel;
			}
			Debug.Log ("Current level = " + level);

			level = level / 10;

			return level;

		}

		int getHealth(){
			int health = 10;
			int level = getLevel ();
			if (level < 10) {
				health += level * 8;
			} else if (level < 20) {
				health += level * 6;
			}

			return health;
		}



	}
}
