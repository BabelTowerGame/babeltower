using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterHandler : MonoBehaviour {

	public InputField nameField;
	public Button submitButton;
    public ToggleGroup genderToggle;
	public ToggleGroup hairToggle;


    protected void OnEnable() {
        submitButton.onClick.AddListener(loadData);
    }


    protected void OnDisable() {
        submitButton.onClick.RemoveListener(loadData);
    }

    public void loadData() {

        //ConstIntDB db = ConstIntDB.Instance;
		string name = nameField.text;
		//Appearance.Gender gender;

        Toggle t = genderToggle.ActiveToggles().FirstOrDefault();
        string genderString = t.gameObject.transform.name.ToUpper();
        t = hairToggle.ActiveToggles().FirstOrDefault();
        string hairColorString =  ("COLOR_" + t.gameObject.transform.name).ToUpper();

        Appearance.Gender gender = Appearance.Gender.Male;
        switch(genderString) {
            case "MALE": gender = Appearance.Gender.Male; break;
            case "FEMALE": gender = Appearance.Gender.Female; break;
            default: Debug.LogError("unexpected gender"); break;
        }

        uint hairColorVal = ConstIntDB.Instance.getByKey(hairColorString).Value;

        Appearance ap = new Appearance();
        ap.gender = gender;
        //ap.hairColor = 
        //ap.skinColor = 

        Character character = new Character(name, ap);


        Debug.Log("Character created: name: " + name + " gender: " + genderString + " hairColor: " + hairColorString + " " + hairColorVal);

        //TODO: place character data to Global Game Manager

		//if (male.isOn) {
		//	gender = Appearance.Gender.Male;
		//	Debug.Log ("male");
		//} else if (female.isOn) {
		//	gender = Appearance.Gender.Female;
		//	Debug.Log ("female");
		//}

			
	}

//	public Character createCharacter(string name, Appearance.Gender gender){
//		return new Character (name, app);
//	}

}
