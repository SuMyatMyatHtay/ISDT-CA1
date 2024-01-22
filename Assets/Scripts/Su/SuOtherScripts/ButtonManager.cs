using UnityEngine;
using UnityEngine.UI;

public class Scene2Controller : MonoBehaviour
{
    [SerializeField] private GameObject UIManager; 
    [SerializeField] private Button StartButton;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        StartButton.onClick.AddListener(StartButtonClick);
        level2Button.onClick.AddListener(Level2ButtonClick);
        level3Button.onClick.AddListener(Level3ButtonClick);
        exitButton.onClick.AddListener(Exit); 
    }

    private void StartButtonClick()
    {
        //UIManager.GetComponent<UIManager>().FadeOutMenu();
        // Access the GameManager script from Scene1
        Debug.Log("Start Button is Clicked");
        //UIManager.GetComponent<UIManager>().FadeOutMenu();
        
        //GameManager.Instance.FadeInBS();
    }

    private void Level2ButtonClick()
    {
        // Access the GameManager script from Scene1
        //GameManager.Instance.LoadScene("Level2");
    }

    private void Level3ButtonClick()
    {
        // Access the GameManager script from Scene1
        //GameManager.Instance.LoadScene("Level3");
    }

    public void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
}