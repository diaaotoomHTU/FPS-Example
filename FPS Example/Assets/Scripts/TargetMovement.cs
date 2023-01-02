using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] float speed;
    int movingLeft = 1;
    Vector3 originalPosition;
    bool movementDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementDisabled)
        {
            Move();
        }
        
    }

    void Move()
    {
        bool shot = this.GetComponent<TargetCollision>().shot;
        if (shot)
        {
            movementDisabled = true;
        }
        this.transform.position = this.transform.position + new Vector3(speed * movingLeft, 0, 0);
        if (!shot && this.transform.position.x > originalPosition.x + 2 || this.transform.position.x < originalPosition.x - 2)
        {
            movingLeft = -movingLeft;
        }
    }
}
