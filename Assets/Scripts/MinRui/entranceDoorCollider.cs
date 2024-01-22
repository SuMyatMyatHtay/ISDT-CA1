using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entranceDoorCollider : MonoBehaviour
{
    /*
     Written By: Min Rui
     Description: Controlling the in and out motion through
     collider of the entrance door
     */

    [SerializeField] private Transform doorEndPoint;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject XROrigin;
    private void OnTriggerEnter(Collider enteredObject)
    {
        if (enteredObject.tag == "Player" || enteredObject.tag == "MainCamera")
        {
            door.GetComponent<Animator>().SetTrigger("DoorOpen");
        }
    }

    private void OnTriggerExit(Collider exitedObject)
    {

        if (exitedObject.tag == "Player" || exitedObject.tag == "MainCamera")
        {
            door.GetComponent<Animator>().SetTrigger("DoorClose");
        }
    }

    private void Update()
    {
        //Debug.Log(MainCamera.transform.position.y + "MainCamera");
        //Debug.Log(Vector3.Distance(MainCamera.transform.position, doorEndPoint.position));
        if (XROrigin != null)
        {
            XROrigin = GameObject.FindGameObjectWithTag("Player");

            //Debug.Log(Vector3.Distance(XROrigin.transform.position, doorEndPoint.position));
            //Player will not be allowed to get out of th maze once they enter
            if (Vector3.Distance(XROrigin.transform.position, doorEndPoint.position) < 2)
            {
                Debug.Log(Vector3.Distance(XROrigin.transform.position, doorEndPoint.position));
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }

}
