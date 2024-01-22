using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy2Collider : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private Enemy2Main Enemy2Main;
    [SerializeField] private ParticleSystem EnemyDamage;
    [SerializeField] private Slider EnemyHealthBar;
    [SerializeField] private AudioSource EnemyHurt;
    private GameObject SourceGun;
    private int damage;
    private int MaxHealth;

    private void Start()
    {
        MaxHealth = Enemy2Main.EnemyHealth;
        EnemyDamage.Pause();
        EnemyHealthBar.value = 1;
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider enteredObject)
    {
        if (enteredObject.tag == "PlayerProjectile")
        {
            SourceGun = Player.SelectedGun;
            damage = SourceGun.GetComponent<GunConfiguration>().GunDamage;
            Enemy2Main.EnemyHealth -= damage;
            EnemyDamage.Play();        
            EnemyHealthBar.value = (float)Enemy2Main.EnemyHealth / MaxHealth;
            EnemyHurt.Play();
        }
    }
}
