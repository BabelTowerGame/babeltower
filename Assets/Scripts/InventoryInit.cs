using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

	public class InventoryInit : MonoBehaviour {
		private Inventory Player_Inventory;
		[SerializeField] private GameObject Content;
		[SerializeField] private Sprite Shoes;
		UIItemSlot[] slots = new UIItemSlot[42];
		public Inventory Inventory{
			get { return this.Player_Inventory; }
			set { this.Player_Inventory = value; }
		}
		// Use this for initialization
		void Start () {
		//Uncomment this after testing
		/*if (this.Player_Inventory == null)
		{
			//this.Destruct();
			return;
		}*/
		//Create a dummy inventory
		Player_Inventory = new Inventory();
		Item testItem = new Item().Init(0,"Bogos shoes", 100);
		testItem.Icon = Shoes;
		testItem.ID = 0;
		testItem.Type = Item.ItemType.Shoes;
		Player_Inventory.list.Add(testItem);
			int i = 0;
			foreach (Transform child in Content.transform)  
			{  
			//Debug.Log ("Child name is" + child.name);
				slots [i++] = child.gameObject.GetComponent<UIItemSlot>();  
			}  
			for (int j = 0; j < Player_Inventory.list.Count; j++) {
			//Debug.Log ("Adding item into Inventory  "+Player_Inventory.list[j].Name);
				slots [j].newAssign (Player_Inventory.list[j], null);
			//Debug.Log (slots [j].gameObject.GetInstanceID ());
			
			}
		}


		// Update is called once per frame
		void Update () {

		}
	}
	