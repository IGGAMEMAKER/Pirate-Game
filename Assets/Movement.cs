using System;
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

    float RotationAngleX;
    float RotationAngleZ;

    public float Sensitivity;

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

    void RockTheShip()
    {
        ZRotationTimer -= Time.deltaTime;

        if (ZRotationTimer < 0)
        {
            ZRotationTimer = ZRotationPeriod;
            ZRotationMultiplier *= -1f;
        }

        ObservableObject.transform.Rotate(new Vector3(0, 0, ZRotationSensitivity * ZRotationMultiplier));
    }

    void Move(float direction)
    {
        ObservableObject.transform.position += ObservableObject.transform.forward * movementSpeed * direction;

        //Camera.transform.position = ObservableObject.transform.position - Distance;

        RockTheShip();
    }

    void RotateObject(float angle)
    {
        ObservableObject.transform.Rotate(0, angle, 0, Space.Self);
    }

    void RotateCameraVertically(float direction)
    {
        RotationAngleZ += direction * Sensitivity / 100f;
    }

    void RotateCameraHorizontally(float direction)
    {
        RotationAngleX += direction * Sensitivity / 100f;
    }

    void LookAtObject()
    {
        Camera.transform.LookAt(ObservableObject.transform);

        float movementX = Mathf.Sin(RotationAngleX);
        float movementZ = Mathf.Cos(RotationAngleX);
        float movementY = Mathf.Sin(RotationAngleZ);

        Camera.transform.position = ObservableObject.transform.position + new Vector3(movementX, movementY, movementZ) * Distance.magnitude;
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W))
            Move(1);

        if (Input.GetKey(KeyCode.S))
            Move(-1);

        if (Input.GetKey(KeyCode.D))
            RotateObject(rotation);

        if (Input.GetKey(KeyCode.A))
            RotateObject(-rotation);

        if (Input.GetKey(KeyCode.UpArrow))
            RotateCameraVertically(1);

        if (Input.GetKey(KeyCode.DownArrow))
            RotateCameraVertically(-1);

        if (Input.GetKey(KeyCode.LeftArrow))
            RotateCameraHorizontally(-1);

        if (Input.GetKey(KeyCode.RightArrow))
            RotateCameraHorizontally(1);
    }

    // Update is called once per frame
    void Update () {
        ProcessInput();

        LookAtObject();
	}
}
