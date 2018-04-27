using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class NetworkIDController : MonoBehaviour {
    //Note that for Character that are not local player
    //A Character Controller will be assigned
    //Animator will be handled serperately
    private CharacterController charController;
    private Animator charAnimator;

    private NetworkID networkID;

    private Vector3 prevPosition;
    private UnityEngine.Quaternion prevRotation;

    private int[] prevAnimationHash;
    private int[] prevAnimationTransitionHash;

    private Vector3 targetPosition;
    private UnityEngine.Quaternion targetRotation;
    private bool targetJump;
    private bool targetCrouch;

    private Vector3 fixedPosDiff;

    const float k_LocalMovementThreshold = 0.00001f;
    const float k_LocalRotationThreshold = 0.00001f;
    const float k_NetWorkTickRate = 0.1f;
    const float k_snapThreshold = 10.0f;
    const float k_interpolateMovement = 0.1f;
    const float k_interpolateRotation = 0.1f;


    private float lastClientSendTime;
    private float lastClientSyncTime;

    // Use this for initialization
    void Awake() {
	    charController = GetComponent<CharacterController>();
        charAnimator = GetComponent<Animator>();
        networkID = GetComponent<NetworkID>();
	    prevPosition = transform.position;
	    prevRotation = transform.rotation;
        prevAnimationHash = new int[charAnimator.layerCount];
        prevAnimationTransitionHash = new int[charAnimator.layerCount];

    }

    void Start() {
        //Debug.Log("-----------Send Enter Event");
        if (this.networkID.IsLocalPlayer) {

            Character c = CharacterManager.character;
            Tob.Event e = new Tob.Event();
            e.Topic = Tob.EventTopic.PlayerEvent;
            e.P = new Tob.PlayerEvent();
            e.P.Type = Tob.PlayerEventType.PlayerEnter;
            e.P.Id = NetworkID.Local_ID;
            e.P.Appearance = new Tob.PlayerAppearance();
            e.P.Appearance.Gender = (Tob.Gender)c.appearance.gender;
            e.P.Equiped = new Tob.PlayerEquiped();
            e.P.Equiped.Head = "5";
            e.P.Equiped.Chest = "3";
            e.P.Equiped.Weapon = "1";
            e.P.Equiped.Legs = "4";
            e.P.Equiped.Shield = "2";   
            e.P.Equiped.Shoes = "6";

            e.P.Position = new Tob.Vector();
            e.P.Position.X = transform.position.x;
            e.P.Position.Y = transform.position.y;
            e.P.Position.Z = transform.position.z;

            NetworkService.Instance.SendEvent(e);

            Debug.Log("-----------Send Enter Event");
        }
    }
	// Update is called once per frame
	void Update() {
        //networkTickrate enforcement
	    if (Time.time - lastClientSendTime < k_NetWorkTickRate) return;
	    if (networkID.IsLocalPlayer) {
	        SendTransform();
            SendAnimation();

	        lastClientSendTime = Time.time;
	    }
	}

    void FixedUpdate() {

        //FixedUpdate for local player are controller by character controller
        if (!networkID.IsLocalPlayer) {
            UpdateTransform();
        }
    }

    bool HasMoved() {
        float diff = 0;
        diff = (transform.position - prevPosition).magnitude;

        if (diff > k_LocalMovementThreshold) {
            return true;
        }

        diff = Quaternion.Angle(transform.rotation, prevRotation);

        if (diff > k_LocalRotationThreshold) {
            return true;
        }

        return false;
    }

    void SendTransform() {

        if (!HasMoved()) return;

        Tob.Event e = new Tob.Event();
        e.Topic = Tob.EventTopic.PlayerEvent;
        e.P = new Tob.PlayerEvent();
        e.P.Id = NetworkID.Local_ID;
        e.P.Type = Tob.PlayerEventType.PlayerPosition;
        e.P.Move = new Tob.PlayerMoveEvent();

        e.P.Move.Target = new Tob.Vector();
        e.P.Move.Target.X = transform.position.x;
        e.P.Move.Target.Y = transform.position.y;
        e.P.Move.Target.Z = transform.position.z;

        e.P.Move.Direction = new Tob.Quad();
        e.P.Move.Direction.A = transform.rotation.x;
        e.P.Move.Direction.B = transform.rotation.y;
        e.P.Move.Direction.C = transform.rotation.z;
        e.P.Move.Direction.D = transform.rotation.w;

        NetworkService.Instance.SendEvent(e);

        prevPosition = transform.position;
        prevRotation = transform.rotation;
    }

    void SendAnimation() {
        Tob.PlayerAnimationEvent pae = new Tob.PlayerAnimationEvent();
        int[] stateHash = new int[charAnimator.layerCount];
        float[] normalizedTime = new float[charAnimator.layerCount];

        //bool hasChanged = false;
        //for (int i = 0; i < charAnimator.layerCount; i++) {
        //    if (CheckAnimStateChanged(i, out stateHash[i], out normalizedTime[i])) {
        //        hasChanged = true;
        //        pae.StateHash.Add(stateHash[i]);
        //        pae.NormalizedTime.Add(normalizedTime[i]);
        //    } else {
        //        pae.StateHash.Add(prevAnimationHash[i]);
        //        pae.NormalizedTime.Add(normalizedTime[i]);
        //    }

        //if(!hasChanged) return;
        for (int i = 0; i < charAnimator.layerCount; i++) {
            if (charAnimator.IsInTransition(i)) {
                AnimatorStateInfo st = charAnimator.GetNextAnimatorStateInfo(i);
                pae.StateHash.Add(st.fullPathHash);
                pae.NormalizedTime.Add(st.normalizedTime);
            } else {
                AnimatorStateInfo st = charAnimator.GetCurrentAnimatorStateInfo(i);
                pae.StateHash.Add(st.fullPathHash);
                pae.NormalizedTime.Add(st.normalizedTime);
            }
        }


        for (int i = 0; i < charAnimator.parameters.Length; i++) {

            AnimatorControllerParameter par = charAnimator.parameters[i];
            if (par.type == AnimatorControllerParameterType.Int) {
                int value = charAnimator.GetInteger(par.nameHash);
                pae.IntParams.Add(value);
            }

            if (par.type == AnimatorControllerParameterType.Float) {
                float value = charAnimator.GetFloat(par.nameHash);
                pae.FloatParams.Add(value);
            }

            if (par.type == AnimatorControllerParameterType.Bool) {
                bool value = charAnimator.GetBool(par.nameHash);
                pae.BoolParams.Add(value);
            }
        }
        Tob.Event e = new Tob.Event();
        Tob.PlayerEvent pe = new Tob.PlayerEvent();
        pe.Type = Tob.PlayerEventType.PlayerAnimation;
        e.Topic = Tob.EventTopic.PlayerEvent;
        e.P = pe;
        e.P.Id = NetworkID.Local_ID;
        pe.Animation = pae;

        NetworkService.Instance.SendEvent(e);

    }


    bool CheckAnimStateChanged(int layer,out int stateHash, out float normalizedTime) {
        stateHash = 0;
        normalizedTime = 0;

        if (charAnimator.IsInTransition(layer)) {
            AnimatorTransitionInfo tt = charAnimator.GetAnimatorTransitionInfo(layer);
            if (tt.fullPathHash != prevAnimationTransitionHash[layer]) {
                // first time in this transition
                prevAnimationTransitionHash[layer] = tt.fullPathHash;
                prevAnimationHash[layer] = 0;
                return true;
            }
            return false;
        }

        AnimatorStateInfo st = charAnimator.GetCurrentAnimatorStateInfo(layer);
        if (st.fullPathHash != prevAnimationHash[layer]) {
            // first time in this animation state
            if (prevAnimationHash[layer] != 0) {
                // came from another animation directly - from Play()
                stateHash = st.fullPathHash;
                normalizedTime = st.normalizedTime;
            }
            prevAnimationTransitionHash[layer] = 0;
            prevAnimationHash[layer] = st.fullPathHash;
            return true;
        }
        return false;
    }

    void UpdateTransform() {

        if (fixedPosDiff == Vector3.zero && targetRotation == transform.rotation)
            return;
        charController.Move(fixedPosDiff * k_interpolateMovement);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.fixedDeltaTime * k_interpolateRotation * 10);


        // if network lag happens the transform will snap into place immeditately
        if (Time.time - lastClientSyncTime > k_NetWorkTickRate) {
            // turn off interpolation if we go out of the time window for a new packet
            fixedPosDiff = Vector3.zero;

            var diff = targetPosition - transform.position + Physics.gravity;
            
            charController.Move(diff);
        }

    }

    public void onReceiveMovement(Tob.PlayerMoveEvent e) {

        //Debug.Log(this.ToString() + "onReceiveMovement" + e.ToString());
        //Save Network Message in a Buffer for next Fixed update
        if (networkID.IsLocalPlayer) {
            //LocalPlayer does not need to be updated by network
            return;
        }


        targetPosition = new Vector3(e.Target.X, e.Target.Y, e.Target.Z);
        targetRotation = new Quaternion(e.Direction.A, e.Direction.B, e.Direction.C, e.Direction.D);

        var totalDistToTarget = (targetPosition - transform.position); 
        var perSecondDist = totalDistToTarget / k_NetWorkTickRate;
        fixedPosDiff = perSecondDist * Time.fixedDeltaTime;

        float dist = (transform.position - targetPosition).magnitude;
        if (dist > k_snapThreshold) {
            transform.position = targetPosition;
        }
        lastClientSyncTime = Time.time;
    }

    //TODO:parameter message
    public void onReceiveAnimation(Tob.PlayerAnimationEvent e) {
        //Animator will be updated in realtime once the message 
        //is receive since no sync issue is involved
        Debug.Log(this.ToString() + "onReceiveAnimation" + e.ToString());


        if (networkID.IsLocalPlayer)
            return;

        int layerCount = this.charAnimator.layerCount;

        for(int i = 0; i < layerCount; i++) {
            charAnimator.Play(e.StateHash[i], i, e.NormalizedTime[i]);
        }

        int a = 0, b = 0, c = 0;


        for (int i = 0; i < charAnimator.parameters.Length; i++) {

            AnimatorControllerParameter par = charAnimator.parameters[i];
            if (par.type == AnimatorControllerParameterType.Int) {
                int newValue = e.IntParams[a++];
                charAnimator.SetInteger(par.nameHash, newValue);
            }

            if (par.type == AnimatorControllerParameterType.Float) {
                float newFloatValue = e.FloatParams[b++];
                charAnimator.SetFloat(par.nameHash, newFloatValue);
            }

            if (par.type == AnimatorControllerParameterType.Bool) {
                bool newBoolValue = e.BoolParams[c++];
                charAnimator.SetBool(par.nameHash, newBoolValue);
            }
        }

    }
}
