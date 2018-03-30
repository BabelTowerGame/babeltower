using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint2TestGenerator : MonoBehaviour {

    [SerializeField]
    private Transform player;

	// Use this for initialization
	void Start () {
        Vector3 mobLocation = player.position;
        mobLocation.x += 10.0f;
        mobLocation.z += 10.0f;
        mobLocation.y = 133.0f;

        M_generator gen = this.transform.GetComponent<M_generator>();

        GameObject monster1;
        monster1 = gen.GenerateMonster(M_generator.DemonType.Demon1, M_generator.DemonSkin.Demons1, 
                M_generator.WeaponType.Sword, mobLocation, new Vector3(0,180,0));

        monster1.GetComponent<Monster>().Current_health = 20.0f;


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
