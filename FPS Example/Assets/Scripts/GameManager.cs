using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int shotsFired = 0;
    public static float globalAccuracy = 0;
    public static int ammo = 20;
    [SerializeField] GameObject target;
    List<Vector3> positions = new List<Vector3>();
    public List<GameObject> flippingTargets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -7; i < -3; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                positions.Add(new Vector3(i * 2, j * - 2 - 10, 33));
            }
        }
        Invoke("SpawnTarget", 1);
        InvokeRepeating("ChooseFlippingTarget", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTarget()
    {
        int index = Random.Range(0, positions.Count);
        Vector3 position = positions[index];
        positions.RemoveAt(index);
        Instantiate(target, position, target.transform.rotation);
        if (positions.Count > 0)
        {
            print("Targets Left: " + positions.Count);
            Invoke("SpawnTarget", 3);
        }
    }

    void ChooseFlippingTarget()
    {
        if (flippingTargets.Count > 0)
        {
            int targetIndex = Random.Range(0, flippingTargets.Count);
            flippingTargets[targetIndex].GetComponent<NormalFlippingMovement>().ResetRotationAndTime();
        }
    }
}
