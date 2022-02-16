using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGuard : MonoBehaviour
{
    [Header("Guard Variables")]
    [SerializeField] float sidewardRange;
    [SerializeField] float sidewardTime;
    [SerializeField] float forwardTime;
    private float forwardVelocity;
    [SerializeField] float forwardMaxVelocity;
    [Header("Shield Variables")]
    [SerializeField] GameObject shieldObject;
    [SerializeField] float rotatingTime;

    

    

    private bool ready = true;

    private void Start()
    {

        StartCoroutine(Accelerate());
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal") && ready)
        {
            StartCoroutine(ChangeLane());
            
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && ready)
        {
            StartCoroutine(RotateShield());
        }

        MoveForward();
    }

   
    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardVelocity * Time.deltaTime);
    }
    
    IEnumerator Accelerate()
    {
        float currentTime = Time.time;
        var currentVelocity = 0;

        while(Time.time < currentTime + forwardTime)
        {
            forwardVelocity = Mathf.Lerp(currentVelocity, forwardMaxVelocity, (Time.time - currentTime) / forwardTime);
            Debug.Log(forwardVelocity);
            yield return null;
        }
        
        forwardVelocity = forwardMaxVelocity;
    }

    IEnumerator ChangeLane()
    {
        ready = false;

        var inputValue = Input.GetAxisRaw("Horizontal");
        float currentPosX = transform.position.x;
        float targetPosX = currentPosX + sidewardRange * inputValue;
        Vector3 targetPos = new Vector3(targetPosX, transform.position.y, transform.position.z);
        
        
        if(targetPosX >= -sidewardRange && targetPosX <= sidewardRange)

        {
            float startTime = Time.time;

            while (Time.time < startTime + sidewardTime)
            {

                transform.position = new Vector3(Mathf.Lerp(currentPosX, targetPosX, (Time.time - startTime) / sidewardTime), transform.position.y,transform.position.z); 
                
                yield return null;
            }
            transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        }

        ready = true;
    }

    IEnumerator RotateShield()
    {
        ready = false;

        float startTime = Time.time;
        //shield pos variables
        float currentPosY = shieldObject.transform.position.y;
        float shieldPosOffset = 1.5f;
        //shield rotation variables
        Quaternion currentAngle = shieldObject.transform.rotation;
        Quaternion horizontalAngle = Quaternion.Euler(0, 0, 90);
        Quaternion verticalAngle = Quaternion.Euler(0, 0, 0);


        if(currentAngle.eulerAngles.z == 90)
        {
            float targetPosY = currentPosY + shieldPosOffset;

            while (Time.time < startTime + rotatingTime)
            {
                shieldObject.transform.position = new Vector3(shieldObject.transform.position.x, Mathf.Lerp(currentPosY, targetPosY, (Time.time - startTime) / rotatingTime), shieldObject.transform.position.z);
                shieldObject.transform.localRotation = Quaternion.Lerp(currentAngle, verticalAngle, (Time.time - startTime) / rotatingTime);
                yield return null;
            }
            shieldObject.transform.position = new Vector3(shieldObject.transform.position.x, targetPosY, shieldObject.transform.position.z);
            shieldObject.transform.rotation = verticalAngle;

            ready = true;

            Debug.Log("vertical");
        }
        else if(currentAngle.z == 0)
        {
            float targetPosY = currentPosY - shieldPosOffset;

            while (Time.time < startTime + rotatingTime)
            {
                shieldObject.transform.position = new Vector3(shieldObject.transform.position.x, Mathf.Lerp(currentPosY, targetPosY, (Time.time - startTime) / rotatingTime), shieldObject.transform.position.z);
                shieldObject.transform.localRotation = Quaternion.Lerp(currentAngle, horizontalAngle, (Time.time - startTime) / rotatingTime);
                yield return null;
            }
            shieldObject.transform.position = shieldObject.transform.position = new Vector3(shieldObject.transform.position.x, targetPosY, shieldObject.transform.position.z); ;
            shieldObject.transform.rotation = horizontalAngle;

            ready = true;

            Debug.Log("horiztonal");
        }
        
    }
    

}
