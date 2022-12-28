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
    // [SerializeField] Transform groundCheck;
    // [SerializeField] LayerMask groundMask;
    // float groundCheckRadius = 0.4f;
    const string mouseX = "Mouse X";
    const string mouseY = "Mouse Y";
    const string verticalMovement = "Vertical";
    const string horizontalMovement = "Horizontal";
    const float gravity = -9.81f;
    float downwardsSpeed = 0f;
    Vector2 currentRotation;
    CharacterController characterController;
    bool isGrounded;
    // float jumpHeight = 50f;

    [SerializeField] Camera fpsCam;
    int targetLayerMask = 1 << 6;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = this.GetComponent<CharacterController>();
        
    }


    void Update()
    {
        MovePlayer();
        MoveCamera();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
    void MoveCamera()
    {
        currentRotation.x -= Input.GetAxis(mouseY) * sensitivity;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upwardsRotationLimit, downwardsRotationLimit);
        currentRotation.y += Input.GetAxis(mouseX) * sensitivity;
        this.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
    }

    void MovePlayer()
    {
        downwardsSpeed += gravity;
        Vector3 forward = transform.forward;
        forward.y = 0;
        Vector3 movement = transform.right * Input.GetAxis(horizontalMovement) + forward * Input.GetAxis(verticalMovement) + Vector3.up * downwardsSpeed * Time.deltaTime;
        characterController.Move(movement * speed * Time.deltaTime);
    }

    void Shoot()
    {
        Ray shootingRay;
        shootingRay = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        Debug.DrawRay(fpsCam.transform.position, transform.forward * 1000, Color.red, 2);
        RaycastHit hit;
        if (Physics.Raycast(shootingRay, out hit, 200f, targetLayerMask))
        {
            Destroy(hit.transform.gameObject);
        }
    }

    
}
