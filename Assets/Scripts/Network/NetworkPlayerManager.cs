﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tob;

public class NetworkPlayerManager : MonoBehaviour {

    public GameObject MalePrefab;
    public GameObject FemalePrefab;

    private Dictionary<string, GameObject> players;

    public void Awake() {
        players = new Dictionary<string, GameObject>();
    }

    //TODO: parse msg to player class
    public void OnPlayerEnter(PlayerEvent e) {
        PlayerAppearance t = e.Appearance;
        Appearance app = new Appearance((Appearance.Gender)t.Gender, 
            CharacterManager.ToColor((uint)t.HairColor));

        Vector3 locaion = new Vector3(e.Position.X, e.Position.Y, e.Position.Z);

        GameObject go;
        
        if (app.gender == Appearance.Gender.Male) {
            go = GameObject.Instantiate(MalePrefab, locaion, Quaternion.identity) as GameObject;
        } else {
            go = GameObject.Instantiate(FemalePrefab, locaion, Quaternion.identity) as GameObject;
        }
        go.GetComponent<EasyEquipmentSystem.EquipmentSystem>().onHairColorChanged(app.hairColor);
        NetworkID id = go.GetComponent<NetworkID>();
        
        id.ID = e.Id;
        id.IsLocalPlayer = false;
        players.Add(e.Id, go);
    }

    public void OnPlayerExit(PlayerEvent e) {
        GameObject go;
        if(players.TryGetValue(e.Id, out go)) {
            Destroy(go);
            players.Remove(e.Id);
        }

    }

    public void OnPlayerCrouch(PlayerEvent e) {
        //ditched
    }

    public void OnPlayerDamaged(PlayerEvent e) {
        if(e.Id == NetworkID.Local_ID) {
            CharacterManager.character.CurrentHealth -= e.Damage;
        }
    }

    public void OnPlayerCast(PlayerEvent e) {
        GameObject go;
        if (players.TryGetValue(e.Id, out go)) {
            //go.GetComponent<AbilityCast>().onAbilityCast(e);
        }
    }

    public void OnPlayerDie(PlayerEvent e) {
        //ditched
    }

    public void OnPlayerEquipped(PlayerEvent e) {

    }

    public void OnPlayerJump(PlayerEvent e) {
        //dictched
    }

    public void OnPlayerMove(PlayerEvent e) {
        //ditched
    }

    public void OnPlayerPosition(PlayerEvent e) {
        GameObject go;
        if (players.TryGetValue(e.Id, out go)) {
            Debug.Log(go.ToString());
            go.GetComponent<NetworkIDController>().onReceiveMovement(e.Move);
        }
    }

    public void OnPlayerAnimation(PlayerEvent e) {
        GameObject go;
        if (players.TryGetValue(e.Id, out go)) {
            go.GetComponent<NetworkIDController>().onReceiveAnimation(e.Animation);
        }

    }


}