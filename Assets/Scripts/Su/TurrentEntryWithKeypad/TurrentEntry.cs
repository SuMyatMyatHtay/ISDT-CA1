using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TurrentEntry : MonoBehaviour
{
    [Header("Keypad Keys")]
    public GameObject EnterKeyVisual;
    public GameObject EnterKeyButton;

    [Header("Toggles Players'")]
    public Toggle ToggleAsuna;
    public Toggle ToggleJolleen;

    [Header("Doors for transition")]
    public GameObject theFirstDoorGO;
    public Animator _aniFirstDoor;
    public GameObject theSecondDoorGO;
    public Animator _aniSecondDoor;
    public GameObject theThirdDoorGO;
    public Animator _aniThirdDoor;

    [Header("Jolleen Related Game Objects")]
    public GameObject OrgPlayerFollow; 
    public GameObject JolleenGO;
    public GameObject JolleenFollowGO;
    public GameObject JolleenFollowGO1;
    public GameObject JolleenFollowGO2;
    public GameObject JolleenLeftGO; 

    [Header("Bool Checks")]
    public bool CanStartCheck; 
    public bool AsunaIsIn;
    public bool JolleenIsIn;

    private bool tempCheck;
    public bool AsunaFunctionCheck;
    public bool firstDoorColliderCheck; 

    [Header("Door Colliders")]
    public GameObject MidDoorCollider;
    public GameObject FirstDoorCollider;

    public Button BreakTheCellButton; 

    private void Awake()
    {
        MidDoorCollider.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        tempCheck = false;
        AsunaFunctionCheck = false; 

        ToggleAsuna.interactable = true;
        ToggleJolleen.interactable = false; 

        EnterKeyButton.SetActive(false);
        EnterKeyVisual.SetActive(false);

        _aniFirstDoor = theFirstDoorGO.GetComponent<Animator>();
        _aniSecondDoor = theSecondDoorGO.GetComponent<Animator>();
        _aniThirdDoor = theThirdDoorGO.GetComponent<Animator>(); 
        AsunaIsIn = false; 
        JolleenIsIn = false;
        CanStartCheck = false;

        firstDoorColliderCheck = false;
        BreakTheCellButton.interactable = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if(JolleenGO != null)
        {
            ToggleChecking();

            if (JolleenGO.GetComponent<Jolleen>()._state == Jolleen.Girl_state.cry)
            {
                ToggleJolleen.interactable = false;
            }
            else
            {
                ToggleJolleen.interactable = true;
            }

            if (CanStartCheck)
            {
                if (ToggleJolleen.isOn == true)
                {
                    GoJolleenFP();
                }
                else
                {
                    JolleenGO.GetComponent<Jolleen>().Player_obj = JolleenLeftGO;
                    if(JolleenGO.GetComponent<Jolleen>()._state != Jolleen.Girl_state.cry)
                    {
                        JolleenGO.GetComponent<Jolleen>()._state = Jolleen.Girl_state.done;  
                    }
                }

                FirstDoorClose();
            }
        }
        

    }

    public void ToggleChecking()
    {
        if (ToggleJolleen.isOn || ToggleAsuna.isOn)
        {
            EnterKeyButton.SetActive(true);
            EnterKeyVisual.SetActive(true);
        }
        else
        {
            EnterKeyButton.SetActive(false);
            EnterKeyVisual.SetActive(false);
        }
    }

    /*
    public void AsunaEntryCheck()
    {
        if (!ToggleAsuna.isOn)
        {
            Debug.Log("this is Asuna Entry Check"); 
        }
    }

    public void AsunaEntryExit()
    {
        if (!ToggleAsuna.isOn)
        {
            Debug.Log("this is Asuna Entry Exit");
        }
    }
    */

    public void GoJolleenFP()
    {
        if(tempCheck == false && JolleenGO != null)
        {
            Debug.Log("JolleenIsIn timecheck");
            //JolleenGO.transform.position = JolleenFollowGO.transform.position;
            JolleenGO.GetComponent<Jolleen>().Player_obj = JolleenFollowGO;
            JolleenGO.GetComponent<Jolleen>()._state = Jolleen.Girl_state.turrentEnter;
            /*
            
            JolleenGO.GetComponent<Jolleen>().Player_obj = JolleenFollowGO; 
            
            Tween.Delay(duration: 3f, () =>
            {
                JolleenFollowGO.transform.position = JolleenFollowGO1.transform.position;
            });
            */


        }
        tempCheck = true; 

    }
    public void FirstDoorClose()
    {
        
        if (AsunaIsIn == ToggleAsuna.isOn && JolleenIsIn == ToggleJolleen.isOn)
        {
            if(ToggleAsuna.isOn == firstDoorColliderCheck)
            {
                MidDoorCollider.SetActive(true); 
                _aniFirstDoor.SetBool("character_nearby", false);
                _aniSecondDoor.SetBool("character_nearby", true);
                _aniThirdDoor.SetBool("character_nearby", true);
                Tween.Delay(duration: 2f, () =>
                {
                    JolleenGO.GetComponent<Jolleen>().Player_obj = JolleenFollowGO1;

                });

                /*
                Tween.Delay(duration: 8f, () =>
                {

                    JolleenFollowGO1.transform.position = JolleenFollowGO2.transform.position;
                    _aniSecondDoor.SetBool("character_nearby", false);
                    _aniThirdDoor.SetBool("character_nearby", false);
                    gameObject.SetActive(false); 

                });
                */
            }
            

        }
       
        
    }

    public void JolleenToCell()
    {
        JolleenFollowGO1.transform.position = JolleenFollowGO2.transform.position;
        _aniSecondDoor.SetBool("character_nearby", false);
        _aniThirdDoor.SetBool("character_nearby", false);
        //gameObject.SetActive(false);
    }
    public void DisableToggles()
    {
        ToggleAsuna.interactable = false; 
        ToggleJolleen.interactable =false;        
    }

    public void BreakTheCell()
    {
        if (JolleenIsIn)
        {
            JolleenGO.GetComponent<Jolleen>().Player_obj = OrgPlayerFollow;
            //BreakTheCellButton.interactable = false;
            JolleenGO.GetComponent<Jolleen>()._state = Jolleen.Girl_state.chase;
            Debug.Log("BreakTheCell");
            BreakTheCellButton.interactable = false;
        }
        else
        {
            //BreakTheCellButton.interactable = false;
        }
        
    }
}
