using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level1GameplayManager : MonoBehaviour
{
    [Header("BOARD VARIABLES")]



    [Header("Actor Variables")]
    [SerializeField] GameObject guardObject;
    private BoardGuard guardScript;



    private void Start()
    {
        guardScript = guardObject.GetComponent<BoardGuard>();
    }
    private void Update()
    {
        Debug.DrawRay(guardObject.transform.position, Vector3.right * 10, Color.green);
        Debug.DrawRay(guardObject.transform.position, -Vector3.right * 10, Color.green);
        Debug.DrawRay(guardObject.transform.position, Vector3.forward * 10, Color.green);
        Debug.DrawRay(guardObject.transform.position, -Vector3.forward * 10, Color.green);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            guardScript.MoveWithMechanism();
           

        }

    }
}

