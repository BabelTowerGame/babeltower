using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    //private DataManager dataManager;

    public GameManager Instance {
        get { return GameManager.instance; }
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        initGame();
    }
	
	// Update is called once per frame
	void Update () {
        //TODO: get update from network interface
        //      sync local changes to network partners
        //      update game data and graphics
		
	}

    private void initGame() {
        //TODO: init game data from const field,
        //      load game state and init network interface
       
    }
}
