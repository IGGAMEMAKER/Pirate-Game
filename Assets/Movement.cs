using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public GameObject ObservableObject;

    Camera Camera;
    float rotation;

    float movementSpeed;

    Vector3 Distance;

    float ZRotationTimer;
    float ZRotationMultiplier;
    float ZRotationPeriod;

    float ZRotationSensitivity;

    // Use this for initialization
    void Start () {
        Camera = GetComponent<Camera>();

        rotation = 0.5f;
        movementSpeed = 0.5f;

        Distance = ObservableObject.transform.position - transform.position;

        ZRotationMultiplier = 1;
        ZRotationPeriod = 1.5f;
        ZRotationTimer = ZRotationPeriod;

        ZRotationSensitivity = 0.05f;

    }

    void Move(float direction)
    {
        ObservableObject.transform.position += ObservableObject.transform.forward * movementSpeed * direction;

        Camera.transform.position = ObservableObject.transform.position - Distance;

        ZRotationTimer -= Time.deltaTime;

        if (ZRotationTimer < 0)
        {
            ZRotationTimer = ZRotationPeriod;
            ZRotationMultiplier *= -1f;
        }

        float ZRotation = ZRotationSensitivity * ZRotationMultiplier;
        ObservableObject.transform.Rotate(new Vector3(0, 0, ZRotation));
    }

    void Rotate(float angle)
    {
        ObservableObject.transform.Rotate(0, angle, 0, Space.Self);
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W))
            Move(1);

        if (Input.GetKey(KeyCode.S))
            Move(-1);

        if (Input.GetKey(KeyCode.D))
            Rotate(rotation);

        if (Input.GetKey(KeyCode.A))
            Rotate(-rotation);
    }
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}
}
