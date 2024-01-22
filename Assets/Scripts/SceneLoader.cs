using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
 
    
    public void LoadSplash()
    {
        GameManager.Instance.LoadChosenScene("SplashDesign");

        GameManager.Instance.XRManager.SetActive(true);
    }

    public void LoadMenu()
    {
        GameManager.Instance.AudioManager.GoToSplash();
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("MenuDesign");
        RenderSettings.skybox = GameManager.Instance.SkyBoxMaterial[1];
        GameManager.Instance.XRManager.SetActive(true);

    }

    public void LoadWinning()
    {
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("WiningSceneDesign");
        GameManager.Instance.XRManager.SetActive(true);
    }

    public void LoadLosing()
    {
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("LoseSceneDesign");
        GameManager.Instance.XRManager.SetActive(true);
    }

    public void LoadFinalScene()
    {
        GameManager.Instance.UnloadChosenScene();

        GameManager.Instance.LoadChosenScene("FinalSceneDesign");

        GameManager.Instance.XRManager.SetActive(false);


    }

    public void LoadSuLoadingScene()
    {
        GameManager.Instance.UnloadChosenScene();

        GameManager.Instance.XRManager.SetActive(false);
        GameManager.Instance.LoadChosenScene("EarthTransition");

        GameManager.Instance.AudioManager.Silence();
    }


    public void LoadMinRuiLoadingScene()
    {
        GameManager.Instance.AudioManager.Portal();
        GameManager.Instance.XRManager.SetActive(false);
        RenderSettings.skybox = GameManager.Instance.SkyBoxMaterial[2];
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("SpaceTransition");
    }



    public void LoadSuScene()
    {
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("SuGameScene");
        GameManager.Instance.XRManager.SetActive(false);
    }


    public void LoadMinRuiMaze()
    {
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("SpaceStationMR");
        GameManager.Instance.XRManager.SetActive(false);
    }

    public void LoadingToSpace()
    {
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("SpaceTransition");
        GameManager.Instance.XRManager.SetActive(false);
    }
    
}
