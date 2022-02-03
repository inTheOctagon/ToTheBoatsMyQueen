using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class MenuManager : MonoBehaviour
{
    [Header("Setup Variables")]

    [SerializeField] GameObject playButtonObject;

    [SerializeField] GameObject enemy1Object;
    [SerializeField] GameObject enemy2Object;
    [SerializeField] GameObject enemy3Object;

    [SerializeField] GameObject gateCam;
    [SerializeField] Vector3 dollyTargetPos = new Vector3(30, 4.5f, 0);
    [SerializeField] float gateCamSpeed;
    [SerializeField] GameObject gameCam;

    private bool doorBool;
    [SerializeField] GameObject doorR;
    [SerializeField] GameObject doorL;
    [SerializeField] float doorSpeed = 3;

    



    

    public void PlayButton()
    {


        var enemy1GatePos = new Vector3(42, 14.95f, 3);
        var enemy2GatePos = new Vector3(42, 15, -0.2f);
        var enemy3GatePos = new Vector3(42.4f, 15, -3.3f);

        enemy1Object.GetComponent<NavMeshAgent>().SetDestination(enemy1GatePos);
        enemy2Object.GetComponent<NavMeshAgent>().SetDestination(enemy2GatePos);
        enemy3Object.GetComponent<NavMeshAgent>().SetDestination(enemy3GatePos);

        playButtonObject.SetActive(false);

        StartCoroutine("doorTimer");

        //enemy1 gate pos: 42, 14.95, 3 - enemy2 gate pos: 42, 15, -0.2 - enemy3 gate pos: 42.4, 15, -3.3
        //enemy1 board pos: 6, 2 , 6 - enemy2 board pos: 10, 2 , 2 - enemy3 board pos: 14, 2, -2

        //enemies walk up so their shadows cast on the gate
        //the gate opens
        //camera changes
        // they walk to their board positions.
    }

    private void Update()
    {
        if (gateCam.transform.position == dollyTargetPos)
        {
            gameCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        }
        else if (doorR.transform.rotation == Quaternion.Euler(0, 90, 0) && doorL.transform.rotation == Quaternion.Euler(0, -90, 0))
        {
            doorBool = false;
            gateCam.transform.position = Vector3.MoveTowards(gateCam.transform.position, dollyTargetPos, gateCamSpeed * Time.deltaTime);
        }
        else if (doorBool && doorL.transform.rotation.y != -90 && doorL.transform.rotation.y != 90)
        {
            doorR.transform.Rotate(0, 15 * Time.deltaTime, 0);
            doorL.transform.Rotate(0, -15 * Time.deltaTime, 0);
        }
        else return;
    }

    IEnumerator doorTimer()
    {
        yield return new WaitForSeconds(2);
        doorBool = true;
    }
}
