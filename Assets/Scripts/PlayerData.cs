using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data", order = 53)]
public class PlayerData : ScriptableObject
{

    [SerializeField] private int playerHealth;
    [SerializeField] private float BGMaudioVolume;
    [SerializeField] private float SFXaudioVolume;
    [SerializeField] private bool doneSu;
    [SerializeField] private bool doneMR;
    // Start is called before the first frame update
    public int PlayerHealth
    {
        get
        {
            return playerHealth;
        }
        set { 
            playerHealth = value; 
        }
    }
    public float BGMAudio
    {
        get
        {
            return BGMaudioVolume;
        }
        set
        {
            BGMaudioVolume = value;
        }
    }

    public float SFXAudio
    {
        get
        {
            return SFXaudioVolume;
        }
        set
        {
            SFXaudioVolume = value;
        }
    }

   
   
    public bool SuSceneCheck
    {

        get
        {
            return doneSu;
        }
        set
        {
            doneSu = value;
        }
    }

    public bool MRSceneCheck
    {
        get
        {
            return doneMR;
        }
        set
        {
            doneMR = value;
        }
    }
}
