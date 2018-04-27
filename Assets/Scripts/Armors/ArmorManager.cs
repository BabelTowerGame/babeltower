using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;
using UnityEngine.UI;
using Tob;


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
		public Text speed_text;


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

		//function to update the armor gui
		public void updateGui(){
			//check player.equips, then show the icons
			if (player.equips.head != null) {
				armorslots [0].newAssign (player.equips.head, null);
			} else {
				armorslots [0].Unassign ();
			}
			if (player.equips.chest != null) {
				armorslots [1].newAssign (player.equips.chest, null);
			} else {
				armorslots [1].Unassign ();
			}
			if (player.equips.legs != null) {
				armorslots [2].newAssign (player.equips.legs, null);
			} else {
				armorslots [2].Unassign ();
			}

			if (player.equips.shoes != null) {
				//			Debug.Log ("now shoes = " + player.equips.shoes);
				armorslots [3].newAssign (player.equips.shoes, null);
				//			Debug.Log ("Now equip: " + player.equips.shoes.Name);
			} else {
				armorslots [3].Unassign ();
			}
			if (player.equips.weapon != null) {
				weaponslots [0].newAssign (player.equips.weapon, null);
				equipSys.chosenHandRightIndex = 1;
			} else {
				weaponslots [0].Unassign ();
				equipSys.chosenHandRightIndex = 0;
			}
			if (player.equips.shield != null) {
				weaponslots [1].newAssign (player.equips.shield, null);
				equipSys.chosenHandLeftIndex = 4;
			} else {
				weaponslots [1].Unassign ();
				equipSys.chosenHandLeftIndex = 0;
			}


			equipSys.UpdateChoicesEquipment ();
			//send message to server
			sendMessage ();
			updateStat ();
		}

		void updateStat(){
			float defense = getDefense ();
			float damage = getDamage ();
			int level = getLevel ();
			int health = getHealth ();
			int speed = getSpeed ();

			defense_text.text = defense.ToString();
			damage_text.text = damage.ToString();
			level_text.text = level.ToString();
			health_text.text = health.ToString();
			speed_text.text = speed.ToString ();
		}
			

		//function that contorls gui to show/hide
		void openGui(){
			//updateGui ();
			armorObject.SetActive (true);

			float defense = getDefense ();
			float damage = getDamage ();
			int level = getLevel ();
			int health = getHealth ();
			int speed = getSpeed ();

			defense_text.text = defense.ToString();
			damage_text.text = damage.ToString();
			level_text.text = level.ToString();
			health_text.text = health.ToString();
			speed_text.text = speed.ToString ();
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

//			Debug.Log ("Current Defense = " + defense);
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

//			Debug.Log ("Current Damage = " + damage);
			return damage;
		}

		int getLevel(){
			int level = 10;
			if (player.equips.weapon != null) {
				level += 5 * player.equips.weapon.QualityLevel;
			}

			if (player.equips.shield != null) {
				level += 2 * player.equips.shield.QualityLevel;
			}

			if (player.equips.head != null) {
				level += 3 * player.equips.head.QualityLevel;
			}

			if (player.equips.chest != null) {
				level += 3 * player.equips.chest.QualityLevel;
			}

			if (player.equips.legs != null) {
				level += 3 * player.equips.legs.QualityLevel;
			}

			if (player.equips.shoes != null) {
				level += 3 * player.equips.shoes.QualityLevel;
			}
//			Debug.Log ("Current level = " + level);

			level = level / 10;

			return level;

		}

		int getSpeed(){
			int speed = 2;
			if (player.equips.shoes != null) {
				speed += player.equips.shoes.Speed;
			}
			return speed;
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

		//unequipped the item
		public void deleteEquip(string name){
			if (name == "Head") {
				player.equips.head = null;
			}
			if (name == "Chest") {
				player.equips.chest = null;
			}
			if (name == "Leg") {
				player.equips.legs = null;

			}
			if (name == "Shoes") {
				player.equips.shoes = null;
			}
			if (name == "Weapon") {
				player.equips.weapon = null;
			}
			if (name == "Shield") {
				player.equips.shield = null;
			}
			updateGui ();
		}

		public bool ifExist(string name){
			if (name == "Head") {
				return player.equips.head != null;
			}
			if (name == "Chest") {
				return player.equips.chest != null;
			}
			if (name == "Leg") {
				return player.equips.legs != null;
			}
			if (name == "Shoes") {
				return player.equips.shoes != null;
			}
			if (name == "Weapon") {
				return player.equips.weapon != null;
			}
			if (name == "Shield") {
				return player.equips.shield != null;
			}
			return false;
		}

		//server message
		void sendMessage(){
			NetworkService ns = NetworkService.Instance;
			PlayerEquiped equips = new PlayerEquiped ();
			if (player.equips.weapon != null) {
				equips.Weapon = player.equips.weapon.ID.ToString ();
			} else {
				equips.Weapon = "";
			}
			if (player.equips.head != null) {
				equips.Head = player.equips.head.ID.ToString ();
			} else {
				equips.Head = "";
			}
			if (player.equips.chest != null) {
				equips.Chest = player.equips.chest.ID.ToString ();
			} else {
				equips.Chest = "";
			}
			if (player.equips.legs != null) {
				equips.Legs = player.equips.legs.ID.ToString ();
			} else {
				equips.Legs = "";
			}
			if (player.equips.shoes != null) {
				equips.Shoes = player.equips.shoes.ID.ToString ();
			} else {
				equips.Shoes = "";
			}
			if (player.equips.shield != null) {
				equips.Shield = player.equips.shield.ID.ToString ();
			} else {
				equips.Shield = "";
			}

			PlayerEvent playerEvent = new PlayerEvent ();
//			playerEvent.Id = NetworkID.Local_ID; //local player id
			playerEvent.Type = Tob.PlayerEventType.PlayerEquipped; //PLAYER_EQUIPPED = 9;
			playerEvent.Equiped = equips;

			Tob.Event e = new Tob.Event ();
			e.Topic = Tob.EventTopic.PlayerEvent;
			e.P = playerEvent;

			//TODO: send event message
//			ns.SendEvent (e);
		}
	}
}
