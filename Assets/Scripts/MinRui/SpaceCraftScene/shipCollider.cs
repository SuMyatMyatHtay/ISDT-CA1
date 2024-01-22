using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shipCollider : MonoBehaviour
{
    /* 
    Written By: Goh Min Rui
    Description: Ship's collider for collider health
    */
    [Header("Slider items")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image Background;
    [SerializeField] private Image FillArea;
    [SerializeField] private Material GlowyBlue;
    [SerializeField] private Material Red;
    [SerializeField] private TextMeshProUGUI healthText;
    [Header("Ship related")]
    [SerializeField] private shipMotion shipMotion;
    public float shipHealth = 300; 
    private float MaxHealth;
    [Header("War Craft Names")]
    private List<string> warcraft = new List<string>() { "WarCraft1(Clone)", "WarCraft2(Clone)", "WarCraft3(Clone)" };
   

    private void Start()
    {
        MaxHealth = shipHealth;
        Background.material = GlowyBlue;
    }

    private void Update()
    {
        float shipHealthValue = shipHealth / MaxHealth;
        slider.value = shipHealthValue;
        healthText.text = @"Planes's Health: 
"
            + shipHealth;
        if (shipHealth <= 0)
        {
            FillArea.enabled=false;
        }
        if (shipHealth < MaxHealth)
        {
            Background.material = null;
        }
        if(shipHealth <= 50)
        {
            healthText.text += "!";
            healthText.color = Color.red;
            FillArea.material = Red;
        }
     
    }

    private void OnTriggerEnter(Collider collidedObj)
    {
        if (collidedObj.tag == "EnemyProjectile")
        {
            //Find the responsible warcraft that shoot the projectile
            Debug.Log("Enter collider" + collidedObj.name);
            for (var i = 0; i < warcraft.Count; i++)
            {
                if (GameObject.Find(warcraft[i]) != null)
                {
                    Debug.Log("Found" + warcraft[i]);
                    GameObject existingWarCraft = GameObject.Find(warcraft[i]);
                    shipHealth -= existingWarCraft.GetComponent<warShipConfiguration>().warShipDamage;
                }
            }
        }
        if (collidedObj.tag == "Asteroids")
        {
            shipHealth -= 1;
        }
    }
}
