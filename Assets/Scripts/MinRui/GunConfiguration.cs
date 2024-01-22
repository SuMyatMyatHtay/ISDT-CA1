using UnityEngine;

public class GunConfiguration : MonoBehaviour
{
    public GunData gunData;
    [HideInInspector] public GameObject GunBullets;
    [HideInInspector] public int MaxBulletCount;
    [HideInInspector] public int BulletCount;
    [HideInInspector] public int GunDamage;
    [HideInInspector] public int MaxCountdown;
    [HideInInspector] public int BulletReloadCountdown;
    [HideInInspector] public AudioSource BulletSFX;

    private void Start()
    {
        GunBullets = gunData.BulletPrefab;
        MaxBulletCount = gunData.MaxBulletCount;
        BulletCount = gunData.MaxBulletCount;
        GunDamage = gunData.GunDamage;
        MaxCountdown = gunData.BulletReloadTime;
        BulletReloadCountdown = gunData.BulletReloadTime;
        BulletSFX = gunData.GunSoundEffect;
    }
}
