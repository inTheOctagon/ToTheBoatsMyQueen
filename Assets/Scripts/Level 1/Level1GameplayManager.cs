using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1GameplayManager : MonoBehaviour
{
    [Header("BOARD VARIABLES")]

    

    [Header("Actor Variables")]
    [SerializeField] GameObject guardObject;
    [SerializeField] LayerMask guardMask;
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
            }
            else
            {
                Debug.Log("no the guard");
                guardObject.GetComponent<BoardGuard>().ResetTheIndicator();
            }



        }
        
    }
}
