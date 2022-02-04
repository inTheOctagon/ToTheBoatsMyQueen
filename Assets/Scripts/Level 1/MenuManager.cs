using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject level1Manager;
    [SerializeField] GameObject playButtonObject;

    public void PlayButton()
    {
        level1Manager.GetComponent<Level1Manager>().SetupStarter();
        playButtonObject.SetActive(false);
    }
    
}
