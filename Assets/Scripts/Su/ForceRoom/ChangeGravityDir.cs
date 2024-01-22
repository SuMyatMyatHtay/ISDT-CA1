using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravityDir : MonoBehaviour
{
    private ConstantForce cForce;
    private Vector3 forceDirection;
    public GameObject ScriptGO;
    // Start is called before the first frame update
    
    void Start()
    {
        cForce = GetComponent<ConstantForce>();
        forceDirection = new Vector3(0, 10, 0);
        cForce.force = forceDirection * ScriptGO.GetComponent<GameSceneScript>().GravityDir;
        //ScriptGO.GetComponent<GameSceneScript>().Patrol_list
    }

    // Update is called once per frame
    void Update()
    {
        cForce.force = forceDirection * ScriptGO.GetComponent<GameSceneScript>().GravityDir;
        
    }

    public void GravityCharge()
    {
        cForce.force = forceDirection * ScriptGO.GetComponent<GameSceneScript>().GravityDir;
    }
}
