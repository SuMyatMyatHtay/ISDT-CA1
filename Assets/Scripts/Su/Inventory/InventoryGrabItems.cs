using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrabItems : MonoBehaviour
{

    [Header("Inventory Items")]
    public GameObject book;
    public GameObject bookRef;

    [Header("Crystal Mini")]
    //public GameObject crystalMini; 
    public GameObject mazeEnterColliderGO;
    public AudioSource AudioOUIM;
    public GameObject CheckHeatPadGO;
    public Item item4; 
    private bool CrystalUseAlr = false; 

    [Header("Magazine")]
    public GameObject magazineref;
    public GameObject magazine1;
    public GameObject magazine1WD;

    [Header("Guns")]
    public GameObject gunRef; 
    public GameObject blueSciFiGun; 
    public GameObject greenSciFiGun;
    public GameObject orangeSciFiGun; 
    public GameObject purpleSciFiGun;
    public GameObject redSciFiGun;
    public GameObject yellowSciFiGun;
    public GameObject laserPistol;
    public GameObject M47Gun;

    [Header("Final Crystals")]
    public GameObject CrystalRef; 
    public GameObject SuCrystal;
    public GameObject MRCrystal;
   
    /*
    public GameObject redShoes;
    public GameObject redShoesRef;
    public GameObject goldenGoose;
    public GameObject goldenGooseRef;
    */

    // Start is called before the first frame update
    void Start()
    {
        //DisableEveryObject(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableEveryObject()
    {
        //book.SetActive(false);
        //bookRef.SetActive(false); 
        /*
        redShoes.SetActive(false);
        redShoesRef.SetActive(false);
        goldenGoose.SetActive(false);
        goldenGooseRef.SetActive(false); 
        */

    }

    public void BookRepositioning()
    {
        DisableEveryObject(); 
        book.transform.position = bookRef.transform.position;
        book.SetActive(true); 
    }

    public void KeepMiniCrystal()
    {
        if(CheckHeatPadGO.GetComponent<CheckHeatPad>().MazeCrystalKeep == true)
        {
            if (!CrystalUseAlr)
            {
                DisableEveryObject();
                mazeEnterColliderGO.GetComponent<MazeEnterCollider>().ShowMeThePath();
               
                InventoryManager.Instance.Remove(item4);
                CrystalUseAlr = true;
            }
        }
        else
        {
            AudioOUIM.Play(); 
        }
        
    }

    //no longer using 
    public void UseMiniCrystal()
    {
        DisableEveryObject();
        mazeEnterColliderGO.GetComponent<MazeEnterCollider>().ShowMeThePath();
    }

    public void MagazineRepositioning(int bulletCount)
    {
        DisableEveryObject(); 
        magazine1.transform.position = magazineref.transform.position; 
        magazine1.SetActive(true);
        magazine1.GetComponent<MagInfo>().magBulletCount = bulletCount; 
    }

    public void MagazineWDRepositioning(int bulletCount)
    {
        DisableEveryObject();
        magazine1WD.transform.position = magazineref.transform.position;
        magazine1WD.SetActive(true);
        magazine1WD.GetComponent<MagInfo>().magBulletCount = bulletCount;
    }

    public void GunPositioning(int gunID)
    {
        switch (gunID)
        {
            case 10: 
                blueSciFiGun.transform.position = gunRef.transform.position; 
                blueSciFiGun.SetActive(true);
                break; 

            case 11: 
                greenSciFiGun.transform.position = gunRef.transform.position;
                greenSciFiGun.SetActive(true); 
                break;

            case 12:
                orangeSciFiGun.transform.position = gunRef.transform.position;
                orangeSciFiGun.SetActive(true);
                break;

            case 13:
                purpleSciFiGun.transform.position = gunRef.transform.position;
                purpleSciFiGun.SetActive(true);
                break;

            case 14:
                redSciFiGun.transform.position = gunRef.transform.position;
                redSciFiGun.SetActive(true);
                break;

            case 15:
                yellowSciFiGun.transform.position = gunRef.transform.position;
                yellowSciFiGun.SetActive(true);
                break;

            case 16:
                laserPistol.transform.position = gunRef.transform.position;
                laserPistol.SetActive(true);
                break;

            case 17:
                M47Gun.transform.position = gunRef.transform.position;
                M47Gun.SetActive(true);
                break;
        }
    }
    
    public void UseSuCrystal()
    {
        SuCrystal.transform.position = CrystalRef.transform.position; 
        SuCrystal.SetActive(true);
    }

    public void UseMRCrystal()
    {
        MRCrystal.transform.position= CrystalRef.transform.position; 
        MRCrystal.SetActive(true); 
    }
    
    /*
    public void RedShoesRepositioning()
    {
        DisableEveryObject(); 
        redShoes.transform.position = redShoesRef.transform.position;
        redShoes.SetActive(true); 
    }

    public void GoldenGooseRepositioning()
    {
        DisableEveryObject();
        goldenGoose.transform.position = goldenGooseRef.transform.position;
        goldenGoose.SetActive(true); 
    }
    */
}
