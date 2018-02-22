using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

public class Screen_setting : MonoBehaviour {
	[SerializeField] private GameObject Resolution;
	[SerializeField] private GameObject Display;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Change_Resolution(){
		GameObject  ChildGameObject1 = Resolution.transform.GetChild (0).gameObject;
		Text txt = ChildGameObject1.GetComponent<Text> ();
		string temp_str = txt.text;
		//Debug.Log (temp_str);
		string[] sArray=temp_str.Split('X');
		int firstval = Int32.Parse(sArray[0]);
		int secval = Int32.Parse(sArray[1]);
		GameObject  ChildGameObject2 = Display.transform.GetChild (0).gameObject;
		Text txt1 = ChildGameObject2.GetComponent<Text> ();
		Debug.Log (txt1.text);
		if (txt1.text == "Full Screen") {
			Screen.SetResolution (firstval, secval, true, 0);
			Debug.Log ("Change into Full Screen Mode");
		} else {
			Screen.SetResolution (firstval, secval, false, 0);
		}

	}
	/*public void Change_Display(){
		GameObject  ChildGameObject1 = m_link.transform.GetChild (0).gameObject;
		Text txt = ChildGameObject1.GetComponent<Text> ();
		string temp_str = txt.text;
		string[] sArray=temp_str.Split('X');
		int firstval = Int32.Parse(sArray[0]);
		int secval = Int32.Parse(sArray[1]);
		if (this.m_SelectedItem == "Full Screen") {
			Screen.SetResolution (firstval, secval, true, 0);
		} else {
			Screen.SetResolution (firstval, secval, false, 0);
		}

	}*/
}
