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
    private BoardGuard guardScript;



    private void Start()
    {
        

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
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if(Enemies != null)
            {
                foreach (GameObject enemy in Enemies)

                    enemy.GetComponent<BoardEnemy>().MoveEnemy();
            }
            

            enemyTurn = false;

            StartCoroutine("roundTransitionTimer");
        }

        


    }

    IEnumerator roundTransitionTimer()
    {
        yield return new WaitForSeconds(2);

       

            friendlyTurn = true;


    }

    


}

