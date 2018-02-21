using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject GameManager;
    public GameObject SoundManager;

    void Awake() {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        //if (GameManager.Instance == null)

            //Instantiate gameManager prefab
            Instantiate(GameManager as GameObject);
    }


}
