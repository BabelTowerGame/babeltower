using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	//positions
	private Transform myTransform;
	private RectTransform myRectTransform;
	private CanvasGroup canvasGroup;
	//position for store
	public Vector3 originalPosition;
	//slot ids
	string newID;
	string oldID;
	string lastID;
	//outer use
	public BagManager bagManager;
	public Character player;
	public GameObject confirmWindow;
	public EasyEquipmentSystem.ArmorManager armorManager;


	// Use this for initialization
	void Start()
	{

		myTransform = this.transform;
		myRectTransform = this.transform as RectTransform;
		canvasGroup = GetComponent <CanvasGroup>();
		originalPosition = myTransform.position;
		//link the players
		player = bagManager.player;
		lastID = oldID;
	}

	void Update()
	{
		lastID = oldID;
		gameObject.transform.SetAsLastSibling();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = false;
		//GameObject ob =Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
		//the original position of the object
		originalPosition = myTransform.position;
		//make it on the top of others
		myTransform.SetAsLastSibling();
		//Debug.Log("Start Dragging..." + this);
		oldID = Regex.Replace(this.name, @"[^\d.\d]", "");
		lastID = oldID;
		//Debug.Log ("begin: lastID = " + lastID);

		//we cannot move a empty item
//		if (int.Parse(oldID) > player.inventory.list.Count) {
//			return;
//		}
		//get the original image
		//oriOb = GetComponentInChildren<Image>;
		//Debug.Log ("source image..." + oriOb);
	}


	public void OnDrag(PointerEventData eventData)
	{
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
		{
			//make the object with mouse move
			//Debug.Log("Moving..." + this);
			myTransform.position = globalMousePos;
		}
		lastID = oldID;
//		GameObject curEnter = eventData.pointerEnter;
		gameObject.transform.SetAsLastSibling();

	}


	public void OnEndDrag(PointerEventData eventData)
	{
		gameObject.transform.SetAsLastSibling();

		lastID = oldID;
		//Debug.Log ("end : lastID = " + lastID);

		myTransform.position = originalPosition;
		this.transform.position = originalPosition;
		//the gameobject we want to move into
		GameObject curEnter = eventData.pointerEnter;
		//Debug.Log ("End Dragging... ");
		Debug.Log ("CurEnter = " + curEnter.name);
        

		//if out of the window
		if (curEnter == null) {
			//Debug.Log ("CurEnter2 = " + curEnter);
			canvasGroup.blocksRaycasts = true; 
			return;
		}

		//we cannot move a empty item
		if (int.Parse(oldID) > player.inventory.list.Count) {
			//Debug.Log ("oldID = " + oldID);

			canvasGroup.blocksRaycasts = true; 
			return;
		}

		//get old item(which we are dragging)
		int old_slot = int.Parse (oldID) - 1;
		Item temp = player.inventory.list [old_slot];

		//out of the bag, back to the slot
		if (curEnter.tag != "Slot") {
//			Debug.Log ("Out! Moving back...");
			//myTransform.position = originalPosition;
//			Item tempItem = player.inventory.list [old_slot];
			if (curEnter.name == "Icon") {
				curEnter = curEnter.transform.parent.gameObject;
				Debug.Log ("test: " + curEnter.name);
			}
			if (curEnter.tag == "Equip") {
//				Debug.Log (curEnter.name);
				if (temp.Type.ToString () == curEnter.name.ToString () && temp.Type.ToString() == "Shoes"){
					if (player.equips.shoes == null) {
						player.equips.shoes = (Shoes)temp;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByIndex(old_slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.shoes;
						player.equips.shoes = (Shoes)temp;
						bagManager.replaceItem (old_slot, tempItem);
					}
				} else if (temp.Type.ToString () == "Armor"){
					//TODO: we have 3 types of armors
					Armor temp2 = (Armor)player.inventory.list [old_slot];
					if(temp2.Armor_Type == Armor.armor_type.chest && curEnter.name.ToString() == "Chest"){
						if (player.equips.chest == null) {
							player.equips.chest = temp2;
							//TODO: After equipped, delete the item from inventory
							bagManager.deleteByIndex(old_slot);
						} else {
							//replace with the current item
							Item tempItem = player.equips.chest;
							player.equips.chest = temp2;
							bagManager.replaceItem (old_slot, tempItem);
						}
					} else if(temp2.Armor_Type == Armor.armor_type.head && curEnter.name.ToString() == "Head"){
						if (player.equips.head == null) {
							player.equips.head = temp2;
							//TODO: After equipped, delete the item from inventory
							bagManager.deleteByIndex(old_slot);
						} else {
							//replace with the current item
							Item tempItem = player.equips.head;
							player.equips.head = temp2;
							bagManager.replaceItem (old_slot, tempItem);
						}
					} else if(temp2.Armor_Type == Armor.armor_type.leg && curEnter.name.ToString() == "Leg"){
						if (player.equips.legs == null) {
							player.equips.legs = temp2;
							//TODO: After equipped, delete the item from inventory
							bagManager.deleteByIndex(old_slot);
						} else {
							//replace with the current item
							Item tempItem = player.equips.legs;
							player.equips.legs = temp2;
							bagManager.replaceItem (old_slot, tempItem);
						}
					}
				} else if (temp.Type.ToString () == curEnter.name.ToString () && temp.Type.ToString() == "Weapon") {
					if (player.equips.weapon == null) {
						player.equips.weapon = (Weapon)temp;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByIndex (old_slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.weapon;
						player.equips.weapon = (Weapon)temp;
						bagManager.replaceItem (old_slot, tempItem);
					}

				} else if (temp.Type.ToString () == curEnter.name.ToString () && temp.Type.ToString() == "Shield") {
					if (player.equips.shield == null) {
						player.equips.shield = (Shield)temp;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByIndex (old_slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.shield;
						player.equips.shield = (Shield)temp;
						bagManager.replaceItem (old_slot, tempItem);
					}
				}
				armorManager.updateGui ();
			}

			//if we drag it to the background, set a confirm window for delete it
			if (curEnter.name == "Background") {
				if (!confirmWindow.activeSelf) {
					confirmWindow.SetActive (true);
					GameObject button = GameObject.Find("Delete_Yes_Button");
					button.GetComponent<DeleteButton> ().name = this.transform.name;
				}
				//deleteItem (temp);
			}

		} else {
			//get the new slot id
			newID = Regex.Replace (curEnter.name, @"[^\d.\d]", ""); 
			if(int.Parse(newID) <= player.inventory.list.Count){
				//if exchange two items in the list
				myTransform.position = originalPosition;
				
				int new_slot = int.Parse (newID) - 1;

				player.inventory.list[old_slot] = player.inventory.list[new_slot];
				player.inventory.list[new_slot] = temp;
//				for (int i = 0; i < player.inventory.list.Count; i++) {
//					Debug.Log (i + " " + player.inventory.list [i].Name);
//				}

			} else {
				//if move to the end
				int i;
				//Item temp = player.inventory.list [int.Parse(oldID) - 1];
				//Debug.Log ("Change items: " + temp.Name);
				for(i = int.Parse(oldID) - 1; i < player.inventory.list.Count - 1; i++){
					//Debug.Log ("Change items: " + player.inventory.list [i].Name + " , " + player.inventory.list [i+1].Name);
					player.inventory.list[i] = player.inventory.list[i + 1];
				}
				player.inventory.list [i] = temp;
			}
//			Debug.Log ("Updating bag...");
			//update the bag now
			bagManager.updateGui ();
		}

		//reset
		canvasGroup.blocksRaycasts = true; 
	}





	public void closeWindow(){
		if (confirmWindow.activeSelf) {
			confirmWindow.SetActive (false);
		}
		//Debug.Log ("cancel lastID = " + lastID);
		//canvasGroup.blocksRaycasts = true; 

	}
}