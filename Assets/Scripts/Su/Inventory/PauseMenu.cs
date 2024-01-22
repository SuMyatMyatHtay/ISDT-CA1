using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public InputActionProperty Left_menu;

    [Header("Pause Screen")]
    public GameObject PauseScreenGO;
    public bool isOpen;


    // Start is called before the first frame update
    void Start()
    {
        PauseScreenGO.SetActive(false);
        isOpen = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Left_menu.action.WasPerformedThisFrame())
        {
            print("left menu key is pressed");
            if (isOpen == true)
            {
                PauseScreenGO.SetActive(false);
                isOpen = false;
                Time.timeScale = 1;
                
            }
            else
            {
                PauseScreenGO.SetActive(true);
                isOpen = true;
                Time.timeScale = 0;
            }
        }
    }
}
