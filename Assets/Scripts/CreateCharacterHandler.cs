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
        submitButton.onClick.AddListener(onCreateCharacter);
    }


    protected void OnDisable() {
        submitButton.onClick.RemoveListener(onCreateCharacter);
    }

    public void onCreateCharacter() {

		string name = nameField.text;

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

        CharacterManager.character.setGender(gender);
        CharacterManager.character.Name = name;

        //Character.Equipped eq = new Character.Equipped();
        //CharacterManager.character.equips = eq;
        //eq.chest = (Armor)ItemDB.Instance.getByID(3);
        //eq.legs = (Armor)ItemDB.Instance.getByID(4);
        //eq.head = (Armor)ItemDB.Instance.getByID(5);
        //eq.shield = (Shield)ItemDB.Instance.getByID(2);
        //eq.weapon = (Weapon)ItemDB.Instance.getByID(1);
        //eq.shoes = (Shoes)ItemDB.Instance.getByID(6);




        Debug.Log("Character created: name: " + name + " gender: " + genderString + " hairColor: " + hairColorString + " " + hairColorVal);

	}


}
