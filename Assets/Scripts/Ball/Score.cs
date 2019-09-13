using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Score : MonoBehaviour {
    public Text ScoreText;
    public float ResetPeriod = 3.0f;
    public Collider ScoreAreaCollider;

    private int CurrentScore = 0;
    public bool InResetPeriod { get; private set; }
    private float ResetCountdown;
    private Vector3 StartPosition;
    private Rigidbody RigidBody;

    public void Awake() {
        RigidBody = GetComponent<Rigidbody>();
    }

    public void Start() {
        UpdateText();
        StartPosition = transform.position;
    }

    public void Update() {
        if (InResetPeriod) {
            ResetCountdown -= Time.deltaTime;

            if (ResetCountdown <= 0.0f) {
                InResetPeriod = false;

                transform.position = StartPosition;
                RigidBody.velocity = Vector3.zero;
                RigidBody.angularVelocity = Vector3.zero;
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other == ScoreAreaCollider && !InResetPeriod) {
            CurrentScore += 1;
            UpdateText();

            // Begin the reset countdown
            InResetPeriod = true;
            ResetCountdown = ResetPeriod;
        }
    }

    private void UpdateText() {
        ScoreText.text = "Score: " + CurrentScore.ToString();
    }
}
