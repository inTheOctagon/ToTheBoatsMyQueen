using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject level1SetupManager;
    [SerializeField] GameObject playButtonObject;

    public void PlayButton()
    {
        level1SetupManager.GetComponent<Level1SetupManager>().SetupStarter();
        playButtonObject.SetActive(false);
    }

    
    
}
