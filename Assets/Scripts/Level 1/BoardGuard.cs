using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoardGuard : MonoBehaviour
{



    [SerializeField] LayerMask boardMask;
    [SerializeField] LayerMask guardMask;
    [SerializeField] LayerMask enemyMask;

    private bool moveBool = false;
    private bool longerTimer = false;

    private bool visualized = false;

    [SerializeField] GameObject longActionAreaIndicator;
    [SerializeField] GameObject shortActionAreaIndicator;

    [SerializeField] Level1GameplayManager gameplayManager;





    Vector3[] indicatorRayDirections = new[] { Vector3.right, -Vector3.right, Vector3.forward, -Vector3.forward };





    public void MoveWithMechanism()
    {
        Ray guardRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit boardHit;



        if (Physics.Raycast(guardRay, 60, guardMask))
        {
            
            if (!visualized) VisualizeIndicators();
            moveBool = true;

        }

        else if (
           Physics.Raycast(guardRay, out boardHit, 60, boardMask)
           && moveBool
           && (Vector3.Distance(boardHit.collider.gameObject.transform.position, gameObject.transform.position) < 5 
           || (Vector3.Distance(boardHit.collider.gameObject.transform.position, gameObject.transform.position) < 8.5f && Vector3.Distance(boardHit.collider.gameObject.transform.position, gameObject.transform.position) > 6.5f))

                )
        {

            Debug.Log(Vector3.Distance(transform.position, boardHit.collider.transform.position));
            gameObject.GetComponent<NavMeshAgent>().SetDestination(boardHit.collider.gameObject.transform.position);
            //switch timer is adjusted according to target position
            if (Vector3.Distance(boardHit.collider.gameObject.transform.position, gameObject.transform.position) > 6.5f) longerTimer = true;
            //coroutine that switches turn bools
            StartCoroutine("roundTransitionTimer");
            ResetTheIndicator();

            moveBool = false;
        }

        else
        {
           
            
            ResetTheIndicator();

            moveBool = false;
            visualized = false;
        }
    }

    private void VisualizeIndicators()
    {

        visualized = true;
        Vector3 guardPosition = transform.position;

        RaycastHit enemyHit;

        Vector3 longIndicatorForwardOffset = new Vector3(5, -1.97f, 0);
        Vector3 shortIndicatorForwardOffset = new Vector3(3.1f, -1.97f, 0);
        Vector3 longIndicatorBackwardOffset = new Vector3(-5, -1.97f, 0);
        Vector3 shortIndicatorBackwardOffset = new Vector3(-3.1f, -1.97f, 0);
        Vector3 longIndicatorUpwardOffset = new Vector3(0, -1.97f, 5);
        Vector3 shortIndicatorUpwardOffset = new Vector3(0, -1.97f, 3.1f);
        Vector3 longIndicatorDownwardOffset = new Vector3(0, -1.97f, -5f);
        Vector3 shortIndicatorDownwardOffset = new Vector3(0, -1.97f, -3.1f);


        for (int i = 0; i < 4; i++)
        {
            //long indicators
            if (!Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 10, enemyMask))
            {

                if (i == 0 && gameObject.transform.position.x <= 6)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorForwardOffset, Quaternion.identity);


                }

                if (i == 1 && gameObject.transform.position.x >= -6)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorBackwardOffset, Quaternion.identity);

                }

                if (i == 2 && gameObject.transform.position.z <= 2)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorUpwardOffset, Quaternion.Euler(0, 90, 0));

                }

                if (i == 3 && gameObject.transform.position.z >= -2)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorDownwardOffset, Quaternion.Euler(0, 270, 0));

                }


            }
            //short indicators
            if (Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 10, enemyMask))
            {

                if (i == 0 && (Vector3.Distance(transform.position, enemyHit.collider.gameObject.transform.position) > 4))
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorForwardOffset, Quaternion.identity);


                }

                if (i == 1 && (Vector3.Distance(transform.position, enemyHit.collider.gameObject.transform.position) > 4))
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorBackwardOffset, Quaternion.identity);

                }

                if (i == 2 && (Vector3.Distance(transform.position, enemyHit.collider.gameObject.transform.position) > 4))
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorUpwardOffset, Quaternion.Euler(0, 90, 0));

                }

                if (i == 3 && (Vector3.Distance(transform.position, enemyHit.collider.gameObject.transform.position) > 4))
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorDownwardOffset, Quaternion.Euler(0, 270, 0));

                }

            }

            //short indicator for edges
            if (!Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 4, enemyMask))
            {

                if (i == 0 && transform.position.x == 10)
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorForwardOffset, Quaternion.identity);


                }

                if (i == 1 && transform.position.x == -10)
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorBackwardOffset, Quaternion.identity);

                }

                if (i == 2 && transform.position.z == 6)
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorUpwardOffset, Quaternion.Euler(0, 90, 0));

                }

                if (i == 3 && transform.position.z == -6)
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorDownwardOffset, Quaternion.Euler(0, 270, 0));

                }




            }


            //attack indicators
            if (Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 4, enemyMask))
            {

                if (i == 0 && gameObject.transform.position.x != -10)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorForwardOffset, Quaternion.identity);


                }

                if (i == 1 && gameObject.transform.position.x != 10)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorBackwardOffset, Quaternion.identity);

                }

                if (i == 2 && gameObject.transform.position.x != 6)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorUpwardOffset, Quaternion.Euler(0, 90, 0));

                }

                if (i == 3 && gameObject.transform.position.x != -6)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorDownwardOffset, Quaternion.Euler(0, 270, 0));




                }
            }

        }
    }
    public void ResetTheIndicator()
    {

        GameObject[] allIndicators = GameObject.FindGameObjectsWithTag("Indicator");

        if (allIndicators != null)
        {

            foreach (GameObject indicator in allIndicators)
            {
                Destroy(indicator.gameObject);
            }
        }

        visualized = false;




    }

    IEnumerator roundTransitionTimer()
    {
        gameplayManager.friendlyTurn = false;

        if (longerTimer)
        {
            Debug.Log("long");

            yield return new WaitForSeconds(3);
        }
        else
        {
            Debug.Log("short");
            yield return new WaitForSeconds(2);
        }


        gameplayManager.enemyTurn = true;
    }
}
