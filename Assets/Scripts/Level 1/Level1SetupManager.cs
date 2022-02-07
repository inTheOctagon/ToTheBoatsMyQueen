using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class Level1SetupManager : MonoBehaviour
{
    [Header("SETUP VARIABLES")]
    
    public bool setup = true;

    [Header("Actor Variables")]
    [SerializeField] GameObject enemyOneObject;
    [SerializeField] GameObject enemyTwoObject;
    [SerializeField] GameObject enemyThreeObject;

    private NavMeshAgent enemyOneNavMeshComp;
    private NavMeshAgent enemyTwoNavMeshComp;
    private NavMeshAgent enemyThreeNavMeshComp;

    private BoardEnemy enemyScript;

    [SerializeField] Vector3 enemyOneBoardPos;
    [SerializeField] Vector3 enemyTwoBoardPos;
    [SerializeField] Vector3 enemyThreeBoardPos;

    private bool moved = false;

    [Header("Capturing Variables")]
    [SerializeField] GameObject gateCam;
    [SerializeField] Vector3 dollyTargetPos = new Vector3(30, 4.5f, 0);
    [SerializeField] float dollySpeed;
    [SerializeField] GameObject gameCam;
    
    [Header("Door Variables")]
    [SerializeField] GameObject doorRObject;
    [SerializeField] GameObject doorLObject;
    private bool doorOpened = false;

    [Header("Tutorial Variables")]
    [SerializeField] GameObject tutorialPanelOne;
    [SerializeField] GameObject tutorialPanelTwo;

    private bool tutorialFirstStep;
    private bool tutorialSecondStep;
    private bool tutorialThirdStep;

    [SerializeField] Level1GameplayManager gameplayManager; 


    private void Start()
    {
        enemyOneBoardPos = new Vector3(6, 2, 6);
        enemyTwoBoardPos = new Vector3(10, 2, 2);
        enemyThreeBoardPos = new Vector3(14, 2, -2);

        enemyOneNavMeshComp = enemyOneObject.GetComponent<NavMeshAgent>();
        enemyTwoNavMeshComp = enemyTwoObject.GetComponent<NavMeshAgent>();
        enemyThreeNavMeshComp = enemyThreeObject.GetComponent<NavMeshAgent>();
    }

    public void SetupStarter()
    {
        var enemyOneGatePos = new Vector3(42, 14.95f, 3);
        var enemyTwoGatePos = new Vector3(42, 15, -0.2f);
        var enemyThreeGatePos = new Vector3(42.4f, 15, -3.3f);


        enemyOneNavMeshComp.SetDestination(enemyOneGatePos);
        enemyTwoNavMeshComp.SetDestination(enemyTwoGatePos);
        enemyThreeNavMeshComp.SetDestination(enemyThreeGatePos);

        StartCoroutine("doorTimer");
    }

    private void Update()
    {

        if (!setup)
        {
            if (gateCam.transform.position == dollyTargetPos && !moved)
            {
                var enemyOneWarpPos = new Vector3(30, 2, 3);
                var enemyTwoWarpPos = new Vector3(30, 2, 0);
                var enemyThreeWarpPos = new Vector3(30, 2, -3);

                enemyOneNavMeshComp.Warp(enemyOneWarpPos);
                enemyTwoNavMeshComp.Warp(enemyTwoWarpPos);
                enemyThreeNavMeshComp.Warp(enemyThreeWarpPos);

                gameCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;

                enemyOneNavMeshComp.speed = 5;
                enemyTwoNavMeshComp.speed = 5;
                enemyThreeNavMeshComp.speed = 5;

                enemyOneNavMeshComp.SetDestination(enemyOneBoardPos);
                enemyTwoNavMeshComp.SetDestination(enemyTwoBoardPos);
                enemyThreeNavMeshComp.SetDestination(enemyThreeBoardPos);

                moved = true;
                setup = true;
                tutorialFirstStep = true;
                if (tutorialFirstStep) Debug.Log("ye this works");
            }

            else if (!doorOpened)
            {
                doorRObject.transform.Rotate(0, 30 * Time.deltaTime, 0);
                doorLObject.transform.Rotate(0, -30 * Time.deltaTime, 0);

                gateCam.transform.position = Vector3.MoveTowards(gateCam.transform.position, dollyTargetPos, dollySpeed * Time.deltaTime);
            }

        }
        else if (enemyOneObject.transform.position.x == enemyOneBoardPos.x && enemyTwoObject.transform.position.x == enemyTwoBoardPos.x && enemyThreeObject.transform.position.x == enemyThreeBoardPos.x && tutorialFirstStep)
        {
            //they move by 1 to reach the red line to kill the queen
            tutorialPanelOne.SetActive(true);
            tutorialFirstStep = false;
            tutorialSecondStep = true;

        }
        else if (Input.anyKeyDown && tutorialSecondStep)
        {
            tutorialPanelOne.SetActive(false);
            tutorialSecondStep = false;
            enemyOneObject.GetComponent<BoardEnemy>().MoveEnemy();
            enemyTwoObject.GetComponent<BoardEnemy>().MoveEnemy();
            enemyThreeObject.GetComponent<BoardEnemy>().MoveEnemy();
            StartCoroutine("tutorialSecondStepTimer");

        }
        else if (Input.anyKeyDown && tutorialThirdStep)
        {
            tutorialThirdStep = false;
            gameplayManager.friendlyTurn = true;
            tutorialPanelTwo.SetActive(false);
        }
        else return;
        
    }

    IEnumerator doorTimer()
    {
        yield return new WaitForSeconds(3);
        setup = false;
    }

    IEnumerator tutorialSecondStepTimer()
    {
        yield return new WaitForSeconds(2);
        tutorialPanelTwo.SetActive(true);
        tutorialThirdStep = true;
    }
}
