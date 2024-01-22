using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shipEffects : MonoBehaviour
{
    /*
     Written By: Min Rui
     Description: Player ship's particle system for the aircraft flames 
     which varies depending on aircraft speed and also shooting effects 
     for the aircraft
     */
    [Header("AircraftShooting")]
    //Projectile Capsule
    [SerializeField] private GameObject shootingProjectile;
    //shootingSpeed
    [SerializeField] private float shootingSpeed = 2400f;
    //List of launching point
    [SerializeField] public List<GameObject> launchersPoint = new List<GameObject>();
    [SerializeField] private GameObject shootingButton;
    private List<GameObject> instantiatedProjectile = new List<GameObject>();

    //Just controlling flames with throttle strength
    public void aircraftEngineFlamesfloat(GameObject flame, float throttleAngle)
    {
        var flameSystem = flame.GetComponent<ParticleSystem>();
        var flameMain = flameSystem.main;
        var flameEmission = flameSystem.emission;
        flameEmission.rateOverTime = (throttleAngle / 90) * 20;
        flameMain.startLifetime = (throttleAngle / 90) * 5;
        flameMain.startSize = (throttleAngle / 90) * 1 + 1;
        flameMain.startColor = new Color(255, 255, 255, (throttleAngle / 90) * 127 + 128);
    }

    //when grab of handle is activated, shoot projectile
    public void aircraftShooting()
    {
        int count = 0;
        for (var i = 0; i < instantiatedProjectile.Count; i++)
        {
            if (instantiatedProjectile[i] != null)
            {
                count += 1;
            }
        }
        //10 projectile instantiated at any time
        if (count < 10)
        {
            shootingButton.GetComponent<Animator>().SetTrigger("Shoot");
            //reduce gravity so projectile doesnt fall immediately
            Physics.gravity = new Vector3(0, -0.5f, 0);
            if (launchersPoint.Count != 0)
            {
                int launcherIndex = Random.Range(0, launchersPoint.Count);
                instantiatedProjectile.Add(Instantiate(shootingProjectile, launchersPoint[launcherIndex].transform.position, Random.rotation));
                shootingProjectile.GetComponent<Rigidbody>().AddForce(launchersPoint[launcherIndex].transform.forward * shootingSpeed, ForceMode.Impulse);
                var index = instantiatedProjectile.Count - 1;
                //randomise destroy of projectile
                float deathTime = Random.Range(5, 8);
                Destroy(instantiatedProjectile[index], deathTime);

            }
        }
    }


}
