using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetRotationAndTime()
    {
        transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        this.GetComponent<TargetCollision>().spawnedTime = System.DateTime.Now;
    }
}