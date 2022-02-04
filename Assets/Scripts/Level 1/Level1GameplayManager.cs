using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level1GameplayManager : MonoBehaviour
{
    [Header("BOARD VARIABLES")]

    

    [Header("Actor Variables")]
    [SerializeField] GameObject guardObject;
    [SerializeField] LayerMask guardMask;
    [SerializeField] LayerMask boardMask;
    private bool moveBool;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            
            if (Physics.Raycast(ray, out hit, 60, guardMask))
            {
                Debug.Log("the guard");
                guardObject.GetComponent<BoardGuard>().SetTheIndicator();
                moveBool = true;
            }
            else if(Physics.Raycast(ray, out hit, 60, boardMask) 
               && moveBool 
               && hit.collider.gameObject.transform.position.z == guardObject.transform.position.z  
               && guardObject.transform.position.x + 10 >= hit.collider.gameObject.transform.position.x
               && guardObject.transform.position.x < hit.collider.gameObject.transform.position.x
                   )
            {
                guardObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(hit.collider.gameObject.transform.position.x, guardObject.transform.position.y, guardObject.transform.position.z));
                guardObject.GetComponent<BoardGuard>().ResetTheIndicator();
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
