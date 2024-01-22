using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAsteroids : MonoBehaviour
{
    [SerializeField] public List<GameObject> asteroids = new List<GameObject>();
    [SerializeField] public float movementSpeed = 5f;
    private List<GameObject> InstantiatedAsteroids = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("asteriodMovement", 0f, 2f);
        for (var i=0; i < asteroids.Count; i++)
        {
          
            for (var x = 0; x < 10; x++)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
                Vector3 asteroidPosition = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(300, -100)) + randomDirection * movementSpeed * Time.deltaTime;
                Quaternion asteroidRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                GameObject newAsteroids = Instantiate(asteroids[i], asteroidPosition, asteroidRotation);
                newAsteroids.tag = "Asteroids";
                InstantiatedAsteroids.Add(newAsteroids);

            }
        }
    }
    private void asteriodMovement()
    {
        for (var i = 0; i < InstantiatedAsteroids.Count; i++)
        {
            //Asteroids current rotation
            Quaternion asteroidCurrRotation = InstantiatedAsteroids[i].transform.rotation;

            Quaternion rotate = Quaternion.Euler(asteroidCurrRotation.eulerAngles + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
            InstantiatedAsteroids[i].transform.rotation= Quaternion.Slerp(asteroidCurrRotation, rotate, movementSpeed * Time.deltaTime);

            Vector3 newPosition = InstantiatedAsteroids[i].transform.position + InstantiatedAsteroids[i].transform.forward * movementSpeed * Time.deltaTime;

            InstantiatedAsteroids[i].transform.position = Vector3.Slerp(InstantiatedAsteroids[i].transform.position,newPosition, movementSpeed * Time.deltaTime);
        }
    }

}
