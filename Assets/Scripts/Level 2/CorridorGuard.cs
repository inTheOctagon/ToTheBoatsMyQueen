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
        if(Input.GetButtonDown("Horizontal") && ready)
        {
            var inputValue = Input.GetAxisRaw("Horizontal");
            var horizontalPos = transform.position.x;

            ready = false;

            StartCoroutine(MoveObject(inputValue, horizontalPos));

        }

    }
   
    IEnumerator MoveObject(float inputValue, float horizontalPos)
    {
        
        
        float startTime = Time.time;

        Vector3 startPos = transform.position;
            
        float targetPosX = Mathf.Clamp(startPos.x + (moveRange * inputValue), -5, 5);

        Vector3 targetPos = new Vector3(targetPosX, startPos.y, startPos.z);
        
        if(targetPosX == -5 || targetPosX == 0 || targetPosX == 5)
            while (Time.time < startTime + moveTime)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, (Time.time - startTime) / moveTime);
                yield return null;
            }
            transform.position = targetPos;

        ready = true;
        
    }
     

}
