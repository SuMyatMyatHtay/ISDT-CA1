using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New warShipData", menuName = "WarShip Data", order = 51)]
public class warShipData : ScriptableObject
{
    [SerializeField]
    private string shipName;
    [SerializeField]
    private float shipAttackDamage;
    [SerializeField]
    private float shipHealth;
    [SerializeField]
    private List<Vector3> projectileLaunchers = new List<Vector3>();

 


    public string warShipName
    {
        //for other script to access

        get
        {
            return shipName;
        }
    }
    public float warShipAttackDamage
    {

        get
        {
            return shipAttackDamage;
        }
    }

    public float enemyAircraft
    {
        get
        {
            return shipHealth;
        }
    }


    public List<Vector3> Launcher
    {
        get
        {
            return projectileLaunchers;
        }
    }
   

}
