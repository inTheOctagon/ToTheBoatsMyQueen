using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGuard : MonoBehaviour
{
    [SerializeField] GameObject longActionAreaIndicator;
    [SerializeField] GameObject shortActionAreaIndicator;

    public void SetTheLongIndicator()
    {
        longActionAreaIndicator.SetActive(true);
    }
   
    public void SetTheShortIndicator()
    {
        shortActionAreaIndicator.SetActive(true);
    }

    public void ResetTheIndicator()
    {
        longActionAreaIndicator.SetActive(false);
        shortActionAreaIndicator.SetActive(false);

    }

}
