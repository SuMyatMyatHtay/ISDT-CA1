using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NavKeypad;
using UnityEngine.UI; 

public class MainExit : MonoBehaviour
{
    public GameObject MainExitDoor;
    public Animator _aniMainDoor;

    public GameObject JolleenGO;
    public Button ExitButton; 
    private bool permissionAllow;

    public GameObject TurrentEntryGO; 
    // Start is called before the first frame update
    void Start()
    {
        _aniMainDoor = MainExitDoor.GetComponent<Animator>();
        _aniMainDoor.SetBool("character_nearby", false);
        permissionAllow = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(permissionAllow == true)
        {
            if (other.gameObject.tag == "Player")
            {
                _aniMainDoor.SetBool("character_nearby", true);
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (permissionAllow == true)
        {
            if (other.gameObject.tag == "Player")
            {
                _aniMainDoor.SetBool("character_nearby", false);
            }
                
        }
    }

    public void ExitButtonIsClicked()
    {
        permissionAllow = true;
        if(JolleenGO != null)
        {
            if(JolleenGO.GetComponent<Jolleen>().inExitCell == false && JolleenGO.GetComponent<Jolleen>()._state != Jolleen.Girl_state.cry && TurrentEntryGO.GetComponent<TurrentEntry>().JolleenIsIn == true)
            {
                JolleenGO.GetComponent<Jolleen>()._nav.Warp(JolleenGO.GetComponent<Jolleen>().JolleenExternalRef.transform.position);
                
            }
        }
        ExitButton.interactable = false; 
        
        //_aniMainDoor.SetBool("character_nearby", true);
    }
}
