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
    public bool shot = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Time.time);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TargetHit(RaycastHit hit)
    {
        print("-----------------------HIT-----------------------");
        shot = true;
        Vector3 point = hit.point;
        DistanceAndScore distanceAndScore = this.transform.gameObject.GetComponent<DistanceAndScore>();
        if (this.transform.gameObject.layer == 6)
        {
            Destroy(this.transform.gameObject);
            this.reactionTime = distanceAndScore.CalculateReactionTime(spawnedTime);
        } else if (this.transform.gameObject.layer == 7)
        {
            Instantiate(bulletImpact, hit.point + (-0.05f * this.transform.up), Quaternion.identity);
            this.reactionTime = 0;
        } else
        {
            this.transform.DOLocalRotate(new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, -90), 0.2f);
            this.gameObject.layer = 9;
            this.reactionTime = distanceAndScore.CalculateReactionTime(spawnedTime);
        }
        this.accuracy = distanceAndScore.CalculateAccuracy(point);
    }

}
