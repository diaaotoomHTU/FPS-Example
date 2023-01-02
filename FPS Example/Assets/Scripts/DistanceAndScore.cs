using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DistanceAndScore : MonoBehaviour
{
    [SerializeField] Transform bullseyePoint;
    [SerializeField] Transform bullseyeEnd;
    [SerializeField] Transform targetEdge;
    float bullseyeDistance;
    float completeDistance;
    // Start is called before the first frame update
    void Start()
    {
        
        bullseyeDistance = Vector3.Distance(bullseyePoint.position, bullseyeEnd.position);
        completeDistance = Vector3.Distance(bullseyePoint.position, targetEdge.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateAccuracy(Vector3 point)
    {
        float distance = Vector3.Distance(bullseyePoint.position, point);
        float accuracy;
        if (distance <= bullseyeDistance)
        {
            accuracy = 100;
        } else
        {
            accuracy = (completeDistance - distance) / completeDistance * 100;
        }
        GameManager.score += (int) accuracy / 10;
        return accuracy;
    }

    public double CalculateReactionTime(DateTime timeAppeared)
    {
        return (DateTime.Now - timeAppeared).TotalMilliseconds;
    }

}
