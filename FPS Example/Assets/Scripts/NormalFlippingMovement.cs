using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFlippingMovement : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void ResetRotationAndTime()
    {
        bool shot = this.GetComponent<TargetCollision>().shot;
        if (shot)
        {
            gameManager.GetComponent<GameManager>().flippingTargets.Remove(this.gameObject);
        }
        else if (!shot)
        {
            this.gameObject.layer = 8;
            transform.DORotate(new Vector3(this.transform.rotation.x, this.transform.rotation.y, 0), 0.2f);
            TargetCollision targetCollision = this.GetComponent<TargetCollision>();
            targetCollision.spawnedTime = System.DateTime.Now;
            Invoke("DisableTarget", 2f);
        }
    }

    void DisableTarget()
    {
        this.gameObject.layer = 9;
        transform.DOLocalRotate(new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, -90), 0.2f);
    }
}
