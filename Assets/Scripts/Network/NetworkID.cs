using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkID : MonoBehaviour {
    public static string Local_ID;
    public string ID;
    public bool IsLocalPlayer;


    public static string generateNewID(int length) {
        string result = "";
        for(int i = 0; i < length; i ++) {
            result += (char)Random.Range(33, 126);
        }
        return result;
    }

}
