using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider enteredObject)
    {
        if (enteredObject.tag == "Player" || enteredObject.tag == "MainCamera")
        {
        }
    }
}
