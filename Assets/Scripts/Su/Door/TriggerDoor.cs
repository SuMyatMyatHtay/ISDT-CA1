using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public GameObject Door;
    public Animator _aniDoor; 
    // Start is called before the first frame update
    void Start()
    {
        _aniDoor = Door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
            _aniDoor.SetBool("character_nearby", true); 
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _aniDoor.SetBool("character_nearby", false);
        }
    }
}
