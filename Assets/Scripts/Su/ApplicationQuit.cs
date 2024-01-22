using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuit : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false; 
    }

    public void HalfExitToMenu()
    {
        GameManager.Instance.AudioManager.GoToSplash();
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("MenuDesign");
        RenderSettings.skybox = GameManager.Instance.SkyBoxMaterial[1];
        GameManager.Instance.XRManager.SetActive(false);
    }
}
