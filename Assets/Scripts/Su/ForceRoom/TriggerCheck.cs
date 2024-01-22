using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public GameObject ScriptGO; 
    public GameObject CubeGO;
    public GameObject HeatPadGO;
    public int HeatPadInt1; 
    public int HeatPadInt2;
    // Start is called before the first frame update
    private void Awake()
    {
        HeatPadInt1 = UnityEngine.Random.Range(1, 11); // Generates a random number between 1 and 100
        do
        {
            HeatPadInt2 = UnityEngine.Random.Range(1, 11);
        } while (HeatPadInt2 == HeatPadInt1);
        // Print the generated random number
        Debug.Log("Random Number 1 : " + HeatPadInt1);
        Debug.Log("Random Number 2 : " + HeatPadInt2);

        
    }
    void Start()
    {
        CheckingHeatCube(); 
        HeatPadGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnTriggerEnter(Collider col)
    {
        CubeGO = col.gameObject;
        Debug.Log(CubeGO.name); 
        
        if (CubeGO.name.Contains("MysteryCube"))
        {
            if(CubeGO.name == "MysteryCube" + HeatPadInt1)
            {
                HeatPad.transform.position = CubeGO.transform.position;
                Debug.Log("Bulls Eye. Pad is discovered"); 
            }
            else
            {
                Debug.Log("it is still the cube so melt it down"); 
            }
            
        }
        else
        {
            Debug.Log("it is not the cube "); 
        }
    }
    */

    public void CheckingHeatCube()
    {
        GameObject[] heatCubePads = GameObject.FindGameObjectsWithTag("HeatPadCube");

        // Get the number of GameObjects with the "HeatCubePad" tag
        int numberOfHeatCubePads = heatCubePads.Length;
        // Print the number of GameObjects
        Debug.Log("Number of HeatPadCube GameObjects in the scene: " + numberOfHeatCubePads);

        
        if(numberOfHeatCubePads == HeatPadInt1 || numberOfHeatCubePads == HeatPadInt2)
        {
            //Debug.Log("Hit bulls eye");
            ScriptGO.GetComponent<GameSceneScript>().GravityDir = ScriptGO.GetComponent<GameSceneScript>().GravityDir * (-1);
        }
        else
        {
            //Debug.Log("Not Hit bulls eye");
        }

    }
}
