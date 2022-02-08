using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level1GameplayManager : MonoBehaviour
{
    [Header("BOARD VARIABLES")]

    [Header("Gameplay Variables")]

    public bool friendlyTurn;
    public bool enemyTurn;

    [Header("Actor Variables")]
    [SerializeField] GameObject guardObject;
    [SerializeField] GameObject enemyOneObject;
    [SerializeField] GameObject enemyTwoObject;
    [SerializeField] GameObject enemyThreeObject;

    private BoardEnemy enemyOneScript;
    private BoardEnemy enemyTwoScript;
    private BoardEnemy enemyThreeScript;

    private BoardGuard guardScript;



    private void Start()
    {
        enemyOneScript = enemyOneObject.GetComponent<BoardEnemy>();
        enemyTwoScript = enemyTwoObject.GetComponent<BoardEnemy>();
        enemyThreeScript = enemyThreeObject.GetComponent<BoardEnemy>();

        guardScript = guardObject.GetComponent<BoardGuard>();
    }
    private void Update()
    {
        if(friendlyTurn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                guardScript.MoveWithMechanism();
                


            }

            
        }
        else if(enemyTurn)
        {
            enemyOneScript.MoveEnemy();
            enemyTwoScript.MoveEnemy();
            enemyThreeScript.MoveEnemy();

            enemyTurn = false;

            

            StartCoroutine("roundTransitionTimer");
        }

        


    }

    IEnumerator roundTransitionTimer()
    {
        yield return new WaitForSeconds(2);
        
        friendlyTurn = true;

        if (enemyOneObject.transform.position.x == -14 || enemyTwoObject.transform.position.x == -14 || enemyThreeObject.transform.position.x == -14)
        {
            Debug.Log("you lost");
        }


    }

    


}

