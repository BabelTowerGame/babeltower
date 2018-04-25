using DuloGames.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrameController : MonoBehaviour {

    public GameObject ProgressBar;
    public GameObject AvaterFrame;
    public GameObject LevelFrame;

    private Text progressBarTxt;
    private UIProgressBar progressBarController;
    
    private Text levelTxt;

    // Use this for initialization
    void Start () {
        progressBarTxt = ProgressBar.GetComponentInChildren<Text>();
        progressBarController = ProgressBar.GetComponent<UIProgressBar>();

        levelTxt = LevelFrame.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        float healthPercent = 9.0f / 10.0f;//CharacterManager.character.CurrentHealth / CharacterManager.character.MaxHealth;
        progressBarController.fillAmount = healthPercent;
        progressBarTxt.text = string.Format("{0:F2}%", healthPercent * 100);

        levelTxt.text = "1";
	}
}
