using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
     Written By: Goh Min Rui
     Description: For enemy in deep corner, I want to make sure player doesnt fight with 2 enemies on the right
*/
public class Enemy2Alt : MonoBehaviour
{
    public GameObject Enemy2First1;

    private void Start()
    {
        gameObject.GetComponent<Enemy2Main>().enabled = false;
    }

    private void Update()
    {
        if (Enemy2First1 == null)
        {
            gameObject.GetComponent<Enemy2Main>().enabled = true;
        }
    }
}

