using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float sensitivity;
    [SerializeField] float speed;
    [SerializeField] float upwardsRotationLimit = -50f;
    [SerializeField] float downwardsRotationLimit = 30f;
    const string mouseX = "Mouse X";
    const string mouseY = "Mouse Y";
    const string verticalMovement = "Vertical";
    const string horizontalMovement = "Horizontal";
    const float gravity = -9.81f;
    float downwardsSpeed = 0f;
    Vector2 currentRotation;
    CharacterController characterController;
    bool isGrounded;
    [SerializeField] Camera fpsCam;
    int targetLayerMask = (1 << 6) | (1 << 7) | (1 << 8);
    [SerializeField] Text ammo;


    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        characterController = this.GetComponent<CharacterController>();
        UpdateAmmo updateAmmo = ammo.GetComponent<UpdateAmmo>();
        updateAmmo.changeText();
    }


    void Update()
    {
        MovePlayer();
        MoveCamera();
        if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.ammo > 0)
        {
            Shoot();
            --GameManager.ammo;
            UpdateAmmo updateAmmo = ammo.GetComponent<UpdateAmmo>();
            updateAmmo.changeText();
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
        // Variables needed
        Ray shootingRay;
        shootingRay = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        Debug.DrawRay(fpsCam.transform.position, transform.forward * 1000, Color.red, 2);
        RaycastHit hit;
        float accuracy;
        double reactionTime;
        ++GameManager.shotsFired;

        // Shoot ray
        if (Physics.Raycast(shootingRay, out hit, 200f, targetLayerMask))
        {
            // Call targetHit depending on whether the target is small or big
            TargetCollision targetCollision = hit.transform.gameObject.GetComponent<TargetCollision>();
            targetCollision.targetHit(hit);
            accuracy = targetCollision.accuracy;
            reactionTime = targetCollision.reactionTime;

        } else
        {
            print("-----------------------MISSED-----------------------");
            accuracy = 0;
            reactionTime = 0;
        }
        GameManager.globalAccuracy = (GameManager.globalAccuracy * (GameManager.shotsFired - 1) + accuracy) / GameManager.shotsFired;
        printStats(accuracy, reactionTime);
    }

    void printStats(float accuracy, double reactionTime)
    {
        print("Score: " + GameManager.score);
        print("Accuracy: " + GameManager.globalAccuracy + "%");
        print("Last Shot Accuracy: " + accuracy + "%");
        if (reactionTime != 0)
        {
            print("Reaction Time: " + reactionTime + "ms");
        }
        print("Shots Fired: " + GameManager.shotsFired);
    }

    
}
