using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint2TestGenerator : MonoBehaviour {

    [SerializeField]
    private Transform player;

	// Use this for initialization
	void Start () {
        Vector3 mobLocation1 = player.position;
        Vector3 mobLocation2 = player.position;
        Vector3 mobLocation3 = player.position;
        Vector3 mobLocation4 = player.position;
        mobLocation1.x += 50.0f;
        mobLocation1.z += 50.0f;
        mobLocation1.y = 150.0f;
        mobLocation2.x += 40.0f;
        mobLocation2.z += 40.0f;
        mobLocation2.y = 150.0f;
        mobLocation3.x += 40.0f;
        mobLocation3.z += 50.0f;
        mobLocation3.y = 150.0f;
        mobLocation4.x += 20.0f;
        mobLocation4.z += 50.0f;
        mobLocation4.y = 150.0f;

        M_generator gen = this.transform.GetComponent<M_generator>();

        GameObject monster1;
        GameObject monster2;
        GameObject monster3;
        GameObject monster4;
        monster1 = gen.GenerateMonster(M_generator.DemonType.Demon1, M_generator.DemonSkin.Demons1, 
                M_generator.WeaponType.Sword, mobLocation1);
        monster2 = gen.GenerateMonster(M_generator.DemonType.Demon2, M_generator.DemonSkin.Demons2,
                M_generator.WeaponType.Trident, mobLocation2);
        monster3 = gen.GenerateMonster(M_generator.DemonType.Demon3, M_generator.DemonSkin.Demons3,
                M_generator.WeaponType.Hammer, mobLocation3);
        monster4 = gen.GenerateMonster(M_generator.DemonType.Demon4, M_generator.DemonSkin.Demons4,
                M_generator.WeaponType.Pike, mobLocation4);

        monster1.GetComponent<Monster>().Current_health = 20.0f;
        monster2.GetComponent<Monster>().Current_health = 20.0f;
        monster3.GetComponent<Monster>().Current_health = 20.0f;
        monster4.GetComponent<Monster>().Current_health = 20.0f;



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
