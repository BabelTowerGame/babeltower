using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tob;

public class NetworkPlayerManager : MonoBehaviour {

    public GameObject MalePrefab;
    public GameObject FemalePrefab;

    private List<GameObject> players;


    //TODO: parse msg to player class
    void OnPlayerEnter(PlayerEvent e) {
        Appearance app = new Appearance();

    }

    void OnPlayerExit(PlayerEvent e) {


    }

    void OnPlayerCrouch(PlayerEvent e) {

    }

    void OnPlayerDamaged(PlayerEvent e) {

    }

    void OnPlayerCast(PlayerEvent e) {

    }

    void OnPlayerDie(PlayerEvent e) {

    }

    void OnPlayerEquipped(PlayerEvent e) {

    }

    void OnPlayerJump(PlayerEvent e) {

    }

    void OnPlayerMove(PlayerEvent e) {

    }

    void OnPlayerPosition(PlayerEvent e) {

    }

	void OnPlayerAnimation(PlayerEvent e) {

    }


}
