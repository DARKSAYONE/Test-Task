using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float mouseSensitivity = 2.0f;

    private Camera playerCamera;
    private CharacterController characterController;
    private float camRotation = 0f;
    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        if (playerCamera == null) Debug.LogError("Player camera not found");
        if (characterController == null) Debug.LogError("CharacterController on player not found");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        PlayerMove();
        CameraMove();
    }

    private void PlayerMove()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = transform.TransformDirection(movement);
        Vector3 moveDirection = movement * walkSpeed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        camRotation -= mouseY;
        camRotation = Mathf.Clamp(camRotation, -80f, 80f);
        playerCamera.transform.localRotation = Quaternion.Euler(camRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
