using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tob;

public class AutoAttack : MonoBehaviour {
	private Transform player;
	private GameObject playerobj;
	private NetworkService NS = NetworkService.Instance;
	//all constant value should be change in real gen
	private float attackDistance = 2.0f;
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
    private bool hitted;
	private bool Die;
	public  Vector3 updatePos;

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
		int dblength = ItemDB.Instance.items.Count;
		int[] Loottable = new int[dblength];
		for (int i = 0; i < dblength; i++) {
			Item tempItem = DB.get (i);
			//Debug.Log (tempItem);
			//Loottable [i] = tempItem.ID;
		}
		this.GetComponent<Monster> ().LootTable = Loottable;

		attackCounter = attackTime;
        grounded = false;
		Die = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (NetworkService.isServer) {
			//server
			//
			new_gravity ();
			if (playerobj == null||animator.GetCurrentAnimatorStateInfo (0).IsName ("Patrolling")) {
				Debug.Log ("I am patrolling");
				oriPos = transform.position;
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
			if (Input.GetKeyDown(KeyCode.Q))
			{
				applyDamage(20.0f, player);
			}

			if (player != null) {
				player = playerobj.GetComponent<Transform> ();
				Vector3 targetPos = player.position;
				targetPos.y = transform.position.y;
				float distance = Vector3.Distance (targetPos, transform.position);
				float rangeDistance = Vector3.Distance (oriPos, transform.position);
				//Debug.Log ("Player is not null");


				if(gameObject.GetComponent<Monster>().Current_health <= 0.0 && Die == false){
					Debug.Log ("Monster died");
					animator.SetTrigger ("DieTrigger");
					this.GetComponent<Monster> ().InBattle = false;
					this.GetComponent<Monster> ().InMovement = false;
					hitted = false;
					this.GetComponent<Monster> ().X = this.transform.position.x;
					this.GetComponent<Monster> ().Y = this.transform.position.y;
					this.GetComponent<Monster> ().Z = this.transform.position.z;
					MonsterEvent e = new MonsterDieEvent();
					e.Id = this.GetComponent<Monster>().ID.ToString();
					LootlistGen();
					Die = true;
					NS.SendMessage("OnMonsterEvent",e);
				}
				//if target player moved out the active range of monster. Go back to original poisition.
				if (rangeDistance >= activeDistance && gameObject.GetComponent<Monster>().InBattle == true) {
					goback = true;
					gameObject.GetComponent<Monster>().InBattle = false;
				}
				if (goback == true) {
					oriPos.y = transform.position.y;
					transform.LookAt(oriPos);
					this.GetComponent<Monster>().Current_health = this.GetComponent<Monster>().Health;
					cc.SimpleMove(transform.forward * walkspeed);
					this.GetComponent<Monster> ().X = this.transform.position.x;
					this.GetComponent<Monster> ().Y = this.transform.position.y;
					this.GetComponent<Monster> ().Z = this.transform.position.z;
					set_back(true);
					hitted =  false;
					gameObject.GetComponent<Monster>().InBattle = false;
					MonsterEvent e = new MonsterDieEvent();
					e.Id = this.GetComponent<Monster>().ID.ToString();


					//Debug.Log("Monster back to ori position");
				}
				//at original position back to patrolling state.
				if (rangeDistance <= 5.0f && animator.GetCurrentAnimatorStateInfo(0).IsName("GoBack")) {
					if (goback == true) {
						set_back (false);
						animator.SetTrigger("BackPatrol");
						transform.LookAt(Vector3.zero);
						//player = null;
						Debug.Log ("Monster restart patrol");
						animator.SetBool("SawPlayer", false);
						goback = false;
						this.GetComponent<Monster> ().InBattle = false;
						this.GetComponent<Monster> ().InMovement = false;
						this.GetComponent<Monster> ().X = this.transform.position.x;
						this.GetComponent<Monster> ().Y = this.transform.position.y;
						this.GetComponent<Monster> ().Z = this.transform.position.z;
					}
				}

				if (distance <= attackDistance) {
					this.GetComponent<Monster> ().X = this.transform.position.x;
					this.GetComponent<Monster> ().Y = this.transform.position.y;
					this.GetComponent<Monster> ().Z = this.transform.position.z;
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
							this.GetComponent<Monster> ().X = this.transform.position.x;
							this.GetComponent<Monster> ().Y = this.transform.position.y;
							this.GetComponent<Monster> ().Z = this.transform.position.z;
							gameObject.GetComponent<Monster>().InBattle = true;
							gameObject.GetComponent<Monster>().InMovement = true;
						}
						if (hitted == true && rangeDistance < activeDistance) {
							//Debug.Log("I am here");
							transform.LookAt(targetPos);
							pause_attack();
							animator.SetBool("SawPlayer",true); 
							cc.SimpleMove(transform.forward*runspeed);
							this.GetComponent<Monster> ().X = this.transform.position.x;
							this.GetComponent<Monster> ().Y = this.transform.position.y;
							this.GetComponent<Monster> ().Z = this.transform.position.z;
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
						this.GetComponent<Monster> ().X = this.transform.position.x;
						this.GetComponent<Monster> ().Y = this.transform.position.y;
						this.GetComponent<Monster> ().Z = this.transform.position.z;
						gameObject.GetComponent<Monster>().InBattle = true;
						gameObject.GetComponent<Monster>().InMovement = true;
						animator.SetBool ("PlayerOutofRange", true);
					}
				}
			}
		} else {
			//client
			new_gravity ();
			Vector3 dPos = new Vector3 (this.GetComponent<Monster> ().X, this.GetComponent<Monster> ().Y, this.GetComponent<Monster> ().Z);
			if(this.GetComponent<Monster>().Updated == true){
				this.transform.position = dPos;
				this.GetComponent<Monster> ().Updated = false;

			}
			/*if (this.GetComponent<Monster> ().Current_health <= 0.0f) {
				manual_die();
			}*/
			else if (this.GetComponent<Monster> ().InMovement == false && this.GetComponent<Monster> ().InBattle == false) {
				manual_patrol();
			}


			Vector3 dTargetPos = new Vector3 (this.GetComponent<Monster> ().PX, this.GetComponent<Monster> ().PY, this.GetComponent<Monster> ().PZ);
			Vector3 diff = dTargetPos - this.transform.position;
			float curDistance = diff.sqrMagnitude;
			this.transform.LookAt (dTargetPos);
			animator.SetTrigger ("M_Go");
			cc.SimpleMove (transform.forward * runspeed);


		}
       
	}



	GameObject FindClosestPlayer(){
		//Debug.Log ("Finding player");
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
		//Debug.Log (closest.transform.position);
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

	public void new_gravity(){
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
			hitted = true;
            //Debug.Log("Hitted by player");
		} else {
			if (this.GetComponent<Monster> ().InBattle == false) {
                // no damage can be cause to monster while it is not in battle	
                //Debug.Log(animator.GetCurrentAnimatorStateInfo(0));
                return;
            }
		}
        float defense = gameObject.GetComponent<Monster>().Defense;
        float real_damage = damage - defense;
        gameObject.GetComponent<Monster>().Current_health -= real_damage;

    }

	public void dealthDmage(){
		if (player != null) {
			//TODO:


			//Should be an event
			player.GetComponent<Character> ().CurrentHealth -= (this.GetComponent<Monster> ().Damage-player.GetComponent<Character> ().Defense);

		}
	}



	//manual control part for network transimission
	public void manual_move(Vector3 dest,bool toplayer){
		transform.LookAt (dest);
		animator.SetTrigger ("M_Go");
		if (toplayer == true) {
			this.GetComponent<Monster> ().InBattle = true;
		} else {
			this.GetComponent<Monster> ().InBattle = false;
		}
		this.GetComponent<Monster> ().InMovement = true;
		float distance = Vector3.Distance (dest, transform.position);
		while (distance > 1.0f) {
			cc.SimpleMove (transform.forward * walkspeed);
			this.GetComponent<Monster> ().X = this.transform.position.x;
			this.GetComponent<Monster> ().Y = this.transform.position.y;
			this.GetComponent<Monster> ().Z = this.transform.position.z;
			distance = Vector3.Distance (dest, transform.position);
		}

	}
	public void manual_attack(){
		//transform.LookAt (target);
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
		//Debug.Log (result);
		int[] lootlist = new int[result];
		for (int i = 0; i < result; i++) {
			int[] temptable = this.GetComponent<Monster> ().LootTable;
			lootlist [i] = temptable [Random.Range (0, temptable.Length)];
		}
		this.GetComponent<Monster> ().LootList = lootlist;
	}
}
