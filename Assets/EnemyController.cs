using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemyMoveSpeed;
    //public Rigidbody rb;
    //public Transform target;

    bool chasing;
    public float distanceToChase,distanceToLoose,distanceToStop;
    public NavMeshAgent navMesh;
    Vector3 targetPoint, startingPoint;
    public float chasingTime,chaseCount;
    public GameObject enemyBulletPrefab;
    public Transform enmeyFirePoint;
    public float fireRate, fireCount,fireWaitCounter,waitBtwnShoots,shootTimeCounter;//enemy fire rate controller
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
        shootTimeCounter = 1f;
        fireWaitCounter = waitBtwnShoots;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPoint = PlayerController.instance.transform.position;
        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
                //fireCount = 1f;
                shootTimeCounter = 1f;
                fireWaitCounter = waitBtwnShoots;
            }
            if (chaseCount > 0)
            {
                chaseCount -= Time.deltaTime;
                if (chaseCount <= 0)
                {
                    navMesh.destination = startingPoint;
                }
            }
            
        }
        else
        {
            //transform.LookAt(targetPoint);
            //rb.velocity = transform.forward * enemyMoveSpeed;
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                navMesh.destination = targetPoint;
            }
            else
            {
                navMesh.destination = startingPoint;
            }
           
            
            
            if (Vector3.Distance(transform.position, targetPoint) > distanceToLoose)
            {
                chasing = false;
                navMesh.destination = startingPoint;
                chaseCount = chasingTime;
            }
            if (fireWaitCounter > 0)
            {
                fireWaitCounter -= Time.deltaTime;
                if (fireWaitCounter <= 0)
                {
                    shootTimeCounter = 1f;
                }
            }
            else
            {
                shootTimeCounter -= Time.deltaTime;
                if (shootTimeCounter > 0)
                {
                    fireCount -= Time.deltaTime;
                    if (fireCount <= 0)
                    {
                        fireCount = fireRate;
                        enmeyFirePoint.LookAt(targetPoint+new Vector3(0f,1.5f,0));
                        //Check the angle of player
                        Vector3 targetDirection = targetPoint - transform.position;
                        float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);
                        if (Mathf.Abs( angle) < 40f)
                        {
                            Instantiate(enemyBulletPrefab, enmeyFirePoint.position, enmeyFirePoint.rotation);
                        }
                        else
                        {
                            fireWaitCounter = waitBtwnShoots;
                        }
                        navMesh.destination = transform.position;


                    }

                }
                else
                {
                    fireWaitCounter = waitBtwnShoots;
                }
            }
            
            




        }

    }
}
