using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCheck : MonoBehaviour
{
    public GameObject fractured;
    public float breakForce; 
    //public Animator _aniAC; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        BreakTheThing(); 
    }
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name + " : OnCollisonEnter trigger");
        if (col.gameObject.name.Contains("Vampire "))
        {
            if(col.gameObject.GetComponent<EnemyV>() != null)
            {
                col.gameObject.GetComponent<EnemyV>().Health_amt = 0;
                //_aniAC = col.gameObject.GetComponent<Animator>();
                //_aniAC.SetTrigger("Death");
            }
            
        }

        if (col.gameObject.name.Contains("Jolleen"))
        {
            col.gameObject.GetComponent<PlayerV>().Health_amt = 0; 
        }

        if (col.gameObject.name.Contains("TurrentTargetPlayer"))
        {
            col.gameObject.GetComponent<PlayerV>().Health_amt -= 200 ;
        }
    }

    public void BreakTheThing()
    {
        GameObject frac = Instantiate(fractured, transform.position, transform.rotation); 

        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force); 
        }
        Destroy(gameObject); 
    }

}
