using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;
using System.Text.RegularExpressions;


public class BagManager : MonoBehaviour {
	//public List<Item> baglist;
	//public int maxValue;
	//window
	public GameObject bagObject;
	public Character player;
	public UIItemSlot[] slots = new UIItemSlot[42];
	[SerializeField] private GameObject Content;
	//only for testing
	[SerializeField] private Sprite Shoes;
	int lastCount;



	void Awake(){

	}

	// Use this for initialization
	void Start () {
		int i = 0;
		foreach (Transform child in Content.transform)  
		{  
			//Debug.Log ("Child name is" + child.name);
			slots [i++] = child.gameObject.GetComponent<UIItemSlot>();  
		}

		//only for testing
		testing();
	}
	
	// Update is called once per frame
	void Update () {
		//check if we need open or close the gui of bag
		if (Input.GetButtonDown("Inventory") && bagObject.activeSelf) {
//			Debug.Log ("close the gui!!!");
			closeGui ();
		} else if (Input.GetButtonDown("Inventory") && !bagObject.activeSelf) {
//			Debug.Log ("open the gui!!!");
			openGui ();
		}
		
	}

	//add items to bag
	public void addItem(Item item){
		if (player.inventory.list.Count < player.inventory.capacity) {
			player.inventory.list.Add (item);
			//if the window is open, update immediately
			if (bagObject.activeSelf) {
				updateGui ();
			}

		} else {
			Debug.Log ("Exceeds the capacity");
		}
	}

	/* Delete items by it own */
	public void deleteItem(Item obj){
		if (player.inventory.list.Contains (obj)) {
			//delete
//			Debug.Log("Delete..." + obj.Name);
			player.inventory.list.Remove (obj);
			//if the window is open, update immediately
			if (bagObject.activeSelf) {
				//when delete, we need to unassign the last item
				slots[player.inventory.list.Count].Unassign();
				updateGui ();
			}

			//TODO: when a character throw out a item, drop it into the environment.

		} else {
			Debug.Log ("not correct item!");
		}
	}

	/* Delete items by its index */
	public void deleteByID(int i){
		if (i < player.inventory.list.Count) {
			player.inventory.list.RemoveAt (i);
			if (bagObject.activeSelf) {
				//when delete, we need to unassign the last item
				slots [player.inventory.list.Count].Unassign ();
				updateGui ();
			}
		}
	}

	/* Use the new item on index i */
	public void replaceItem(int i, Item temp){
		if (player.inventory.list.Count > i) {
			player.inventory.list [i] = temp;
			//update the icon
			slots [i].newAssign (player.inventory.list [i], null);
		}
	}

	//we need to update the gui everytime when we open
	public void updateGui(){
		int i;
//		Debug.Log ("count = " + player.inventory.list.Count);
		for(i = 0; i < player.inventory.list.Count; i++){
			//TODO: to show the items in the bag
//			Debug.Log("Add the " + i + " item.");
			if (player.inventory.list [i] == null) {
				Debug.Log ("Error: NULL!");
			} else {
				slots [i].newAssign (player.inventory.list [i], null);
			}
		}
	}

	//function that contorls gui to show/hide
	void openGui(){
		updateGui ();
		bagObject.SetActive (true);
	}

	void closeGui(){
		bagObject.SetActive (false);
	}

	void testing(){
		Item testItem = new global::Shoes().Init(1, "1shoes", 10, 5);
		testItem.Icon = Shoes;
		Item testchest = new global::Armor ().Init (2, "chest", 5, Armor.armor_type.chest, 4);
		testchest.Icon = Shoes;
		Item testhead = new Armor().Init (3, "head", 10, Armor.armor_type.head, 5);
		testhead.Icon = Shoes;
		Item testleg = new Armor().Init (4, "leg", 5, Armor.armor_type.leg, 4);
		testleg.Icon = Shoes;

		Item replaceShoes = new global::Shoes().Init (5, "2shoes", 20, 50);

		player.inventory.list.Add(testItem);
		player.inventory.list.Add (testchest);
		player.inventory.list.Add (testhead);
		player.inventory.list.Add (testleg);
		player.inventory.list.Add (replaceShoes);

		Debug.Log (player.inventory.list.Count);
	}

}
