using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class NetworkIDController : MonoBehaviour {

    private ThirdPersonCharacter charController;
    private NetworkID networkID;
    private Vector3 prevPosition;
    private Quaternion prevRotation;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool targetJump;
    private bool targetCrouch;

    private Vector3 fixedPosDiff;


    const float k_LocalMovementThreshold = 0.00001f;
    const float k_LocalRotationThreshold = 0.00001f;
    const float k_NetWorkTickRate = 0.1f;
    const float k_snapThreshold = 0.1f;
    const float k_interpolateMovement = 0.1f;
    const float k_interpolateRotation = 0.1f;


    private float lastClientSendTime;
    private float lastClientSyncTime;

    // Use this for initialization
    void Awake() {
	    charController = GetComponent<ThirdPersonCharacter>();
        networkID = GetComponent<NetworkID>();
	    prevPosition = transform.position;
	    prevRotation = transform.rotation;

	}
	
	// Update is called once per frame
	void Update() {
	    if (networkID.IsLocalPlayer) {
	        SendTransform();

            //TODO: May need a way to enforce server tickrate OR Do we really need a tickrate for client?
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

    void UpdateTransform() {

        if (fixedPosDiff == Vector3.zero && targetRotation == transform.rotation)
            return;
        charController.Move(fixedPosDiff * k_interpolateMovement, targetCrouch, targetJump, false);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.fixedDeltaTime * k_interpolateRotation * 10);


        // if network lag happens the transform will snap into place immeditately
        if (Time.time - lastClientSyncTime > k_NetWorkTickRate) {
            // turn off interpolation if we go out of the time window for a new packet
            fixedPosDiff = Vector3.zero;

            var diff = targetPosition - transform.position;
            charController.Move(diff,false,false,false);
        }




    }

    public void onReceiveMessage() {
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
}
