using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartingPlayerData : MonoBehaviour
{
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        RestartPlayerData(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartPlayerData()
    {
        playerData.SuSceneCheck = false;
        playerData.MRSceneCheck = false;
        playerData.PlayerHealth = 1000; 
    }
}
