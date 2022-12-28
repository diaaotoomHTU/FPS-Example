using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float sensitivity;
    [SerializeField] float speed;
    [SerializeField] float upwardsRotationLimit = -50f;
    [SerializeField] float downwardsRotationLimit = 30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    float groundCheckRadius = 0.4f;
    const string mouseX = "Mouse X";
    const string mouseY = "Mouse Y";
    const string verticalMovement = "Vertical";
    const string horizontalMovement = "Horizontal";
    const float gravity = -9.81f;
    float downwardsSpeed = 0f;
    Vector2 currentRotation;
    CharacterController characterController;
    bool isGrounded;
    float jumpHeight = 50f;

    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
    }

    
    void Update()
    {
        // Camera movement
        currentRotation.x -= Input.GetAxis(mouseY) * sensitivity;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upwardsRotationLimit, downwardsRotationLimit);
        currentRotation.y += Input.GetAxis(mouseX) * sensitivity;
        this.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);

        // Player movement and jumping
        downwardsSpeed += gravity;
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        print(isGrounded);
        if (isGrounded)
        {
            downwardsSpeed = -2f;
        }
        Vector3 forward = transform.forward;
        forward.y = 0;
        Vector3 movement = transform.right * Input.GetAxis(horizontalMovement) + forward * Input.GetAxis(verticalMovement) + Vector3.up * downwardsSpeed * Time.deltaTime;
        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            movement += Vector3.up * Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
        characterController.Move(movement * speed * Time.deltaTime);

        
    }
}
