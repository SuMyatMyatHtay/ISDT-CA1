using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingToSuScene : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float Countdown = 2.0f;
    [SerializeField] private TextMeshProUGUI progressText;
     private string updateText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene(Countdown));
        slider.value = 0;
        currentTime = Countdown;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        float progress = Countdown - currentTime;


        if (progress < Countdown)
        {
            slider.value = progress / Countdown;
            progressText.text = "Loading " + Math.Round(progress / Countdown * 100f) + "%";

        }
    }

    private IEnumerator LoadScene(float amount)
    {

        yield return new WaitForSeconds(18f);
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("SuGameScene");
        GameManager.Instance.XRManager.SetActive(false);

    }


}
