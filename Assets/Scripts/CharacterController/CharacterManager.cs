using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

    public static Character character;
    public GameObject hairColorToggleObject;
    public GameObject[] charObjects;
    private List<EasyEquipmentSystem.EquipmentSystem> charEqs;

    // Use this for initialization
    void Start () {

        DontDestroyOnLoad(this);

        if(CharacterManager.character == null) {
            character = new Character();
        }

        Toggle[] hcToggles = hairColorToggleObject.GetComponentsInChildren<Toggle>();

        foreach(Toggle t in hcToggles) {
            t.onValueChanged.AddListener(delegate {
                this.onHairColorToggleChanged(t);
            });
        }

        charEqs = new List<EasyEquipmentSystem.EquipmentSystem>();
        foreach(GameObject obj in charObjects) {
            charEqs.Add(obj.GetComponent<EasyEquipmentSystem.EquipmentSystem>());
        }
	}

    public static Color32 ToColor(uint HexVal) {
        byte R = (byte)((HexVal >> 16) & 0xFF);
        byte G = (byte)((HexVal >> 8) & 0xFF);
        byte B = (byte)((HexVal) & 0xFF);
        return new Color32(R, G, B, 255);
    }

    void onHairColorToggleChanged(Toggle toggle) {
        string colorName = "COLOR_" + toggle.gameObject.name;
        Color32 color = ToColor(ConstIntDB.Instance.getByKey(colorName).Value);

        //Debug.Log(toggle.gameObject.name + " " + color.ToString());

        CharacterManager.character.setHairColor(color);

        foreach(var obj in charEqs) {
            obj.onHairColorChanged(color);
        }
    }


}
