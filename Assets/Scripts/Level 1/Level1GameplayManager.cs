using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level1GameplayManager : MonoBehaviour
{
    [Header("BOARD VARIABLES")]



    [Header("Actor Variables")]
    [SerializeField] GameObject guardObject;

    [SerializeField] LayerMask boardMask;
    [SerializeField] LayerMask guardMask;
    [SerializeField] LayerMask enemyMask;

    private bool moveBool;
    private void Update()
    {
        Debug.DrawRay(guardObject.transform.position, Vector3.right * 10, Color.green);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {


            Ray guardRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit guardHit;
            RaycastHit boardHit;



            if (Physics.Raycast(guardRay, out guardHit, 60, guardMask))
            {
                Debug.Log("the guard");

                Ray indicatorRay = new Ray(guardObject.transform.position, transform.right);
                RaycastHit enemyHit;

                if (Physics.Raycast(indicatorRay, out enemyHit, 4, enemyMask))
                {
                    guardObject.GetComponent<BoardGuard>().SetTheLongIndicator();
                    moveBool = true;
                    
                }
                else if (Physics.Raycast(indicatorRay, out enemyHit, 10, enemyMask))
                {
                    guardObject.GetComponent<BoardGuard>().SetTheShortIndicator();
                    moveBool = true;
                    
                }
                else if (!Physics.Raycast(indicatorRay, out enemyHit, 10, enemyMask))
                {
                    guardObject.GetComponent<BoardGuard>().SetTheLongIndicator();
                    moveBool = true;
                    
                    
                }

            }

            else if (Physics.Raycast(guardRay, out boardHit, 60, boardMask)
               && moveBool
               && boardHit.collider.gameObject.transform.position.z == guardObject.transform.position.z
               && guardObject.transform.position.x + 10 >= boardHit.collider.gameObject.transform.position.x
               && guardObject.transform.position.x < boardHit.collider.gameObject.transform.position.x
                   )
            {
                Debug.Log("what?");
                guardObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(boardHit.collider.gameObject.transform.position.x, guardObject.transform.position.y, guardObject.transform.position.z));
                guardObject.GetComponent<BoardGuard>().ResetTheIndicator();
                moveBool = false;
            }

            else
            {
                Debug.Log("no the guard");
                guardObject.GetComponent<BoardGuard>().ResetTheIndicator();
                moveBool = false;
            }








        }

    }
}

