using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public AudioManager AudioManager;
    //list of system prefabs
    public GameObject[] SystemPrefabs;

    //list that has already been created
    private List<GameObject> _instanceSystemPrefabs;

    public GameObject XRManager;

    //mainly for going back to menu
    //if player goes back to menu, unload the prev scene
    //string.Empty (empty string)
    public string _currentLevelName;
    public string _previousLevelName;
    List<AsyncOperation> _loadOperations;

    public PlayerData PlayerData;
    public List<Material> SkyBoxMaterial = new List<Material>();

    //ensuring one instance of the level manager
    private void Start()
    {
        _loadOperations = new List<AsyncOperation>();

        //InstantiateSystemPrefabs();
        LoadSplash();
        //dont destroy the main manager game object
        DontDestroyOnLoad(gameObject);

    }



    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            //remove extra scene loaded
            _loadOperations.Remove(ao);
        }
        Debug.Log("yay! load");
    }

    void UnLoadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("yay! unload");
    }


    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            //create list
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instanceSystemPrefabs.Add(prefabInstance);
        }
    }

    //loading scene
    public void LoadChosenScene(string sceneNameLoad)
    {
        _previousLevelName = _currentLevelName;
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneNameLoad, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("Unable to load async operation is null");
            return;
        }
        ao.completed += OnLoadOperationComplete;
        _currentLevelName = sceneNameLoad;


    }

    //unloading scene
    public void UnloadChosenScene()
    {
        Debug.Log(_currentLevelName);
        AsyncOperation ao = SceneManager.UnloadSceneAsync(_currentLevelName);
        if (ao == null)
        {
            Debug.LogError("Unable to unload async operation is null");
            return;
        }
        ao.completed += UnLoadOperationComplete;
    }


    protected override void OnDestroy()
    {
        /*
        base.OnDestroy();
            for (int i = 0; i < _instanceSystemPrefabs.Count; i++)
        {
            Destroy(_instanceSystemPrefabs[i]);
        }
        _instanceSystemPrefabs.Clear();
        */

    }


    public void LoadSplash()
    {
        GameManager.Instance.XRManager.SetActive(true);
        GameManager.Instance.LoadChosenScene("SplashDesign");
        RenderSettings.skybox = SkyBoxMaterial[0];
        AudioManager.GoToSplash();
        StartCoroutine(WaitToLoadMenu());
    }

    private IEnumerator WaitToLoadMenu()
    {

        yield return new WaitForSeconds(2f);
        LoadMenu();

    }

    public void LoadMenu()
    {

        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("MenuDesign");
        RenderSettings.skybox = SkyBoxMaterial[1];
    }


}