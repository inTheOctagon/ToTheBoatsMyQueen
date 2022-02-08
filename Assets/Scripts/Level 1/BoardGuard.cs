using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoardGuard : MonoBehaviour
{

    [SerializeField] LayerMask boardMask;
    [SerializeField] LayerMask guardMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask attackMask;

    private RaycastHit boardHit;

    private bool moveBool = false;
    private bool longerTimer = false;

    private bool visualized = false;

    [SerializeField] GameObject longActionAreaIndicator;
    [SerializeField] GameObject shortActionAreaIndicator;
    [SerializeField] GameObject attackActionAreaIndicator;

    [SerializeField] Level1GameplayManager gameplayManager;
    [SerializeField] Level1SetupManager setupManager;

    Vector3[] indicatorRayDirections = new[] { Vector3.right, -Vector3.right, Vector3.forward, -Vector3.forward };





    public void MoveWithMechanism()
    {
        Ray guardRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        

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

            var roundedTargetPos = new Vector3(Mathf.Round(boardHit.collider.gameObject.transform.position.x), Mathf.Round(boardHit.collider.gameObject.transform.position.y), Mathf.Round(boardHit.collider.gameObject.transform.position.z));

            SetDestination(roundedTargetPos);
            
            

            
            //switch timer is adjusted according to target position
            if (Vector3.Distance(boardHit.collider.gameObject.transform.position, gameObject.transform.position) > 6.5f) longerTimer = true;
           
            if(longerTimer) StartCoroutine("SwitchStates", 3);
            else StartCoroutine("SwitchStates", 2);
            
            ResetTheIndicator();

            moveBool = false;
        }

        else if(
                Physics.Raycast(guardRay, out boardHit, 60, attackMask)
           && moveBool
           && ((Vector3.Distance(boardHit.collider.gameObject.transform.position, gameObject.transform.position) < 8.5f && Vector3.Distance(boardHit.collider.gameObject.transform.position, gameObject.transform.position) > 6.5f))
               )
        {
            
            
                StartCoroutine("AttackTheEnemy");
            
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
            //long indicators for enemies
            if (!Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 10, enemyMask))
            {

                if (i == 0 && gameObject.transform.position.x <= 6)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorForwardOffset, Quaternion.identity);


                }

                if (i == 1 && gameObject.transform.position.x >= -6)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorBackwardOffset, Quaternion.Euler(0, 180, 0));

                }

                if (i == 2 && gameObject.transform.position.z <= 2)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorUpwardOffset, Quaternion.Euler(0, 270, 0));

                }

                if (i == 3 && gameObject.transform.position.z >= -2)
                {
                    Instantiate(longActionAreaIndicator, guardPosition + longIndicatorDownwardOffset, Quaternion.Euler(0, 90, 0));

                }


            }
            //shorts indicators for enemies
            if (Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 10, enemyMask))
            {

                if (i == 0 && (Vector3.Distance(transform.position, enemyHit.collider.gameObject.transform.position) > 4))
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorForwardOffset, Quaternion.identity);


                }

                if (i == 1 && (Vector3.Distance(transform.position, enemyHit.collider.gameObject.transform.position) > 4))
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorBackwardOffset, Quaternion.Euler(0, 180, 0));

                }

                if (i == 2 && (Vector3.Distance(transform.position, enemyHit.collider.gameObject.transform.position) > 4))
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorUpwardOffset, Quaternion.Euler(0, 270, 0));

                }

                if (i == 3 && (Vector3.Distance(transform.position, enemyHit.collider.gameObject.transform.position) > 4))
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorDownwardOffset, Quaternion.Euler(0, 90, 0));

                }

            }

            //short indicators for edges
            if (!Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 4, enemyMask))
            {

                if (i == 0 && transform.position.x == 10)
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorForwardOffset, Quaternion.identity);


                }

                if (i == 1 && transform.position.x == -10)
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorBackwardOffset, Quaternion.Euler(0, 180, 0));

                }

                if (i == 2 && transform.position.z == 6)
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorUpwardOffset, Quaternion.Euler(0, 270, 0));

                }

                if (i == 3 && transform.position.z == -6)
                {
                    Instantiate(shortActionAreaIndicator, guardPosition + shortIndicatorDownwardOffset, Quaternion.Euler(0, 90, 0));

                }




            }


            //attack indicators for enemies
            if (Physics.Raycast(transform.position, indicatorRayDirections[i], out enemyHit, 4, enemyMask))
            {

                if (i == 0 && gameObject.transform.position.x != 10)
                {
                    Instantiate(attackActionAreaIndicator, guardPosition + longIndicatorForwardOffset, Quaternion.identity);


                }

                if (i == 1 && gameObject.transform.position.x != -10)
                {
                    Instantiate(attackActionAreaIndicator, guardPosition + longIndicatorBackwardOffset, Quaternion.Euler(0, 180, 0));

                }

                if (i == 2 && gameObject.transform.position.x != 6)
                {
                    Instantiate(attackActionAreaIndicator, guardPosition + longIndicatorUpwardOffset, Quaternion.Euler(0, 270, 0));

                }

                if (i == 3 && gameObject.transform.position.x != -6)
                {
                    Instantiate(attackActionAreaIndicator, guardPosition + longIndicatorDownwardOffset, Quaternion.Euler(0, 90, 0));




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

    public void SetDestination(Vector3 pos)
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(pos);
    }

    IEnumerator SwitchStates(float secs)
    {

        gameplayManager.friendlyTurn = false;
       
        yield return new WaitForSeconds(secs);

        gameplayManager.enemyTurn = true;


    }

    IEnumerator AttackTheEnemy()
    {
        gameplayManager.friendlyTurn = false;

        var boardHitPos = (boardHit.collider.transform.position);
        var transformedBoardHitPos = transform.TransformPoint(boardHit.collider.transform.position);

        Debug.Log(boardHitPos);

        ResetTheIndicator();

        transform.LookAt(boardHitPos);
        
        yield return new WaitForSeconds(0.5f);       
        Debug.Log("attack anim");
        yield return new WaitForSeconds(0.5f);

        RaycastHit enemyHit;
        
        Vector3 raycastDir = boardHitPos - transform.position;

        if (Physics.Raycast(transform.position,raycastDir, out enemyHit, 4))
        {
            enemyHit.collider.gameObject.GetComponent<BoardEnemy>().TakeDamage(1);
            
            
        }

        var transitionalOffset = 1.5f;
        //saðdaysa üstten
        if (boardHitPos.x > transform.position.x)
        {
            SetDestination(boardHitPos + new Vector3(0,0,transitionalOffset));
            
        }
        //soldaysa alttan
        else if (boardHitPos.x < transform.position.x && Mathf.Round(boardHitPos.z) == Mathf.Round(transform.position.z))
        {
            SetDestination(boardHitPos + new Vector3(0, 0, -transitionalOffset));
            
        }
        //üstteyse soldan
        else if (boardHitPos.z > transform.position.z && Mathf.Round(boardHitPos.x) == Mathf.Round(transform.position.x))
        {
            SetDestination(boardHitPos + new Vector3(transitionalOffset, 0, 0));
            
        }
        //alttaysa saðdan
        else if (boardHitPos.z < transform.position.z)
        {
            SetDestination(boardHitPos + new Vector3(-transitionalOffset, 0, 0));
            
        }
        yield return new WaitForSeconds(1.3f);

        boardHitPos = new Vector3(Mathf.Round(boardHitPos.x), Mathf.Round(boardHitPos.y), Mathf.Round(boardHitPos.z));

        SetDestination(boardHitPos);

        yield return new WaitForSeconds(1);

        setupManager.CheckForSetupTwo();

        if(setupManager.waveOneEnemyNumber != 0) gameplayManager.friendlyTurn = true;



    }

}
