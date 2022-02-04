using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGuard : MonoBehaviour
{
    [SerializeField] GameObject longActionAreaIndicator;
    [SerializeField] GameObject shortActionAreaIndicator;

    public void SetTheIndicator()
    {
        longActionAreaIndicator.SetActive(true);
        //shortActionAreaIndicator.SetActive(true);
    }
   
    public void ResetTheIndicator()
    {
        longActionAreaIndicator.SetActive(false);
    }

}
