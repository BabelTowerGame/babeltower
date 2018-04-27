using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tob;

public class NetworkPlayerManager : MonoBehaviour {

    public GameObject MalePrefab;
    public GameObject FemalePrefab;

    public Dictionary<string, GameObject> players;
    public Dictionary<string, PlayerAppearance> playerAppearence;

    public void Awake() {
        players = new Dictionary<string, GameObject>();
        playerAppearence = new Dictionary<string, PlayerAppearance>();
    }

    //TODO: parse msg to player class
    public void OnPlayerEnter(PlayerEvent e) {

        GameObject obj;

        if (players.TryGetValue(e.Id,out obj)) return;
        
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


        if(NetworkService.isServer) {
            Dictionary<string, GameObject>.Enumerator eu = players.GetEnumerator();
            foreach (var pair in players) {
               // var pair = eu.Current;
                Tob.Event ee = new Tob.Event();
                ee.Topic = EventTopic.PlayerEvent;
                ee.P = new Tob.PlayerEvent();
                ee.P.Id = pair.Key;
                Debug.Log("Send ID: "+ee.P.Id);
                ee.P.Type = PlayerEventType.PlayerEnter;
                PlayerAppearance ap = playerAppearence[pair.Key];
                Debug.Log(ap);
                ee.P.Appearance = new Tob.PlayerAppearance();
                ee.P.Appearance.Gender = ap.Gender;
                ee.P.Appearance.HairColor = ap.HairColor;

                ee.P.Equiped = new Tob.PlayerEquiped();
                ee.P.Equiped.Head = "5";
                ee.P.Equiped.Chest = "3";
                ee.P.Equiped.Weapon = "1";
                ee.P.Equiped.Legs = "4";
                ee.P.Equiped.Shield = "2";
                ee.P.Equiped.Shoes = "6";

                ee.P.Position = new Vector();
                ee.P.Position.X = pair.Value.transform.position.x;
                ee.P.Position.Y = pair.Value.transform.position.y;
                ee.P.Position.Z = pair.Value.transform.position.z;

                Debug.Log("                                     "+ee.ToString());

                NetworkService.Instance.SendEvent(ee);
            }
            M_generator mg = GameObject.FindGameObjectWithTag("NetworkMonster").GetComponent<M_generator>();
            for (int i = 0; i < mg.monsterList.Length; i++) {
                Vector3 gencoord = new Vector3(mg.monsterList[i].GetComponent<Monster>().X, mg.monsterList[i].GetComponent<Monster>().Y, mg.monsterList[i].GetComponent<Monster>().Z);
                Tob.Event ee = new Tob.Event();
                ee.Topic = EventTopic.MonsterEvent;
                MonsterEvent e0 = new MonsterEvent();
                MonsterSpawnEvent e1 = new MonsterSpawnEvent();
                e1.Id = i.ToString();
                Tob.Vector passpos = new Tob.Vector();
                passpos.X = gencoord.x;
                passpos.Y = gencoord.y;
                passpos.Z = gencoord.z;
                e1.Position = passpos;
                e1.DemonSkin = (int)mg.monsterList[i].GetComponent<Monster>().Skin;
                e1.DemonType = (int)mg.monsterList[i].GetComponent<Monster>().Type;
                e1.WeaponType = (int)mg.monsterList[i].GetComponent<Monster>().Weapon;
                e0.Spawn = e1;
                ee.M = e0;
                NetworkService.Instance. SendEvent(ee);
            }
        }

        players.Add(e.Id, go);
        playerAppearence.Add(e.Id, t);


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
