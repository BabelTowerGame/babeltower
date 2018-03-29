using System.Collections;
using UnityEngine;

public class BulletShotTriggerable : MonoBehaviour {

	[HideInInspector]public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public void Throw() {
		var bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 6;

		Destroy (bullet, 2.0f);
	}
}
