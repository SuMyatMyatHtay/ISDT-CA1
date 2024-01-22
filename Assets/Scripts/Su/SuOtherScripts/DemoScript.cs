using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public AudioSource demoAudioSource;
    public GameObject rayInteractorLeft;
    public GameObject rayInteractorRight; 

    private float demoTimer = 0f;
    private float demoMessageTime = 8f;
    private bool thirtySecondsMessagePrinted = false;

    private void Start()
    {
        //demoMessageTime = demoAudioSource.clip.length; 
        demoAudioSource.Play();
        rayInteractorLeft.SetActive(false);
        rayInteractorRight.SetActive(false); 
    }
    void Update()
    {
        demoTimer += Time.deltaTime;

        // Check if 30 seconds have passed and the message hasn't been printed yet
        if (demoTimer >= demoMessageTime && !thirtySecondsMessagePrinted)
        {
            rayInteractorLeft.SetActive(true);
            rayInteractorRight.SetActive(true);
            thirtySecondsMessagePrinted = true;
        }
    }
}
