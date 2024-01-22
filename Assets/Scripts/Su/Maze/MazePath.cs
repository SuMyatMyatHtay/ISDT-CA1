using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePath : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Maze Trigger : " +  other.gameObject.name); 
        if(other.gameObject.name == "Floor")
        {
            Debug.Log("Maze Trigger Yes : " + other.gameObject.name);
            meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
        }
    }
}
