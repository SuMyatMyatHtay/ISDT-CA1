using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCollider : MonoBehaviour
{
    public GameObject InventoryManager;
    public AudioSource AudioFCF;

    [SerializeField] private PlayerData playerData;
    private bool CrystalCheckBool; 
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("TurrentTargetPlayer"))
        {
            CrystalCheckBool = false;
            CheckCrystal(); 
        }
        
    }

    public void CheckCrystal()
    {
        
        foreach (var item in InventoryManager.GetComponent<InventoryManager>().Items)
        {
            if(item.id == 1)
            {
                playerData.SuSceneCheck = true;
                CrystalCheckBool = true;
                //GameManager.Instance.AudioManager.GoToSplash();
                GameManager.Instance.UnloadChosenScene();
                GameManager.Instance.LoadChosenScene("FinalSceneDesign");
                RenderSettings.skybox = GameManager.Instance.SkyBoxMaterial[1];
                GameManager.Instance.XRManager.SetActive(true);
            }
        }
         

        if (!CrystalCheckBool)
        {
            AudioFCF.Play(); 
        }
    }
}
