using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("GunBullet : " + col.gameObject.name);
        GameObject collidedObject = col.gameObject;

        if(collidedObject.tag == "Enemy")
        {
            collidedObject.GetComponent<EnemyV>().injureTest = true; 
            if(collidedObject.GetComponent <EnemyV>()._state == EnemyV.Mutant_state.injured)
            {
                collidedObject.GetComponent<EnemyV>().Damage(10f, transform.position);
            }
        }
    }
}
