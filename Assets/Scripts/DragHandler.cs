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
	//outer use
	public BagManager bagManager;
	public Character player;

	// Use this for initialization
	void Start()
	{

		myTransform = this.transform;
		myRectTransform = this.transform as RectTransform;
		canvasGroup = GetComponent <CanvasGroup>();
		originalPosition = myTransform.position;
		//link the players
		player = bagManager.player;
	}

	void Update()
	{
		
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
//		GameObject curEnter = eventData.pointerEnter;

	}


	public void OnEndDrag(PointerEventData eventData)
	{
		myTransform.position = originalPosition;
		//the gameobject we want to move into
		GameObject curEnter = eventData.pointerEnter;
		//Debug.Log ("End Dragging... ");

		//out of the bag, back to the slot
		if (curEnter.tag != "Slot") {
			Debug.Log ("Out! Moving back...");
			//myTransform.position = originalPosition;
		} else {
			//get the new slot id
			newID = Regex.Replace (curEnter.name, @"[^\d.\d]", ""); 
			if(int.Parse(newID) <= player.inventory.list.Count){
				//if exchange two items in the list
				myTransform.position = originalPosition;
				
				int old_slot = int.Parse (oldID) - 1;
				int new_slot = int.Parse (newID) - 1;
				Item temp;
				if (old_slot > player.inventory.list.Count) {
					return;
				}
				//Debug.Log("old slot = " + old_slot);
				//Debug.Log ("new slot: " + new_slot);
				temp = player.inventory.list [old_slot];
				//Debug.Log ("Now item: " + temp.Name);
				player.inventory.list[old_slot] = player.inventory.list[new_slot];
				player.inventory.list [new_slot] = temp;

			} else {
				//if move to the end
				int i;
				Item temp = player.inventory.list [int.Parse(oldID) - 1];
				Debug.Log ("Change items: " + temp.Name);
				for(i = int.Parse(oldID) - 1; i < player.inventory.list.Count - 1; i++){
					Debug.Log ("Change items: " + player.inventory.list [i].Name + " , " + player.inventory.list [i+1].Name);
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
}