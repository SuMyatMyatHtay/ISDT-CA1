using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween; 

public class GoBackToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tween.Delay(duration: 5f, () =>
        {
            LoadMenu(); 
        }); 
    }

    public void LoadMenu()
    {

        GameManager.Instance.AudioManager.GoToSplash();
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("MenuDesign");
        RenderSettings.skybox = GameManager.Instance.SkyBoxMaterial[1];
        GameManager.Instance.XRManager.SetActive(false);
    }
}
