using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Backfunction : MonoBehaviour
{
    public void OnBackButtonClick()
    {
        // Iterate through all currently loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(i);

            // Check if the scene is not the "persistent" scene, "Splash," or "MainMenuScene"
            if (loadedScene.name != "Boot" && loadedScene.name != "MainMenu")
            {
                // Unload the scene
                SceneManager.UnloadSceneAsync(loadedScene);
            }
        }

        // Load the "MainMenuScene" additively
        //GameManager.Instance.LoadSceneAdditively("MainMenu");
    }
}
