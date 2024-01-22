using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingToPortal : MonoBehaviour
{
    [SerializeField] private Slider slider;
  private float currentTime = 0f;
    [SerializeField] private float Countdown = 5.0f;
    [SerializeField] private TextMeshProUGUI progressText;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(LoadScene(2.0f));
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
            slider.value = progress/Countdown;
            progressText.text = "Get ready to pilot a plane " + @"
"+Math.Round(progress / Countdown * 100f)+ "%";

        }
    }

    private IEnumerator LoadScene(float amount)
    {
   
            yield return new WaitForSeconds(amount);
            GameManager.Instance.UnloadChosenScene();
            GameManager.Instance.LoadChosenScene("SpaceCraftSceneMR");
            GameManager.Instance.XRManager.SetActive(false);

        GameManager.Instance.AudioManager.MinRui();
    }


}
