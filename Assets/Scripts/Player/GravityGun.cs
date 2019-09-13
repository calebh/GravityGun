using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour {
    public GameObject Ball;
    private Score GameScore;
    private Rigidbody BallRigidBody;
    public float MaxGrabDistance = 5.0f;
    public float MaxGrabIntensity = 10.0f;
    public float GrabVelocityDecay = 0.9f;
    public float FireIntensity = 500.0f;

    private bool HoldingBall = false;

    public void Awake() {
        BallRigidBody = Ball.GetComponent<Rigidbody>();
        GameScore = Ball.GetComponent<Score>();
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0) && !HoldingBall &&
            (Ball.transform.position - transform.position).magnitude <= MaxGrabDistance &&
            !GameScore.InResetPeriod) {

            HoldingBall = true;
            BallRigidBody.useGravity = false;
        } else if (Input.GetMouseButtonDown(0) && HoldingBall) {
            HoldingBall = false;
            BallRigidBody.useGravity = true;
            BallRigidBody.AddForce(transform.forward * FireIntensity, ForceMode.Impulse);
        }

        if (HoldingBall) {
            Vector3 forceDir = (transform.position - Ball.transform.position).normalized;
            float grabIntensity = (transform.position - Ball.transform.position).magnitude.MapInterval(0.0f, MaxGrabDistance, 0.0f, MaxGrabIntensity);
            BallRigidBody.AddForce(forceDir * grabIntensity);

            BallRigidBody.velocity *= GrabVelocityDecay;
        }
    }
}
