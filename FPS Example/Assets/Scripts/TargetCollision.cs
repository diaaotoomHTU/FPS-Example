using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TargetCollision : MonoBehaviour
{
    [SerializeField] GameObject bulletImpact;
    public DateTime spawnedTime = DateTime.Now;
    public float accuracy;
    public double reactionTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void targetHit(RaycastHit hit)
    {
        print("-----------------------HIT-----------------------");
        Vector3 point = hit.point;
        DistanceAndScore distanceAndScore = this.transform.gameObject.GetComponent<DistanceAndScore>();
        if (this.transform.gameObject.layer == 6)
        {
            Destroy(this.transform.gameObject);
        } else if (this.transform.gameObject.layer == 7)
        {
            Instantiate(bulletImpact, hit.point + (-0.05f * this.transform.up), Quaternion.identity);
        } else
        {
            this.transform.DORotate(new Vector3(0, 0, -90), 0.2f);
            TargetReset targetReset = this.GetComponent<TargetReset>();
            targetReset.Invoke("resetRotationAndTime", 2);
        }
        this.accuracy = distanceAndScore.CalculateAccuracy(point);
        this.reactionTime = distanceAndScore.CalculateReactionTime(spawnedTime);
        
    }
}
