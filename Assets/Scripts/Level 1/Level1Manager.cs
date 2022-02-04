using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class Level1Manager : MonoBehaviour
{
    [Header("Actor Variables")]

    [SerializeField] GameObject enemy1Object;
    [SerializeField] GameObject enemy2Object;
    [SerializeField] GameObject enemy3Object;
    private bool moved = false;

    [Header("Capturing Variables")]

    [SerializeField] GameObject gateCam;
    [SerializeField] Vector3 dollyTargetPos = new Vector3(30, 4.5f, 0);
    [SerializeField] float dollySpeed;
    [SerializeField] GameObject gameCam;
    
    [Header("Door Variables")]

    [SerializeField] GameObject doorR;
    [SerializeField] GameObject doorL;
    private bool doorBool = false;

    public void SetupStarter()
    {
        var enemy1GatePos = new Vector3(42, 14.95f, 3);
        var enemy2GatePos = new Vector3(42, 15, -0.2f);
        var enemy3GatePos = new Vector3(42.4f, 15, -3.3f);

        enemy1Object.GetComponent<NavMeshAgent>().SetDestination(enemy1GatePos);
        enemy2Object.GetComponent<NavMeshAgent>().SetDestination(enemy2GatePos);
        enemy3Object.GetComponent<NavMeshAgent>().SetDestination(enemy3GatePos);

        StartCoroutine("doorTimer");
    }

    private void Update()
    {
        if (gateCam.transform.position == dollyTargetPos && !moved)
        {
            var enemy1BoardPos = new Vector3(6, 2, 6);
            var enemy2BoardPos = new Vector3(10, 2, 2);
            var enemy3BoardPos = new Vector3(14, 2, -2);

            enemy1Object.GetComponent<NavMeshAgent>().Warp(new Vector3(30, 2, 3));
            enemy2Object.GetComponent<NavMeshAgent>().Warp(new Vector3(30, 2, 0));
            enemy3Object.GetComponent<NavMeshAgent>().Warp(new Vector3(30, 2, -3));

            gameCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;

            enemy1Object.GetComponent<NavMeshAgent>().speed = 5;
            enemy2Object.GetComponent<NavMeshAgent>().speed = 5;
            enemy3Object.GetComponent<NavMeshAgent>().speed = 5;

            enemy1Object.GetComponent<NavMeshAgent>().SetDestination(enemy1BoardPos);
            enemy2Object.GetComponent<NavMeshAgent>().SetDestination(enemy2BoardPos);
            enemy3Object.GetComponent<NavMeshAgent>().SetDestination(enemy3BoardPos);
            
            doorBool = false;
            moved = true;
        }

        else if (doorBool)
        {
            doorR.transform.Rotate(0, 30 * Time.deltaTime, 0);
            doorL.transform.Rotate(0, -30 * Time.deltaTime, 0);
            
            gateCam.transform.position = Vector3.MoveTowards(gateCam.transform.position, dollyTargetPos, dollySpeed * Time.deltaTime);
        }
        else return;
    }

    IEnumerator doorTimer()
    {
        yield return new WaitForSeconds(3);
        doorBool = true;
    }
}
