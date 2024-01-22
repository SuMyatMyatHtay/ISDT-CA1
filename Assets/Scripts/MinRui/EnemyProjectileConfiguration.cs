using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileConfiguration : MonoBehaviour
{
    public int Damage;
    public EnemyProjectileData EnemyProjectileData;

    private void Start()
    {
        Damage = EnemyProjectileData.EnemyDamage;
    }
}
