using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCollider : MonoBehaviour
{
    public PlayerData playerData;
    private int PlayerHealth;
    void Start()
    {
        PlayerHealth = playerData.PlayerHealth;
    }
    private void Update()
    {
        if (playerData.PlayerHealth <= 0)
        {
            GameManager.Instance.UnloadChosenScene();
            GameManager.Instance.LoadChosenScene("LoseSceneDesign");
            GameManager.Instance.XRManager.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "EnemyProjectile")
        {
            Debug.Log(other);
            EnemyProjectileConfiguration config = other.GetComponent<EnemyProjectileConfiguration>();
            if (config != null)
            {
                playerData.PlayerHealth = (int)playerData.PlayerHealth - config.Damage;
                Debug.Log("hit");
            }
        }
    }



}