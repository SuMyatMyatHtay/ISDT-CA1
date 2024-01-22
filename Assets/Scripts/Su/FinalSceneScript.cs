using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FinalSceneScript : MonoBehaviour
{
    public PlayerData playerData;
    //private InventoryManager inventoryManager;
    public Item SuItem;
    public Item MRItem;
    public int CrystalCheck = 0;

    public Button SuGameButton; 
    public Button MRGameButton; 

    // Start is called before the first frame update
    void Start()
    {
        if (playerData.SuSceneCheck)
        {
            SuGameButton.interactable = false; 
            InventoryManager.Instance.Add(SuItem);
        }

        if (playerData.MRSceneCheck)
        {
            MRGameButton.interactable = false; 
            InventoryManager.Instance.Add(MRItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CrystalCheck==2)
        {
            Debug.Log("Move to Victory Scene");
            GameManager.Instance.AudioManager.Silence();
            GameManager.Instance.UnloadChosenScene();
            GameManager.Instance.LoadChosenScene("WiningSceneDesign");
            GameManager.Instance.XRManager.SetActive(false);
        }
    }
}
