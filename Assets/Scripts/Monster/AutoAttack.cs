using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour {
	private Transform player;
	private GameObject playerobj;
	private float attackDistance;
	private Animator animator;
	private float walkspeed;
	private float runspeed;
	private CharacterController cc;
	private float attackTime;
	private float attackCounter;
	private float awakeDistance;
	private float activeDistance;
	private Vector3 oriPos;
    private bool hitstatus;

	// Use this for initialization
	void Start () {
		player = null;
		playerobj = FindClosestPlayer ();
		if (playerobj != null) {
			player = playerobj.GetComponent<Transform> ();
		}
		cc = gameObject.GetComponent<CharacterController> ();
		animator = this.GetComponent<Animator> ();
		//once reach target. Attack immediatelly
		attackCounter = attackTime;

	}
	
	// Update is called once per frame
	void Update () {
		if (playerobj == null) {
			playerobj = FindClosestPlayer ();
		}
		if (player != null) {
			player = playerobj.GetComponent<Transform> ();
			Vector3 targetPos = player.position;
			targetPos.y = transform.position.y;
			//transform.LookAt (targetPos);
			float distance = Vector3.Distance (targetPos, transform.position);
            Debug.Log(distance);
			float rangeDistance = Vector3.Distance (oriPos, transform.position);

			//check if self_health value reached zero,go to defeated state
			//TODO: check self_current health
			if(gameObject.GetComponent<Monster>().Current_health <= 0.0){
				animator.SetTrigger ("DieTrigger");
			}

			//if target player moved out the active range of monster. Go back to original poisition.
			if (rangeDistance >= activeDistance) {
				transform.LookAt (oriPos);
				cc.SimpleMove (transform.forward * walkspeed);
                gameObject.GetComponent<Monster>().InBattle = false;
                set_back (true);
				restart_patrol (false);
			}
			//at original position back to patrolling state.
			if (rangeDistance == 0.0) {
				set_back (false);
				restart_patrol (true);
			}

			//start attack while reached available distance
			if (distance <= attackDistance) {
				//Back from goto player to Attack
				animator.SetBool ("PlayerOutofRange", false);
				attackCounter += Time.deltaTime;
				//check target player's health value. If it is zero, go back to original position
				if (attackCounter > attackTime) {
					//set attack mode using random number
					start_attack();
					attackCounter = 0;
				} else {
					//TODO :just wait for next attack
					animator.SetBool("Waiting",true);
				}
			}
			else {
				//once reach target. Attack immediatelly
				attackCounter = attackTime;
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Patrolling")) {
                    if (distance <= awakeDistance || hitstatus == true)
                    {
                        transform.LookAt(targetPos);
                        animator.SetTrigger("GoTrigger");
                        cc.SimpleMove(transform.forward * runspeed);
                        gameObject.GetComponent<Monster>().InBattle = true;
                        gameObject.GetComponent<Monster>().InMovement = true;
                    }
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
					cc.SimpleMove (transform.forward * runspeed);
					animator.SetBool ("PlayerOutofRange", true);
				}

			}
			
		}
		//set Hitted animation
		/*if (animator.GetBool ("Hitted")) {
			reset_hit ();
		}*/
	}



	GameObject FindClosestPlayer(){
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject target in players)
		{
			Vector3 diff = target.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = target;
				distance = curDistance;
			}
		}
		return closest;
	}
	public void initAttack(){
		//initiate attacking parameter
		
	}
	public float AttackDistance{
		get { return this.attackDistance; }
		set { this.attackDistance = value; }
	}

	public float WalkingSpeed{
		get { return this.walkspeed; }
		set { this.walkspeed = value; }
	}
	public float RuningSpeed{
		get { return this.runspeed; }
		set { this.runspeed = value; }
	}
	public float ActiveDistance{
		get { return this.activeDistance; }
		set { this.activeDistance = value; }
	}
	public float AwakeDistance{
		get { return this.awakeDistance; }
		set { this.awakeDistance = value; }
	}
	public Vector3 OriPosition{
		get { return this.oriPos; }
		set { this.oriPos = value; }
	}
	public float AttackTime{
		get { return this.attackTime; }
		set { this.attackTime = value; }
	}
	void set_back(bool flag){
		animator.SetBool("OutRange", flag);
	}
	void restart_patrol(bool flag){
		animator.SetBool ("AtOri", flag);
	}
	public void hit(Transform player){
        if (gameObject.GetComponent<Monster>().InBattle == false) {
            this.player = player;
            this.hitstatus = true;
        }
	}
	void reset_hit(){
		animator.SetBool ("Hitted", false);
	}
	void start_attack(){
		animator.SetBool ("Attack", true);
	}
}
