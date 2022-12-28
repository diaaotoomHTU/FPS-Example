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
        bullseyeDistance = getDistanceBetweenPoints(bullseyePoint.position, bullseyeEnd.position);
        completeDistance = getDistanceBetweenPoints(bullseyePoint.position, targetEdge.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float getDistanceBetweenPoints(Vector3 point1, Vector3 point2) 
    {
        float xDiff = point2.x - point1.x;
        float zDiff = point2.z - point1.z;
        float distance = Mathf.Sqrt(xDiff * xDiff + zDiff * zDiff);
        return distance;
    }

    public float CalculateAccuracy(Vector3 point)
    {
        float distance = getDistanceBetweenPoints(bullseyePoint.position, point);
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

    void updateScore()
    {

    }
}
