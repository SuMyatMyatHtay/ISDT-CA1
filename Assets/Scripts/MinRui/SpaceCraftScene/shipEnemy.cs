using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Anything related to the enemy aircrafts
public class shipEnemy : MonoBehaviour
{
    [Header("WarCraft")] 
    [SerializeField] private List<GameObject> warCrafts;
    [SerializeField] private GameObject warCraftProjectile; 
    private float Attack_Range = 50f;
    [SerializeField]
    private float Rotate_amt = 2.0f;
    [SerializeField]
    private GameObject planeBottom;
    private bool cannotInstantiate = false;


    [Header("Points")]
    [SerializeField] private List<GameObject> PointLeft;
    [SerializeField] private List<GameObject> PointRight;

    [Header("Target")]
    [SerializeField] private GameObject target;
    [SerializeField] private ShipUI ShipUI;

    private List<GameObject> InstantiatedWarCraftList = new List<GameObject>();

    private bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitStart());
    }

    private IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(15f);
        hasStarted = true;
        createWarCrafts();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == true)
        {
            if (target != null)
            {
                float count = 0;
                for (var i = 0; i < InstantiatedWarCraftList.Count; i++)
                {
                    if (InstantiatedWarCraftList[i] != null)
                    {
                        count += 1;
                        GameObject currWarCraft = InstantiatedWarCraftList[i];

                        warShipConfiguration warCraftConfig = currWarCraft.GetComponent<warShipConfiguration>();

                        if (warCraftConfig.warShipHealth <= 0)
                        {
                            ShipUI.isEnemyDead = true;
                            currWarCraft.GetComponent<Rigidbody>().useGravity = true;
                            currWarCraft.GetComponent<NavMeshAgent>().enabled = false;
                            currWarCraft.GetComponent<Rigidbody>().useGravity = true;
                            Vector3 dropDown = new Vector3(currWarCraft.transform.position.x, currWarCraft.transform.position.y - 50, currWarCraft.transform.position.z);
                            currWarCraft.transform.position = Vector3.Lerp(currWarCraft.transform.position, dropDown, Time.deltaTime * 5f);
                            Destroy(currWarCraft, 10f);
                            return;
                        }

                        //set warcraft to move towards player ship
                        currWarCraft.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);

                        Vector3 _direction = target.transform.position - currWarCraft.transform.position;

                        //Destroy warcraft if warcraft is behind target;
                        if (currWarCraft.transform.position.z > target.transform.position.z)
                        {
                            Destroy(currWarCraft, 10f);
                            return;
                        }




                        //Look at direction
                        currWarCraft.transform.rotation = Quaternion.Slerp(currWarCraft.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * Rotate_amt);


                        float distanceToTarget = Vector3.Distance(InstantiatedWarCraftList[i].transform.position, target.transform.position);
                        //Debug.Log("Difference" +distanceToTarget);     

                        //if ship is in bound, move warcraft same level as target
                        if (ShipUI.outOfBound == false)
                        {
                            //moving warcraft to player ship vertical axis so it is at same level
                            Vector3 targetPosition = new Vector3(planeBottom.transform.position.x, target.transform.position.y + 5, planeBottom.transform.position.z);
                            planeBottom.transform.position = Vector3.Lerp(planeBottom.transform.position, targetPosition, Time.deltaTime * 2f);
                        }



                        //Attack Range = stopping distance
                        Attack_Range = InstantiatedWarCraftList[i].GetComponent<NavMeshAgent>().stoppingDistance;

                        //make warcraft move back if too near (doesnt work too well)
                        if (distanceToTarget < 20)
                        {
                            Vector3 moveBackDirection = currWarCraft.transform.position - target.transform.position;
                            Vector3 newPosition = currWarCraft.transform.position + moveBackDirection.normalized * 20;
                            currWarCraft.GetComponent<NavMeshAgent>().SetDestination(newPosition);
                        }

                        //Instantiate bullets for enemy if warcraft within attack range
                        if (distanceToTarget < Attack_Range)
                        {
                            ShipUI.enemyApproaching = true;
                            int eachWarCraftProjectiles = 0;
                            for (int x = 0; x < warCraftConfig.listOfProjectile.Count; x++)
                            {
                                if (warCraftConfig.listOfProjectile[x] != null)
                                {
                                    eachWarCraftProjectiles += 1;
                                }
                            }

                            //only 3 bullets at any time
                            if (eachWarCraftProjectiles < 3)
                            {
                                Physics.gravity = new Vector3(0, -0.5f, 0);
                                Vector3 launcherTransform = warCraftConfig.Launchers;
                                Quaternion launcherRotation = Quaternion.Euler(90, 0, 0);
                                GameObject newProjectile = Instantiate(warCraftProjectile, InstantiatedWarCraftList[i].transform.TransformPoint(launcherTransform), launcherRotation);
                                warCraftConfig.listOfProjectile.Add(newProjectile);
                                int deathTime = Random.Range(5, 10);
                                Destroy(newProjectile, deathTime);
                            }
                        }

                    }

                }
                if (count == 0)
                {
                    createWarCrafts();
                }
            }

        }
    }
    private void createWarCrafts()
    {
        for (var i = 0; i < PointLeft.Count; i++)
        {
            if (target.transform.position.z > PointLeft[i].transform.position.z+50)
            {
                int Index;
                //Instantiate(Object original, Vector3 position, Quaternion rotation);
                //Pick 1/3 warcraft for variety aircraft enemy purposes
                Index = Random.Range(0, warCrafts.Count);
                Vector3 InstantiateTransform = new Vector3(target.transform.position.x, PointRight[i].transform.position.y, PointRight[i].transform.position.z);
                GameObject instantiedObject = Instantiate(warCrafts[Index], InstantiateTransform, PointLeft[i].transform.rotation);
                InstantiatedWarCraftList.Add(instantiedObject);
                i = PointLeft.Count;
            }
        }
      
    }



}
