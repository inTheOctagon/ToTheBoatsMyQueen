using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoardEnemy : MonoBehaviour
{
    private NavMeshAgent enemyNavMeshComp;

    [SerializeField] float healthValue;

    private void Start()
    {
       enemyNavMeshComp = GetComponent<NavMeshAgent>();
    }

    public void MoveEnemy()
    {
        var newPos = new Vector3(this.gameObject.transform.position.x -4, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        enemyNavMeshComp.SetDestination(newPos);
    }

    public void TakeDamage(float damage)
    {
        healthValue = healthValue - damage;
        if (healthValue == 0)
        {
            //play death anim and disable navmeshagentcomp but for now destroy
            Destroy(this.gameObject);
        }
    }

}
