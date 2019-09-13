using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour
{
    public float MaxSensitivity = 10.0f;
    public float MinSensitivity = 0.3f;

    public float SensitivityX = 3.0f;
    public float SensitivityY = 3.0f;

    public float YAngleConstraint = 88.0f;

    public float RotationX = 0.0f;
    public float RotationY = 0.0f;
    private Queue<float> PrevDeltaRotX = new Queue<float>();
    private Queue<float> PrevDeltaRotY = new Queue<float>();

    public int NumOfFramesToAverage = 10;

    public void Start() {
        if (GetComponent<Rigidbody>()) {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        DisableCursor();
    }

    public void EnableCursor() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        float deltaRotX = Input.GetAxis("Mouse X") * SensitivityX;
        float deltaRotY = -Input.GetAxis("Mouse Y") * SensitivityY;

        PrevDeltaRotX.Enqueue(deltaRotX);
        PrevDeltaRotY.Enqueue(deltaRotY);

        while (PrevDeltaRotX.Count > NumOfFramesToAverage)
        {
            PrevDeltaRotX.Dequeue();
        }

        while (PrevDeltaRotY.Count > NumOfFramesToAverage)
        {
            PrevDeltaRotY.Dequeue();
        }

        int framerateAdjustedNumOfFrames = (int)(1.0f / Time.smoothDeltaTime).MapIntervalWithClamp(0.0f, 60.0f, 5.0f, NumOfFramesToAverage);

        float deltaX = PrevDeltaRotX.Take(framerateAdjustedNumOfFrames).Average();
        float deltaY = PrevDeltaRotY.Take(framerateAdjustedNumOfFrames).Average();

        float newRotX = RotationX + deltaX;
        newRotX = ((newRotX % 360.0f) + 360.0f) % 360.0f;

        float newRotY = RotationY + deltaY;
        newRotY = Mathf.Clamp(newRotY, -YAngleConstraint, YAngleConstraint);

        RotationX = newRotX;
        RotationY = newRotY;

        transform.localEulerAngles = new Vector3(RotationY, RotationX, 0.0f);
    }
}