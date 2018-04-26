using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;

public class Character_change : MonoBehaviour {
	[SerializeField] private GameObject Hookobj;
	[SerializeField] private Sprite source1;
	[SerializeField] private Toggle Hooktoggle;
    [SerializeField] private GameObject male;
    [SerializeField] private GameObject female;
    //[SerializeField] private Sprite source2;
    // Use this for initialization
    void Start () {
		Hooktoggle.onValueChanged.AddListener (Outlookchange);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Outlookchange(bool flag){
		if (flag == true) {
			Image img = Hookobj.GetComponent<Image>();
			img.sprite = source1;
            male.SetActive(!male.activeSelf);
            female.SetActive(!female.activeSelf);
        }
	}
}
