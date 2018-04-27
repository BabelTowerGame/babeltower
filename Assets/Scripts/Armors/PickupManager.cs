using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;
using System.Text.RegularExpressions;

public struct Pickitem {
	public AutoAttack monster;
	public int index;
	public int id;

	public Pickitem(AutoAttack x, int y, int id){
		this.monster = x;
		this.index = y;
		this.id = id;
	}
}

public class PickupManager : MonoBehaviour {

	public List<Pickitem> pickupList = new List<Pickitem>();
	public GameObject window;
	public UIItemSlot[] slots = new UIItemSlot[42];
	[SerializeField] private GameObject Content;
	public ItemDB DB;
    public GameObject playerObject;
   



	// Use this for initialization
	void Start () {
        playerObject = GameObject.FindWithTag("Player");
        int i = 0;
		foreach (Transform child in Content.transform)  
		{  
			//Debug.Log ("Child name is" + child.name);
			slots [i++] = child.gameObject.GetComponent<UIItemSlot>();  
		}
		DB = ItemDB.Instance;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pickup") && window.activeSelf) {
//			Debug.Log ("close the gui!!!");
			closeGui ();
		} else if (Input.GetButtonDown ("Pickup") && !window.activeSelf) {
			Debug.Log ("open the gui!!!");
			//update then show the window
//			updateGui();
			openGui ();
		}
//		if (window.activeSelf) {
////			update too fast
//			if (!IsInvoking ()) {
//				InvokeRepeating ("pickUpListGen", 1.0f, 2.5f); 
//			}
//			updateGui ();
//		} else if (!window.activeSelf) {
//			CancelInvoke();
//		}
	}

	void openGui(){
		window.SetActive (true);
		pickUpListGen ();
	}

	void closeGui(){
		window.SetActive (false);
	}

	void pickUpListGen(){
		pickupList.Clear ();
		foreach (Collider item in Physics.OverlapSphere (playerObject.transform.position, 3.0f)) {
            //Debug.Log(item.tag);
			if (item.tag == "Monster") {
				//if the monster is dead
				item.GetComponent<AutoAttack>().applyDamage(30, this.transform);
				Debug.Log ("monster health = " + item.GetComponent<Monster> ().Current_health);
				if (item.GetComponent<AutoAttack> ().LootReady) {
					AutoAttack tempmon = item.GetComponent<AutoAttack> ();
					Debug.Log ("Monster = " + tempmon);
					int[] templist = item.GetComponent<Monster> ().LootList;
					Debug.Log ("item amount = " + templist.Length);
					for (int i = 0; i < templist.Length; i++) {
						if (templist [i] != -1) {
							Pickitem temp = new Pickitem (tempmon, i, templist[i]);
//							Debug.Log ("add item" + temp);
							if (pickupList.Count < 42) {
								pickupList.Add (temp);
							} else {
								return;
							}
						}
					}
				}
			}
		}

//		if (pickupList.Count > 0) {
//			for (int i = 0; i < pickupList.Count; i++) {
//				Debug.Log ("Pick up monster : " + pickupList [i].monster);
//				Debug.Log ("Pick up item : " + pickupList [i].index);
//
//			}
//			Debug.Log ("Found: " + pickupList.Count);
//		} else {
//			Debug.Log ("Nothing found.");
//		}

		updateGui ();
	}

	void updateGui(){
		for (int i = 0; i < pickupList.Count; i++) {
//			Debug.Log ("item id = " + pickupList[i].id);
			Item it = DB.getByID(pickupList[i].id);
//			Debug.Log ("Item = " + it);
			slots [i].newAssign (it, null);
		}

		//clear other things
		for (int i = pickupList.Count; i < 42; i++) {
			slots [i].Unassign();
		}
	}

	public void deleteItem(int index){
		if (index < pickupList.Count) {
			pickupList.RemoveAt (index);
			updateGui ();
		}
	}
}
