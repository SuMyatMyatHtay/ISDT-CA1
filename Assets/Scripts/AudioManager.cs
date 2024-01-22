using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> BGM = new List<AudioSource>();
    public  List<AudioSource> SFX = new List<AudioSource>();
    public PlayerData playerData;
    public Slider BGMSlider;
    public Slider SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject BackgroundSlider = GameObject.Find("BGMSlider");
        GameObject SoundSlider = GameObject.Find("SFXSlider");
        if (BackgroundSlider != null && SoundSlider != null)
        {

            BGMSlider = BackgroundSlider.GetComponent<Slider>();
            SFXSlider = SoundSlider.GetComponent<Slider>();
            if (BGMSlider != null && SFXSlider != null)
            {
                BGMSlider.value = playerData.BGMAudio;
                SFXSlider.value = playerData.SFXAudio;
            }
        }
        
    }

    private void Update()
    {
        SFX.Clear();
        SFX.AddRange(FindObjectsOfType<AudioSource>());
        for(int x = 0; x < SFX.Count; x++)
        {
            foreach (var audio in BGM)
            {
                if (audio != SFX[x])
                {
                    SFX[x].volume = playerData.SFXAudio;
                }
            }

        }

    }
    public void SetBGMVolume(float volume)
    {
        foreach (var audio in BGM)
        {
           audio.volume = volume;
        }

        playerData.BGMAudio = volume;
    }

    public void SetSFXVolume(float volume)
    {

        playerData.SFXAudio = volume;
        
    }

    public void GoToSplash()
    {
        BGM[0].Play();
        BGM[0].volume = playerData.BGMAudio;
    }
    public void Portal()
    {
        foreach(var audio in BGM)
        {
            audio.Stop();
        }
        BGM[1].Play();
        BGM[1].volume = playerData.BGMAudio;
    }

    public void Silence()
    {
        foreach (var audio in BGM)
        {
            audio.Stop();
        }

    }
    public void MinRui()
    {
        foreach (var audio in BGM)
        {
            audio.Stop();
        }
        BGM[2].Play();
        BGM[2].volume = playerData.BGMAudio;
    }
}
