using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour, IObserver{

    InputHandle inputHandle;
    public CinemachineVirtualCamera playerCam;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private CharacterController characterController;
    public float movementSpeed = 5.0f;
    public float lookSensitivity = 0.5f;
    public float jumpForce = 5.0f;
    private bool isJumping = false;
    public float gravity = 9.81f;
    public Vector3 velocity;
    private float verticalLookRotation;

    public bool canControl = true;

    void Start() {
        FindObjectOfType<GameManager>().AddObserver(this);
        inputHandle = GetComponent<InputHandle>();
        characterController = GetComponent<CharacterController>();
        velocity = new Vector3(0, -10, 0);
    }

    void Update() {
        if (canControl) {
            moveInput = inputHandle.MoveAction;
            lookInput = inputHandle.LookAction;

            float horizontal = moveInput[0];
            float vertical = moveInput[1];

            Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
            movementDirection = transform.TransformDirection(movementDirection);
            movementDirection *= movementSpeed;
            characterController.Move((movementDirection + velocity) * Time.deltaTime);
            float mouseX = lookInput[0] * lookSensitivity;
            float mouseY = lookInput[1] * lookSensitivity;
            transform.Rotate(Vector3.up * mouseX);
            verticalLookRotation += mouseY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
            playerCam.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        }
    }

    public void OnNotify<T>(T data) {
        print("Movement dostal " + data);
        switch (data) {
            case GameState.Paused:
                canControl = false;
                break;
            case GameState.WalkMode:
                canControl = true;
                break;
        }
    }
}
