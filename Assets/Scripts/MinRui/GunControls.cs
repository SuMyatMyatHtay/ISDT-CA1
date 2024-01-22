using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GunControls : MonoBehaviour
{
    /*
     Written By: Goh Min Rui
     Description: Gun XR interactor settings
     */
    [SerializeField] private bool gunIsGrabbed=false;
    [SerializeField] private Item item;
    [SerializeField] private InputActionProperty Left_pri;
    private GunConfiguration GunConfiguration;
    [SerializeField] private Player Player;

    [Header("Shooting")]
    [SerializeField] private List<Transform> GunTriggerPoint = new List<Transform>();
    [SerializeField] private Slider ReloadSlider;
    [SerializeField] private TextMeshProUGUI ReloadingText;
    private GameObject GunBullets;
    private int NoOfBullets;
    private bool AllowInstantiate = true;


    private void Start()
    {
        if (gameObject.GetComponent<GunConfiguration>() != null)
        {
            GunConfiguration = gameObject.GetComponent<GunConfiguration>();
            ReloadSlider.value = 1;
            ReloadingText.text = GunConfiguration.BulletCount+ @"
Bullets";
            ReloadingText.fontSize = 5f;
        }
    }

    private void Update()
    {
        if (Left_pri.action.WasPressedThisFrame())
        {
            if (gunIsGrabbed == true)
            {
                InventoryManager.Instance.Add(item);
                //Destroy(gameObject);
                gameObject.SetActive(false);
                gameObject.transform.position = Vector3.zero;

            }
        }
    }


    public void GunIsGrabbed()
    {
        Player.SelectedGun =gameObject;
        gunIsGrabbed = true;
    }

    public void GunIsNotGrabbed()
    {
        gunIsGrabbed = false;
    }

    public void GunIsActivated()
    {
        GunConfiguration = gameObject.GetComponent<GunConfiguration>();

        if (AllowInstantiate)
        {
            int RandomTriggerPoint = Random.Range(0, GunTriggerPoint.Count);
            // Instantiate Bullets
            GameObject newBullets = Instantiate(GunConfiguration.GunBullets, GunTriggerPoint[RandomTriggerPoint].position, Random.rotation);
            Instantiate(GunConfiguration.BulletSFX).Play();
            GunConfiguration.BulletCount--;

            ReloadingText.text = $"{GunConfiguration.BulletCount} Bullets";
            ReloadSlider.value = (float)GunConfiguration.BulletCount / GunConfiguration.MaxBulletCount;
            ReloadingText.fontSize = 4f;
            Destroy(newBullets, 2f);

            if (GunConfiguration.BulletCount == 0)
            {
                AllowInstantiate = false;
                StartCoroutine(BulletCountdown());
            }
        }
    }

    private IEnumerator BulletCountdown()
    {
        int countdown = GunConfiguration.BulletReloadCountdown;

        while (countdown > 0)
        {
            ReloadingText.text = $"{countdown}s";
            ReloadSlider.value = 0f + (float)countdown / GunConfiguration.MaxCountdown;
            ReloadingText.fontSize = 7f;
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        AllowInstantiate = true;
        GunConfiguration.BulletCount = GunConfiguration.MaxBulletCount;
        ReloadingText.text = GunConfiguration.BulletCount + @"
Bullets";
        ReloadingText.fontSize = 5f;
    }
}




   
