using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoardEnemy : MonoBehaviour
{
    private NavMeshAgent enemyNavMeshComp;

    private void Start()
    {
       enemyNavMeshComp = GetComponent<NavMeshAgent>();
    }

    public void MoveEnemy()
    {
        var newPos = new Vector3(this.gameObject.transform.position.x -4, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        enemyNavMeshComp.SetDestination(newPos);
    }

}
