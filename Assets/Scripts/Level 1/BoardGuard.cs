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

    private bool visualized = false;

    [SerializeField] GameObject longActionAreaIndicator;
    [SerializeField] GameObject shortActionAreaIndicator;





    Vector3[] indicatorRayDirections = new[] { Vector3.right , -Vector3.right, Vector3.forward, -Vector3.forward };





    public void MoveWithMechanism()
    {
        Ray guardRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit boardHit;



        if (Physics.Raycast(guardRay, 60, guardMask))
        {
            Debug.Log("the guard");
            if (!visualized) VisualizeIndicators();
            moveBool = true;

        }

        else if (
           Physics.Raycast(guardRay, out boardHit, 60, boardMask)
           && moveBool
           //&& boardHit.collider.gameObject.transform.position.x >= -14 && boardHit.collider.gameObject.transform.position.x <= 14
           //&& boardHit.collider.gameObject.transform.position.z >= -10 && boardHit.collider.gameObject.transform.position.z <= 10
           && Vector3.Distance(boardHit.collider.gameObject.transform.position, gameObject.transform.position) <= 6
                )
        {

            gameObject.GetComponent<NavMeshAgent>().SetDestination(boardHit.collider.gameObject.transform.position);
            ResetTheIndicator();

            moveBool = false;
        }

        else
        {
            Debug.Log("no the guard");
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

        Vector3 indicatorForwardOffset = new Vector3(5, -1.97f, 0);
        Vector3 indicatorBackwardOffset = new Vector3(-5, -1.97f, 0);
        Vector3 indicatorUpwardOffset = new Vector3(0, -1.97f, 5);
        Vector3 indicatorDownwardOffset = new Vector3(0, -1.97f, -5);
        

        for (int i = 0; i < 4; i++)
        {
            if (!Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 10, enemyMask))
            {
                if(i == 0 && gameObject.transform.position.x <= 6)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + indicatorForwardOffset, Quaternion.identity);
                    Debug.Log(0);
                    
                }
                
                if(i == 1 && gameObject.transform.position.x >= -6)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + indicatorBackwardOffset, Quaternion.identity);
                    Debug.Log(1);
                }
                
                if(i == 2 && gameObject.transform.position.z <= 2)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + indicatorUpwardOffset, Quaternion.Euler(0, 90, 0));
                    Debug.Log(2);
                }
                
                if(i == 3 && gameObject.transform.position.z >= -2)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + indicatorDownwardOffset, Quaternion.Euler(0, 270, 0));
                    Debug.Log(3);
                }
                

            }

            
        }

        


        //if (Physics.Raycast(indicatorRayForward, out enemyHit, 4, enemyMask))
        //{
        //    longActionAreaIndicator.SetActive(true);
        //    moveBool = true;

        //}

        //if (Physics.Raycast(indicatorRayForward, out enemyHit, 10, enemyMask))
        //{
        //    Instantiate(shortActionAreaIndicator, guardPosition + indicatorOffset, Quaternion.identity);
        //    moveBool = true;

        //}
        //if (!Physics.Raycast(indicatorRayForward, out enemyHit, 10, enemyMask))
        //{
        //    Instantiate(longActionAreaIndicator, guardPosition + indicatorOffset , Quaternion.identity);

        //    moveBool = true;


        //}



        //}

        //if (!Physics.Raycast(indicatorRayBackward, out enemyHit, 10, enemyMask))
        //{
        //    Instantiate(longActionAreaIndicator, guardPosition + indicatorOffset, Quaternion.identity);

        //    moveBool = true;


        //}

        //if (!Physics.Raycast(indicatorRayBackward, out enemyHit, 10, enemyMask))
        //{
        //    Instantiate(longActionAreaIndicator, guardPosition + indicatorOffset, Quaternion.identity);

        //    moveBool = true;


        //}




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
}
