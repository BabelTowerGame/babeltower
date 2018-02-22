using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterApperaenceSaver : MonoBehaviour {

	public InputField nameField;
	public Button submitButton;
	public Toggle male;
	public Toggle female;
	public Toggle hairColorBlack;
	public Toggle hairColorYellow;
	public Toggle hairColorRed;


	public void loadData() {
		string name = nameField.text;
		Appearance.Gender gender;
		Debug.Log (name);

		if (male.isOn) {
			gender = Appearance.Gender.Male;
			Debug.Log ("male");
		} else if (female.isOn) {
			gender = Appearance.Gender.Female;
			Debug.Log ("female");
		}
	}

//	public Character createCharacter(string name, Appearance.Gender gender){
//		return new Character (name, app);
//	}

}
