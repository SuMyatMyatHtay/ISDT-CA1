using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //attach XR here
    public GameObject MenuXR;

    private void Start()
    {
        if (GameManager.Instance._previousLevelName != "SplashDesign")
        {
            MenuXR.SetActive(true);
        }
        else
        {
            MenuXR.SetActive(false);
        }

    }


}