using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int shotsFired = 0;
    public static float globalAccuracy = 0;
    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnTarget", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTarget()
    {
        Instantiate(target, new Vector3(Random.Range(-7, -3) * 2,  Random.Range(0, 2) * -2 - 10, 33), target.transform.rotation);
    }
}
