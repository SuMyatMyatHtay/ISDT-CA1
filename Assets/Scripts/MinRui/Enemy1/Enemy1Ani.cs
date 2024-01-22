using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Ani : MonoBehaviour
{
    [SerializeField] private Enemy1Main Enemy1Main;
    // Start is called before the first frame update
   public void afterHurt()
    {
        Enemy1Main.hurtDone();
    }
}
