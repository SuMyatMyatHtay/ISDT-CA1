using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeExitCollider : MonoBehaviour
{
    /*
     Written By: Min Rui
     Description: False isTrigger for unused/exited doors; Player cannot reenter the maze
     */

    [SerializeField] private List<BoxCollider> DoorColliders =  new List<BoxCollider>();


    private void Update()
    {
        for(int i =0; i< DoorColliders.Count; i++)
        {

           
            if (DoorColliders[i].isTrigger == false)
            {
                for (int x = 0; x < DoorColliders.Count; x++)
                {
                    if (x != i)
                    {
                        DoorColliders[i].isTrigger = false;
                    }
                }
            }
        }
    }

}
