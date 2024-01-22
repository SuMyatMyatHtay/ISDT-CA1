using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jolleen_ani : MonoBehaviour
{
    public Jolleen _code; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpDone()
    {
        _code.JumpDone(); 
    }

    public void Attack()
    {
        _code.Attack(); 
    }

    public void AttackDone()
    {
        _code.AttackDone(); 
    }
}
