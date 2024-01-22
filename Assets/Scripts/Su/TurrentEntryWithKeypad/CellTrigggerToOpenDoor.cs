using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CellTrigggerToOpenDoor : MonoBehaviour
{
    [Header("Doors for transition")]
    public GameObject theDoorGO;
    public Animator _aniDoor;

    public Button BreakTheCellButton;
    public GameObject JolleenGO; 

    // Start is called before the first frame update
    void Start()
    {
        _aniDoor = theDoorGO.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Jolleen"))
        {
            JolleenGO.GetComponent<Jolleen>().inExitCell = true; 
            _aniDoor.SetBool("character_nearby", true);
            //BreakTheCellButton.interactable = true; 
        }
        
            
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Contains("Jolleen"))
        {
            JolleenGO.GetComponent<Jolleen>().inExitCell = false; 
            _aniDoor.SetBool("character_nearby", false);
            //BreakTheCellButton.interactable = false;
        }
    }

}
