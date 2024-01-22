using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerV : MonoBehaviour
{
   public PlayerData PlayerData;
    /*
     PlayerData.PlayerHealth = (int) PlayerData.PlayerHealth-_dmg
     
     */
    public float Health_amt, HealthMax_amt;
    public Slider HealthSlider; 
    public int playerCheck; //if it is our main player, it will be represented by 1. 
    //public GameObject PostProcessingGO;
    // Start is called before the first frame update
    void Start()
    {
        if(playerCheck == 1)
        {
            HealthMax_amt = (float)PlayerData.PlayerHealth;
        }
        Health_amt = gameObject.GetComponent<PlayerV>().HealthMax_amt;
        HealthSlider.value = 1f; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Health_amt <= 0)
        {
            Health_amt = 0; 
            HealthSlider.value = 0f; 
            //then die 
            //print("Player Die");
            //Destroy(this.gameObject); 
            gameObject.tag = "Untagged";

            if (playerCheck == 1)
            {
                if (PlayerData.PlayerHealth<=0)
                {
                    GameManager.Instance.UnloadChosenScene();
                    GameManager.Instance.LoadChosenScene("LoseSceneDesign");
                    GameManager.Instance.XRManager.SetActive(false);
                }
            }

            /*
            if (playerCheck == 0)
            {
                Destroy(this.gameObject);
            }
            */
        }
            //Health_amt = gameObject.GetComponent<PlayerV>().Health_amt;
        //Debug.Log(Health_amt / HealthMax_amt + " : WHealth"); 
        HealthSlider.value = Health_amt / HealthMax_amt;

    }

    public void Damage(float _dmg)
    {
        Health_amt -= _dmg;
        if (playerCheck == 1)
        {
            PlayerData.PlayerHealth = (int)PlayerData.PlayerHealth - (int)_dmg; 
            //PostProcessingGO.GetComponent<PostProcessing>().UpdateVignetteIntensity(Health_amt);
        }
    }
}
