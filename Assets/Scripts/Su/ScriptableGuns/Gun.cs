using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using Random = UnityEngine.Random;

//---------------------------------------------------------------------------------
// Author		: SU MYAT MYAT HTAY 
// Date  		: 2023-02-12
// Description	: Gun Script
//---------------------------------------------------------------------------------

// [RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    public bool clipFinished;
    public int BulletCount { get; set; }
    [SerializeField] private TMP_Text bulletDisplay;
    [SerializeField] private AnimateCharging animateCharging;
    [SerializeField] private bool AddBulletSpread = true;
    [SerializeField] private Vector3 BulletSpreadVariance = new Vector3(0.05f, 0.05f, 0.05f);
    [SerializeField] private Vector3 TightBulletSpreadVariance = new Vector3(0.01f, 0.01f, 0.01f);
    [SerializeField] private ParticleSystem ShootingSystem;
    [SerializeField] private Transform BulletSpawnPoint;
    [SerializeField] private ParticleSystem ImpactParticleSystem;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private float ShootDelay = 0.5f;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private float BulletSpeed = 100;

    private bool gunIsGrabbed;
    // private Animator Animator;
    private float LastShootTime;
    [SerializeField] private Vector3 curBulletSpreadVariance; // To know which spread is currently using

    [SerializeField] private XRSocketInteractor socket;
    IXRSelectInteractable objName; // To access to the magazine in the socket

    private void Awake()
    {
        // Animator = GetComponent<Animator>();
        curBulletSpreadVariance = BulletSpreadVariance;
        BulletCount = 0;
        clipFinished = true;
        animateCharging.isLastBullet = true;
    }

    private void Start()
    {
        UpdateBulletsDisplay();
    }

    public void Shoot()
    {
        if (clipFinished) return; // No more bullet to shoot

        if (LastShootTime + ShootDelay < Time.time)
        {
            // Use an object pool instead for these! To keep this tutorial focused, we'll skip implementing one.
            // For more details you can see: https://youtu.be/fsDE_mO4RZM or if using Unity 2021+: https://youtu.be/zyzqA_CPz2E

            // Animator.SetBool("IsShooting", true);
            ShootingSystem.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                LastShootTime = Time.time;
            }
            // this has been updated to fix a commonly reported problem that you cannot fire if you would not hit anything
            else
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + GetDirection() * 100, Vector3.zero, false));

                LastShootTime = Time.time;
            }

            BulletCount--;
            if (BulletCount == 0)
            {
                clipFinished = true;
                animateCharging.isLastBullet = true; // When shooted last bullet, make the gun slide stay at back
            }
            UpdateBulletsDisplay();
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = BulletSpawnPoint.transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-curBulletSpreadVariance.x, curBulletSpreadVariance.x),
                Random.Range(-curBulletSpreadVariance.y, curBulletSpreadVariance.y),
                Random.Range(-curBulletSpreadVariance.z, curBulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        // This has been updated from the video implementation to fix a commonly raised issue about the bullet trails
        // moving slowly when hitting something close, and not
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        // Animator.SetBool("IsShooting", false);
        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }

    public void UpdateBulletsDisplay()
    {
        bulletDisplay.text = String.Format("{0:D2}", BulletCount);
    }

    public void SetGunGrabbed(bool value)
    {
        gunIsGrabbed = value;
    }

    public void SetHoverOverGun(bool isHovered)
    {
        if (isHovered && gunIsGrabbed)
        {
            curBulletSpreadVariance = TightBulletSpreadVariance;
        }
        else
        {
            curBulletSpreadVariance = BulletSpreadVariance;
        }
    }

    public void MagInserted()
    {
        StartCoroutine(CheckMag());
    }

    public void MagRemoved()
    {
        UpdateBulletsInMagazine(); // To update bullet count in MagInfo

        if (BulletCount > 1) BulletCount = 1;

        if (BulletCount == 1) animateCharging.isLastBullet = true;
        UpdateBulletsDisplay();
    }

    private IEnumerator CheckMag()
    {
        yield return new WaitForEndOfFrame();

        objName = socket.GetOldestInteractableSelected();

        if (objName != null)
        {
            Debug.Log(objName.transform.name + " in SO socket of " + transform.name);

            MagInfo magInfo = objName.transform.gameObject.GetComponent<MagInfo>();
            

            if (magInfo != null)
            {
                Debug.Log("Amount of bullets in magazine: " + magInfo.magBulletCount.ToString());
                BulletCount += magInfo.magBulletCount;

                if (BulletCount == 1)
                {
                    if (clipFinished) animateCharging.EndCharge();
                    clipFinished = false;
                    animateCharging.isLastBullet = true;
                }
                else if (BulletCount > 0)
                {
                    if (clipFinished) animateCharging.EndCharge();
                    clipFinished = false;
                    animateCharging.isLastBullet = false;
                }

                UpdateBulletsDisplay();
            }
            
        }
        else
        {
            Debug.Log("No magazine");
        }
    }

    private void UpdateBulletsInMagazine()
    {
        if (objName != null)
        {
            MagInfo magInfo = objName.transform.gameObject.GetComponent<MagInfo>();
            if (BulletCount > 0)
            {
                magInfo.magBulletCount = BulletCount - 1;
            }
            else
            {
                magInfo.magBulletCount = 0;
            }
            magInfo.UpdateBulletsDisplay();
        }
    }
}
