using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour {
	private Transform player;
	private GameObject playerobj;
	private float attackDistance = 1.0f;
	private Animator animator;
	private float walkspeed = 3.0f;
	private float runspeed = 3.0f;
	private CharacterController cc;
	private float attackTime = 3;
	private float attackCounter;
	private float awakeDistance = 10;
    private float activeDistance = 30;
	private Vector3 oriPos;
    private bool hitstatus = false;
    private bool goback;
    private bool grounded;
    private bool testval;

	// Use this for initialization
	void Start () {
		player = null;
		playerobj = FindClosestPlayer ();
		if (playerobj != null) {
			player = playerobj.GetComponent<Transform> ();
		}
		cc = gameObject.GetComponent<CharacterController> ();
		animator = this.GetComponent<Animator> ();
		attackCounter = attackTime;
        grounded = false;

	}
	
	// Update is called once per frame
	void Update () {
        new_gravity ();
        //Debug.Log(cc.isGrounded ? "GROUNDED" : "NOT GROUNDED");
        if (playerobj == null) {
			playerobj = FindClosestPlayer ();
		}
        if (grounded == false) {
            //Debug.Log("Gounding");
            if (cc.isGrounded == true) {
                oriPos = transform.position;
                Debug.Log(oriPos);
            }
            grounded = true;
        }
		if (player != null) {
			player = playerobj.GetComponent<Transform> ();
			Vector3 targetPos = player.position;
			targetPos.y = transform.position.y;
			float distance = Vector3.Distance (targetPos, transform.position);
			float rangeDistance = Vector3.Distance (oriPos, transform.position);

			if(gameObject.GetComponent<Monster>().Current_health <= 0.0){
				//Debug.Log ("Monster died");
				animator.SetTrigger ("DieTrigger");
			}

			//if target player moved out the active range of monster. Go back to original poisition.
			if (rangeDistance >= activeDistance && gameObject.GetComponent<Monster>().InBattle == true) {
                goback = true;
                gameObject.GetComponent<Monster>().InBattle = false;
            }
            if (goback == true) {
                oriPos.y = transform.position.y;
                transform.LookAt(oriPos);
                cc.SimpleMove(transform.forward * walkspeed);
                set_back(true);
                //Debug.Log("Monster back to ori position");
            }
			//at original position back to patrolling state.
			if (rangeDistance <= 5.0f) {
				if (goback == true) {
					set_back (false);
					animator.SetTrigger("BackPatrol");
                    transform.LookAt(Vector3.zero);
                    //Debug.Log ("Monster restart patrol");
                    animator.SetBool("SawPlayer", false);
                    testval = true;
                    goback = false;
                }
			}

			if (distance <= attackDistance) {
				animator.SetBool ("PlayerOutofRange", false);
				attackCounter += Time.deltaTime;
				//TODO check target player's health value. If it is zero, go back to original position
				if (attackCounter > attackTime) {
                    //Debug.Log("Start attack");
					start_attack();
					attackCounter = 0;
					//animator.SetBool ("Waiting", false);
				}
			}
			else {
				attackCounter = attackTime;

				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Patrolling")||animator.GetCurrentAnimatorStateInfo (0).IsName ("Go to player")) {
                    if (distance < awakeDistance && rangeDistance < activeDistance)
                    {
                        //Debug.Log("Monster go to player");
                        transform.LookAt(targetPos);
                        pause_attack();
                        animator.SetBool("SawPlayer",true); 
						cc.SimpleMove(transform.forward*runspeed);
                        gameObject.GetComponent<Monster>().InBattle = true;
                        gameObject.GetComponent<Monster>().InMovement = true;
                    }
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack1")
                    ||animator.GetCurrentAnimatorStateInfo (0).IsName ("STAND")
                    ||animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) {
                    //Debug.Log("Persue player");
                    cc.SimpleMove (transform.forward * runspeed);
					animator.SetBool ("PlayerOutofRange", true);
				}
			}
		}
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
    void pause_attack() {
        animator.SetBool("Attack", false);
    }

	void attack(Transform Player){
		
	}
	void new_gravity(){
		Vector3 movement = Vector3.zero;
		//movement = transform.position;
		if (!cc.isGrounded) {
            movement += Physics.gravity; ; 
		    cc.Move (movement * Time.deltaTime);
                //Debug.Log("Add gravity");
        } 
	}

}
