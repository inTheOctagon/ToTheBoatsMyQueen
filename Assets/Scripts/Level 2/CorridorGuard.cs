using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGuard : MonoBehaviour
{
    [SerializeField] float moveRange;
    [SerializeField] float moveTime;

    private bool ready = true;

    private void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            if(ready) StartCoroutine(ChangeLane());
        }

    }
   
    IEnumerator ChangeLane()
    {
        ready = false;

        var inputValue = Input.GetAxisRaw("Horizontal");
        Vector3 startPos = transform.position;
        float targetPosX = startPos.x + moveRange * inputValue;
        Vector3 targetPos = new Vector3(targetPosX, startPos.y, startPos.z);  
        
        if(targetPosX >= -moveRange && targetPosX <= moveRange)

        {
            float startTime = Time.time;

            while (Time.time < startTime + moveTime)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, (Time.time - startTime) / moveTime);
                yield return null;
            }
            transform.position = targetPos;
        }

        ready = true;
    }

}
