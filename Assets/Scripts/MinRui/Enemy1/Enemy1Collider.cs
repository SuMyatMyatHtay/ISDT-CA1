using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1Collider : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private Enemy1Main Enemy1Main;
    [SerializeField] private ParticleSystem EnemyDamage;
    [SerializeField] private Slider EnemyHealthBar;
    [SerializeField] private AudioSource EnemyHurt;
    private GameObject SourceGun;
    private int damage;
    private int MaxHealth;

    private void Start()
    {
        MaxHealth = Enemy1Main.EnemyHealth;
        EnemyDamage.Pause();
        EnemyHealthBar.value = 1;
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider enteredObject)
    {
        if (enteredObject.tag == "PlayerProjectile")
        {
            Enemy1Main._state= Enemy1Main.Enemy_State.hurt;
            SourceGun= Player.SelectedGun;
            damage=SourceGun.GetComponent<GunConfiguration>().GunDamage;
            Enemy1Main.EnemyHealth -= damage;
            EnemyDamage.Play();
            EnemyHealthBar.value = (float)Enemy1Main.EnemyHealth / MaxHealth;
            Debug.Log(Enemy1Main.EnemyHealth / MaxHealth+" HEALTH");
            EnemyHurt.Play();
        }
    }
}
