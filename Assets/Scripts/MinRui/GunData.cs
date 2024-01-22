using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunData", menuName = "Gun Data", order = 51)]
public class GunData : ScriptableObject
{
   [SerializeField] private string GunName;
   [SerializeField] private GameObject projectilePrefab;
   [SerializeField] private int maxBulletCount;
   [SerializeField] private int gunDamage;
   [SerializeField] private int BulletCountdown;
    [SerializeField] private AudioSource BulletSound;


    public string WeaponName
    {
        get
        {
            return GunName;
        }
    }

    public GameObject BulletPrefab
    {
        get
        {
            return projectilePrefab;
        }
    }

    public int MaxBulletCount
    {
        get
        {
            return maxBulletCount;
        }
    }

    public int GunDamage
    {
        get
        {
            return gunDamage;
        }
    }

    public int BulletReloadTime
    {
        get
        {
            return BulletCountdown;
        }
    }

    public AudioSource GunSoundEffect
    {
        get
        {
            return BulletSound;
        }
    }
}
