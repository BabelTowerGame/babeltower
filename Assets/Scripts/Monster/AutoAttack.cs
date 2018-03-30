using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour {
	private Transform player;
	private GameObject playerobj;
	private float attackDistance = 2.0f;
	private Animator animator;
	private float walkspeed = 0.1f;
	private float runspeed = 0.07f;
	private CharacterController cc;
	private float attackTime = 3;
	private float attackCounter;
    [SerializeField]
	private float awakeDistance = 20;
    [SerializeField]
    private float activeDistance = 50;
	private Vector3 oriPos;
    private bool hitstatus;
    private bool goback;

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
		//new_gravity ();
		if (playerobj == null) {
			playerobj = FindClosestPlayer ();
		}
		if (player != null) {
			player = playerobj.GetComponent<Transform> ();
			Vector3 targetPos = player.position;
			targetPos.y = transform.position.y;
			float distance = Vector3.Distance (targetPos, transform.position);
			float rangeDistance = Vector3.Distance (oriPos, transform.position);
			//Debug.Log (distance);

			//check if self_health value reached zero,go to defeated stat
			if(gameObject.GetComponent<Monster>().Current_health <= 0.0){
				Debug.Log ("Monster died");
				//animator.SetTrigger ("DieTrigger");
			}

			//if target player moved out the active range of monster. Go back to original poisition.
			if (rangeDistance >= activeDistance) {
                goback = true;

			}
            if (goback == true) {
                transform.LookAt(oriPos);
                cc.Move(transform.forward * walkspeed);
                gameObject.GetComponent<Monster>().InBattle = false;
                set_back(true);
                Debug.Log("Monster back to ori position");
                restart_patrol(false);
            }
			//at original position back to patrolling state.
			if (rangeDistance <= 5.0) {
                goback = false;
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Return to Ori")) {
					set_back (false);
					restart_patrol (true);
					//Debug.Log ("Monster restart patrol");
				}
			}

			//start attack while reached available distance
			if (distance <= attackDistance) {
				//Back from goto player to Attack
				animator.SetBool ("PlayerOutofRange", false);
				attackCounter += Time.deltaTime;
				//check target player's health value. If it is zero, go back to original position
				if (attackCounter > attackTime) {
					//set attack mode using random number
					//Debug.Log("Monster start attack player");
					start_attack();
					attackCounter = 0;
					animator.SetBool ("Waiting", false);
				} else {
					//TODO :just wait for next attack
					//Debug.Log("Monster wait for next attack");
					animator.SetBool("Waiting",true);
				}
			}
			else {
				//once reach target. Attack immediatelly
				attackCounter = attackTime;
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Patrolling")||animator.GetCurrentAnimatorStateInfo (0).IsName ("Go to player")) {
                    if (distance <= awakeDistance || hitstatus == true)
                    {
                        transform.LookAt(targetPos);
                        //Debug.Log("Monster goto Player");
                        pause_attack();
                        animator.SetTrigger("GoTrigger"); 
						cc.Move(transform.forward*runspeed);
                        gameObject.GetComponent<Monster>().InBattle = true;
                        gameObject.GetComponent<Monster>().InMovement = true;
                    }
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")||animator.GetCurrentAnimatorStateInfo (0).IsName ("STAND")) {
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
    void pause_attack() {
        animator.SetBool("Attack", false);
    }

	void attack(Transform Player){
		
	}
	void new_gravity(){
		Vector3 movement;
		movement = transform.position;
		float _vertSpeed = 0.0f;;
		if (!cc.isGrounded) {
            
			_vertSpeed += -9.8f * 5 * Time.deltaTime;
			if (_vertSpeed < -10.0f) { 
				_vertSpeed = -10.0f; 
				movement.y = _vertSpeed; 
				movement *= Time.deltaTime; 
				cc.Move (movement);
                //Debug.Log("Add gravity");
            } 
		} else {

		}
	}
	void applyDamage(float damage){
		float defense = gameObject.GetComponent<Monster> ().Defense;
		float real_damage = damage - defense;
		gameObject.GetComponent<Monster> ().Current_health -= real_damage;
	}
}
