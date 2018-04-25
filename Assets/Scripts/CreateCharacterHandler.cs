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
        CharacterManager.character.Level = 1;
        CharacterManager.character.CurrentHealth = 100;
        CharacterManager.character.MaxHealth = 100;
        Debug.Log("Character created: name: " + name + " gender: " + genderString + " hairColor: " + hairColorString + " " + hairColorVal);

	}


}
