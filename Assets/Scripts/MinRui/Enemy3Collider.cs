using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy3Collider : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private Enemy3Main Enemy3Main;
    [SerializeField] private GameObject EnemyDamage;
    [SerializeField] private Slider EnemyHealthBar;
    [SerializeField] private AudioSource EnemyHurt;
    private ParticleSystem DamageParticles;
    private GameObject SourceGun;
    private int damage;
    private int MaxHealth;

    private void Start()
    {
        MaxHealth = Enemy3Main.EnemyHealth;
        DamageParticles = EnemyDamage.GetComponent<ParticleSystem>();
        DamageParticles.Pause();
        EnemyDamage.SetActive(false);
        EnemyHealthBar.value = 1;
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider enteredObject)
    {
        Debug.Log(enteredObject.name);
        if (enteredObject.tag == "PlayerProjectile")
        {
            EnemyDamage.SetActive(true);
            SourceGun = Player.SelectedGun;
            damage = SourceGun.GetComponent<GunConfiguration>().GunDamage;
            Enemy3Main.EnemyHealth -= damage;
            DamageParticles.Clear();
            DamageParticles.Play();
            EnemyHealthBar.value =(float) Enemy3Main.EnemyHealth / MaxHealth;
            EnemyHurt.Play();
        }
    }
}
