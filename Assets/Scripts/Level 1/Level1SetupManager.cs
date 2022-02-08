using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class Level1SetupManager : MonoBehaviour
{
    [Header("SETUP VARIABLES")]
    
    public bool setupOne = true;
    public bool setupTwo = true;
    public float waveOneEnemyNumber = 3;

    [SerializeField] GameObject doorRObject;
    [SerializeField] GameObject doorLObject;
    private bool doorOpened = false;

    [SerializeField] GameObject boardGuard;
    [SerializeField] BoardGuard boardScript;

    [Header("Wave One Variables")]
    [SerializeField] GameObject enemyOneObject;
    [SerializeField] GameObject enemyTwoObject;
    [SerializeField] GameObject enemyThreeObject;

    private NavMeshAgent enemyOneNavMeshComp;
    private NavMeshAgent enemyTwoNavMeshComp;
    private NavMeshAgent enemyThreeNavMeshComp;

    [SerializeField] Vector3 enemyOneBoardPos;
    [SerializeField] Vector3 enemyTwoBoardPos;
    [SerializeField] Vector3 enemyThreeBoardPos;

    private bool moved = false;

    [SerializeField] GameObject tutorialPanelOne;
    [SerializeField] GameObject tutorialPanelTwo;

    private bool tutorialFirstStep;
    private bool tutorialSecondStep;
    private bool tutorialThirdStep;

    [Header("Wave Two Variables")]
    [SerializeField] GameObject enemyFourObject;
    [SerializeField] GameObject enemyFiveObject;
    [SerializeField] GameObject enemySixObject;
    [SerializeField] GameObject enemySevenObject;
    [SerializeField] GameObject enemyEightObject;

    private NavMeshAgent enemyFourNavMeshComp;
    private NavMeshAgent enemyFiveNavMeshComp;
    private NavMeshAgent enemySixNavMeshComp;
    private NavMeshAgent enemySevenNavMeshComp;
    private NavMeshAgent enemyEightNavMeshComp;

    [SerializeField] Vector3 enemyFourBoardPos;
    [SerializeField] Vector3 enemyFiveBoardPos;
    [SerializeField] Vector3 enemySixBoardPos;
    [SerializeField] Vector3 enemySevenBoardPos;
    [SerializeField] Vector3 enemyEightBoardPos;

    private bool tutorialFourthStep;
    private bool tutorialFifthStep;


    [Header("Capturing Variables")]
    [SerializeField] GameObject gateCam;
    [SerializeField] Vector3 dollyTargetPos = new Vector3(30, 4.5f, 0);
    [SerializeField] float dollySpeed;
    [SerializeField] GameObject gameCam;
    
    [SerializeField] Level1GameplayManager gameplayManager; 


    private void Start()
    {
        boardScript = boardGuard.GetComponent<BoardGuard>();

        enemyOneBoardPos = new Vector3(6, 2, 6);
        enemyTwoBoardPos = new Vector3(10, 2, 2);
        enemyThreeBoardPos = new Vector3(14, 2, -2);

        enemyFourBoardPos = new Vector3(14, 2.1f, -2);
        enemyFiveBoardPos = new Vector3(2, 2.1f, 2);
        enemySixBoardPos = new Vector3(6, 2.1f, -2);
        enemySevenBoardPos = new Vector3(10, 2.5f, 2);
        enemyEightBoardPos = new Vector3(10, 2.1f, -6);

        enemyOneNavMeshComp = enemyOneObject.GetComponent<NavMeshAgent>();
        enemyTwoNavMeshComp = enemyTwoObject.GetComponent<NavMeshAgent>();
        enemyThreeNavMeshComp = enemyThreeObject.GetComponent<NavMeshAgent>();

        enemyFourNavMeshComp = enemyFourObject.GetComponent<NavMeshAgent>();
        enemyFiveNavMeshComp = enemyFiveObject.GetComponent<NavMeshAgent>();
        enemySixNavMeshComp = enemySixObject.GetComponent<NavMeshAgent>();
        enemySevenNavMeshComp = enemySevenObject.GetComponent<NavMeshAgent>();
        enemyEightNavMeshComp = enemyEightObject.GetComponent<NavMeshAgent>();

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

        if (!setupOne)
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

                tutorialFirstStep = true;

                doorOpened = true;

            }

            else if (!doorOpened)
            {
                doorRObject.transform.Rotate(0, 30 * Time.deltaTime, 0);
                doorLObject.transform.Rotate(0, -30 * Time.deltaTime, 0);

                gateCam.transform.position = Vector3.MoveTowards(gateCam.transform.position, dollyTargetPos, dollySpeed * Time.deltaTime);
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
                setupOne = true;
            }
            else return;

        }
        else if (!setupTwo)
        {
            Debug.Log(enemyFiveBoardPos);
            Debug.Log(enemyFiveObject.transform.position);

            if (enemyFiveObject.transform.position.x == enemyFiveBoardPos.x && tutorialFourthStep)
            {
                
                //panel çýkar 
                tutorialFourthStep = false;
                tutorialFifthStep = true;
                
            }
            else if(Input.anyKeyDown && tutorialFifthStep)
            {
                //paneli kapat ve
                gameplayManager.enemyTurn = true;
                setupTwo = true;
                gameplayManager.enemyTurn = true;
            }
        }
        
        else return;
        
        
    }

    public void CheckForSetupTwo()
    {
        waveOneEnemyNumber--;
        if (waveOneEnemyNumber == 0)
        {
            gameplayManager.friendlyTurn = false;

            enemyFourObject.SetActive(true);
            enemyFiveObject.SetActive(true);
            enemySixObject.SetActive(true);
            enemySevenObject.SetActive(true);
            enemyEightObject.SetActive(true);

            boardScript.SetDestination(new Vector3(-14, 2, 2));

            enemyFourNavMeshComp.SetDestination(enemyFourBoardPos);
            enemyFiveNavMeshComp.SetDestination(enemyFiveBoardPos);
            enemySixNavMeshComp.SetDestination(enemySixBoardPos);
            enemySevenNavMeshComp.SetDestination(enemySevenBoardPos);
            enemyEightNavMeshComp.SetDestination(enemyEightBoardPos);

            setupTwo = false;
            tutorialFourthStep = true;

            Debug.Log("wave 2 baby");
        }

    }


    IEnumerator doorTimer()
    {
        yield return new WaitForSeconds(3);
        setupOne = false;
    }

    IEnumerator tutorialSecondStepTimer()
    {
        yield return new WaitForSeconds(2);
        tutorialPanelTwo.SetActive(true);
        tutorialThirdStep = true;
    }

    
}
