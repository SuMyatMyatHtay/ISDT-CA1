using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New EnemysData", menuName = "Enemy Data", order =  54)]
public class EnemyProjectileData : ScriptableObject
{
    [SerializeField] private string Name;
    [SerializeField] private int Damage;

    public string EnemyName
    {
        get
        {
            return EnemyName;
        }
    }

    public int EnemyDamage
    {
        get
        {
            return Damage;
        }
    }
}
