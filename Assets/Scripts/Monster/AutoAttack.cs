using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour {
	private Transform player;
	private GameObject playerobj;

	//all constant value should be change in real gen
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
	private bool Die;

	// Use this for initialization
	void Start () {
		player = null;
		playerobj = FindClosestPlayer ();
		if (playerobj != null) {
			player = playerobj.GetComponent<Transform> ();
		}
		cc = gameObject.GetComponent<CharacterController> ();
		animator = this.GetComponent<Animator> ();

		ItemDB DB = ItemDB.Instance;
		int dblength = ItemDB.Instance.items.Length;
		int[] Loottable = new int[dblength];
		for (int i = 0; i < dblength; i++) {
			Loottable [i] = DB.get (i).ID;
		}
		this.GetComponent<Monster> ().LootTable = Loottable;

		attackCounter = attackTime;
        grounded = false;
		Die = false;

	}
	
	// Update is called once per frame
	void Update () {
        new_gravity ();
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
		if (player != null && Die != true) {
			player = playerobj.GetComponent<Transform> ();
			Vector3 targetPos = player.position;
			targetPos.y = transform.position.y;
			float distance = Vector3.Distance (targetPos, transform.position);
			float rangeDistance = Vector3.Distance (oriPos, transform.position);

			if(gameObject.GetComponent<Monster>().Current_health <= 0.0){
				//Debug.Log ("Monster died");
				animator.SetTrigger ("DieTrigger");
				LootlistGen();
				Die = true;
			}

			/*if (Input.GetKeyDown (KeyCode.L))  
			{  
				manual_attack ();	
			}  
			if (Input.GetKeyDown (KeyCode.T))  
			{  
				manual_move ();
			} */ 
			//if target player moved out the active range of monster. Go back to original poisition.
			if (rangeDistance >= activeDistance && gameObject.GetComponent<Monster>().InBattle == true) {
                goback = true;
                gameObject.GetComponent<Monster>().InBattle = false;
            }
			/*if (player.gameObject.GetComponent<Character>().currentHealth <= 0.0f) {
				goback = true;
				gameObject.GetComponent<Monster>().InBattle = false;
			}*/
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
	public bool LootReady{
		get { return this.Die; }
		set {this.Die = value; }
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
	public int[] lootableItem(){
		if (LootReady) {
			return this.GetComponent<Monster> ().LootList;
		}
		return null;
			
	}
	public int lootItem(int index){
		if (LootReady) {
			int[] lootlist = gameObject.GetComponent<Monster> ().LootList;
			if (index >= lootlist.Length) {
				return -1;
			} else {
				int result = lootlist [index];
				lootlist [index] = -1;
				gameObject.GetComponent<Monster> ().LootList = lootlist;
				return result;
			}
		}
		return -1;
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

	void new_gravity(){
		Vector3 movement = Vector3.zero;
		//movement = transform.position;
		if (!cc.isGrounded) {
            movement += Physics.gravity; ; 
		    cc.Move (movement * Time.deltaTime);
                //Debug.Log("Add gravity");
        } 
	}
	public void applyDamage(float damage,Transform player){
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Patrolling")) {
			this.player = player;
		} else {
			if (this.GetComponent<Monster> ().InBattle == false) {
				// no damage can be cause to monster while it is not in battle	
				return;
			}
		}
		float defense = gameObject.GetComponent<Monster> ().Defense;
		float real_damage = damage - defense;
		gameObject.GetComponent<Monster> ().Current_health -= real_damage;
	}

	void dealthDmage(){
		if (player != null) {
			player.GetComponent<Character> ().currentHealth -= this.GetComponent<Monster> ().Damage;

		}
	}



	//manual control part for network transimission
	public void manual_move(Vector3 dest){
		transform.LookAt (dest);
		animator.SetTrigger ("M_Go");
		float distance = Vector3.Distance (dest, transform.position);
		while (distance > 1.0f) {
			cc.SimpleMove (transform.forward * walkspeed);
			distance = Vector3.Distance (dest, transform.position);
		}

	}
	public void manual_attack(Vector3 target){
		transform.LookAt (target);
		animator.SetTrigger ("M_Attack");
	}
	public void manual_die(){
		animator.SetTrigger ("M_Die");
	}
	public void manual_patrol(){
		animator.SetTrigger("M_Patrol");
	}

	void LootlistGen(){
		int iUp=10; 
		int iDown=1;
		int result = Random.Range (iDown, iUp);
		int[] lootlist = new int[result];
		for (int i = 0; i < result; i++) {
			int[] temptable = this.GetComponent<Monster> ().LootTable;
			lootlist [i] = temptable [Random.Range (0, temptable.Length)];
		}
		this.GetComponent<Monster> ().LootList = lootlist;
	}
}
