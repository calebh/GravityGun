using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public Camera PlayerCamera;
    public float MovementSpeed = 5.0f;

    private Rigidbody PlayerRigidBody;

    public void Awake() {
        PlayerRigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        Vector3 forward = PlayerCamera.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        Vector3 movementDirection = (Input.GetAxis("Vertical") * forward +
            Input.GetAxis("Horizontal") * right).normalized;

        Vector3 horizontalPlaneVelocity = movementDirection * MovementSpeed * Time.deltaTime;

        PlayerRigidBody.velocity = new Vector3(horizontalPlaneVelocity.x, PlayerRigidBody.velocity.y, horizontalPlaneVelocity.z);
    }
}
