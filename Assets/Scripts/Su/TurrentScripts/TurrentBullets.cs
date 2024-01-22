using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentBullets : MonoBehaviour
{
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
        
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Umm shld be collider");
            other.gameObject.GetComponent<PlayerV>().Damage(10f); 
        }
        if (other.gameObject.name.Contains("WallC"))
        {
            Destroy(gameObject); 
        }
    }
}
