using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//get everything from enemy's aircraft scriptable object

/* 
   Written By: Min Rui
   Description: Script to just get the data from the respective scriptable object
*/
public class warShipConfiguration : MonoBehaviour
{
    public warShipData warShipData;
    public GameObject launchingPoint;
    public Vector3 Launchers;
    public GameObject warShipObject;
    public List<GameObject> listOfProjectile = new List<GameObject>();
    public float warShipDamage;
    public float warShipHealth;

    //Basically ties the scriptable object and usse it in shipEnemy
    void Start()
    {
        //Randomly pick index of launching Point
        int chosenIndex = Random.Range(0, warShipData.Launcher.Count);
        Launchers = warShipData.Launcher[chosenIndex];
        warShipObject = gameObject;
        warShipDamage = warShipData.warShipAttackDamage;
        warShipHealth = warShipData.enemyAircraft;
    }
    private void Update()
    {
        int chosenIndex = Random.Range(0, warShipData.Launcher.Count);
        Launchers = warShipData.Launcher[chosenIndex];
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Enter collider" + other.tag+" dwd"+other.name);
        if (other.tag == "PlayerProjectile")
        {
            Debug.Log("hit");
            warShipHealth -= 10;
        }
    }
}
