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
    private Quaternion prevRotation;


    private int[] prevAnimationHash;
    private int[] prevAnimationTransitionHash;



    private Vector3 targetPosition;
    private Quaternion targetRotation;
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
	
	// Update is called once per frame
	void Update() {
        //networkTickrate enforcement
	    if (Time.time - lastClientSendTime < k_NetWorkTickRate) return;
	    if (networkID.IsLocalPlayer) {
	        SendTransform();

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
        //TODO: Build Transform message and send to server

        //Write position message

        //Write rotation message

        prevPosition = transform.position;
        prevRotation = transform.rotation;
    }

    void SendAnimation() {
        int[] stateHash = new int[charAnimator.layerCount];
        float[] normalizedTime = new float[charAnimator.layerCount];
        bool hasChanged = false;
        for (int i = 0; i < charAnimator.layerCount; i++) {
            if (CheckAnimStateChanged(i, out stateHash[i], out normalizedTime[i])) {
                hasChanged = true;
            }
        }
        if(!hasChanged) return;
        //TODO: Build network message using Hash and Time array above
        //TODO: Send msg to network
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

            var diff = targetPosition - transform.position;
            charController.Move(diff);
        }

    }

    //TODO:parameter message
    public void onReceiveMovement() {
        //Save Network Message in a Buffer for next Fixed update
        if (networkID.IsLocalPlayer) {
            //LocalPlayer does not need to be updated by network
            return;
        }

        //TODO:parse network message into targetPosition and targetRotation
        //TODO:parse network message into jum and crouch as well

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
    public void onReceiveAnimation() {
        //Animator will be updated in realtime once the message 
        //is receive since no sync issue is involved

        if (networkID.IsLocalPlayer)
            return;

        //TODO parse messageto stateHash and normalizedTime
        //Since we have only two layers this will be good enough
        //charAnimator.Play(msg.layer[0].stateHash, 0, msg.layer[0].normalizedTime);
        //charAnimator.Play(msg.layer[1].stateHash, 1, msg.layer[1].normalizedTime);

        for (int i = 0; i < charAnimator.parameters.Length; i++) {

            AnimatorControllerParameter par = charAnimator.parameters[i];
            if (par.type == AnimatorControllerParameterType.Int) {
                int newValue = 0; //TODO: read from message
                charAnimator.SetInteger(par.nameHash, newValue);
            }

            if (par.type == AnimatorControllerParameterType.Float) {
                float newFloatValue = 0; //TODO: read from message
                charAnimator.SetFloat(par.nameHash, newFloatValue);
            }

            if (par.type == AnimatorControllerParameterType.Bool) {
                bool newBoolValue = false; //TODO: read from message
                charAnimator.SetBool(par.nameHash, newBoolValue);
            }
        }

    }
}
