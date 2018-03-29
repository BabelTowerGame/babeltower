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

	}


	public void OnEndDrag(PointerEventData eventData)
	{
		lastID = oldID;
		//Debug.Log ("end : lastID = " + lastID);

		myTransform.position = originalPosition;
		this.transform.position = originalPosition;
		//the gameobject we want to move into
		GameObject curEnter = eventData.pointerEnter;
		//Debug.Log ("End Dragging... ");

		//Debug.Log ("CurEnter = " + curEnter);

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
			Debug.Log ("Out! Moving back...");
			//myTransform.position = originalPosition;

			//if we drag it to the background, set a confirm window for delete it
			if (curEnter.name == "Background") {
				if (!confirmWindow.activeSelf) {
					confirmWindow.SetActive (true);
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

//				if (old_slot > player.inventory.list.Count) {
//					return;
//				}
				Debug.Log("old slot = " + old_slot);
				Debug.Log ("new slot = " + new_slot);
				Debug.Log ("Drag item: " + temp.Name);

				player.inventory.list[old_slot] = player.inventory.list[new_slot];
				player.inventory.list[new_slot] = temp;
				for (int i = 0; i < player.inventory.list.Count; i++) {
					Debug.Log (i + " " + player.inventory.list [i].Name);
				}

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
			Debug.Log ("Updating bag...");
			//update the bag now
			bagManager.updateGui ();
		}

		//reset
		canvasGroup.blocksRaycasts = true; 
	}

	public void deleteItem(){
		Debug.Log ("lastID = " + this.transform.name);
		oldID = Regex.Replace(this.name, @"[^\d.\d]", "");
		int old_slot = int.Parse (oldID) - 1;
		//Item temp = player.inventory.list [old_slot];
		bagManager.deleteByID(old_slot);
		//close the confirm window
		confirmWindow.SetActive (false);
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