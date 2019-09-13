using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OutOfBounds : MonoBehaviour {
    public Transform FloorTransform;
    private Vector3 StartPosition;
    private Rigidbody RigidBody;

    public void Awake() {
        RigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    public void Start() {
        StartPosition = transform.position;
    }

    public void Update() {
        // There is no floor in the out of bounds area
        if (transform.position.y <= FloorTransform.position.y - 10.0f) {
            transform.position = StartPosition;
            RigidBody.velocity = Vector3.zero;
            RigidBody.angularVelocity = Vector3.zero;
        }
    }
}
