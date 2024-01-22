using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBullets : MonoBehaviour
{
    /*
     * Written By:Goh Min Rui
     * Description: For shooters that randomly shoots projectiles
     */

    [SerializeField] private List<Transform> TriggerPoints = new List<Transform>();
    [SerializeField] private List<GameObject> ProjectileList = new List<GameObject>();
    [SerializeField] private List<GameObject> Shooter = new List<GameObject>();
    [SerializeField] private Transform DetectionPoint;
    [SerializeField] private Transform Player;
    private bool InstantiateWait=false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var shoot in Shooter)
        {
            shoot.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(DetectionPoint.position, Player.position) < 2f)
        {
            foreach (var shoot in Shooter)
            {
                shoot.SetActive(true);
                Animator animator = shoot.GetComponent<Animator>();
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                {
                    animator.SetTrigger("Shoot");
                }
            }
            StartCoroutine(waitForShooting());
        }   
    }
    
    private IEnumerator waitForShooting()
    {
        yield return new WaitForSeconds(2f);
        if (InstantiateWait == false)
        {
            StartCoroutine(eachCycle());
        }
    }
    private IEnumerator eachCycle()
    {
        InstantiateWait = true;
        foreach (var TriggerPoints in TriggerPoints)
        {
            StartCoroutine(InstantiateProjectile(TriggerPoints)); 
        }
            float randomLoop = Random.Range(1, 2);
            yield return new WaitForSeconds(randomLoop);
           InstantiateWait= false;

    }

    private IEnumerator InstantiateProjectile(Transform TriggerPoints)
    {
        int randomIndex = Random.Range(0, ProjectileList.Count);
        Destroy(Instantiate(ProjectileList[randomIndex], TriggerPoints.position, Random.rotation), 3f);

        float randomLoop = Random.Range(0f, 1.5f);
        yield return new WaitForSeconds(randomLoop);

    }
}
