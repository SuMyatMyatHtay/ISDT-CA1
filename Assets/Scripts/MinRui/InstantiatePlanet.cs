using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class InstantiatePlanet : MonoBehaviour
{
    [SerializeField] private List<GameObject> AllPlanets = new List<GameObject>();
    public GameObject player;
    [SerializeField] private float SmoothMovement=5f;
    [SerializeField] private MRAnimations MRAnimations;
    [SerializeField] private Transform startPoint;
    [SerializeField] private List<GameObject> warCrafts = new List<GameObject>();
    [SerializeField] private List<GameObject> instantiatedPoints = new List<GameObject>();
    [SerializeField] private GameObject warCraftProjectile;

    private List<GameObject> InstantiatedAirCrafts = new List<GameObject>();
    [SerializeField]  private GameObject planeBottom;
    private float Attack_Range = 5f;
    public Transform previousPlanetTransform;
    public GameObject SelectedPlanet;
    public GameObject FallenPlanet;
    public bool reachDestination=false;
   private bool canMove=true;
    public PlanetControl PlanetControl;
    [SerializeField] private TextMeshProUGUI ExitText;
    [SerializeField] private GameObject ExitButton;
    [SerializeField] private PlayerData playerData;
    private void Start()
    {
     if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }


        ExitText.text = "";
        ExitButton.SetActive(false);
    }

    private void Update()
    {
        if (canMove == true&& AllPlanets.Count>0)
        {
            StartCoroutine(PlanetMovementCoroutine());
        }
        if (player.transform.position.y <-15f)
        {
            MRAnimations.fadeInAndOutSequence();
            StartCoroutine(redirectPlayer());
         
        }
        if (reachDestination == true)
        {
                float count = 0;
                for (var i = 0; i < InstantiatedAirCrafts.Count; i++)
                {
                    GameObject target = player;
                    if (InstantiatedAirCrafts[i] != null)
                    {
                    count += 1;
                        GameObject currWarCraft = InstantiatedAirCrafts[i];

                        warShipConfiguration warCraftConfig = currWarCraft.GetComponent<warShipConfiguration>();
                        if (warCraftConfig.warShipHealth <= 0)
                        {
                        Vector3 targetPosition = new Vector3(planeBottom.transform.position.x, target.transform.position.y + 7, planeBottom.transform.position.z);
                        planeBottom.transform.position = Vector3.Lerp(planeBottom.transform.position, targetPosition, Time.deltaTime * 2f);

                        currWarCraft.GetComponent<Rigidbody>().useGravity = true;
                            currWarCraft.GetComponent<NavMeshAgent>().enabled = false;
                            currWarCraft.GetComponent<Rigidbody>().useGravity = true;
                            Vector3 dropDown = new Vector3(currWarCraft.transform.position.x, currWarCraft.transform.position.y - 50, currWarCraft.transform.position.z);
                            currWarCraft.transform.position = Vector3.Lerp(currWarCraft.transform.position, dropDown, Time.deltaTime * 5f);
                            Destroy(currWarCraft, 5f);
                            return;
                        }
                        else
                        {
                            //set warcraft to move towards player ship
                            currWarCraft.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);

                            Vector3 _direction = target.transform.position - currWarCraft.transform.position;


                            //Look at direction
                            currWarCraft.transform.rotation = Quaternion.Slerp(currWarCraft.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * 2f);


                            float distanceToTarget = Vector3.Distance(InstantiatedAirCrafts[i].transform.position, player.transform.position);
                            //Debug.Log("Difference" + distanceToTarget);

                            //moving warcraft to player ship vertical axis so it is at same level
                            Vector3 targetPosition = new Vector3(planeBottom.transform.position.x, target.transform.position.y+3 , planeBottom.transform.position.z);
                            planeBottom.transform.position = Vector3.Lerp(planeBottom.transform.position, targetPosition, Time.deltaTime * 2f);




                            InstantiatedAirCrafts[i].GetComponent<NavMeshAgent>().stoppingDistance=10f;



                            //Instantiate bullets for enemy if warcraft within attack range
                            if (distanceToTarget < 11f)
                            {
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
                                    Vector3 launcherTransform = warCraftConfig.Launchers;
                                    Quaternion launcherRotation = Quaternion.Euler(90, 0, 0);
                                    GameObject newProjectile = Instantiate(warCraftProjectile, InstantiatedAirCrafts[i].transform.TransformPoint(launcherTransform), launcherRotation);
                                    warCraftConfig.listOfProjectile.Add(newProjectile);
                                    int deathTime = Random.Range(5, 10);
                                    Destroy(newProjectile, deathTime);
                                }
                            }
              
                        }

                    }

                

            }

            if (count == 0)
            {
                InstantiateAircrafts();
            }
        }
    }

    private IEnumerator redirectPlayer()
    {
        Debug.Log("Current Planet: " + SelectedPlanet);
        yield return new WaitForSeconds(1f);
        if (player.GetComponent<Rigidbody>() != null)
        {
            FallenPlanet.transform.position = previousPlanetTransform.position;
            Destroy(player.GetComponent<Rigidbody>());
        }
      
            if (SelectedPlanet != null)
            {
                player.transform.position = SelectedPlanet.transform.position;
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                player.transform.position = startPoint.position;

                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        
    }

    private IEnumerator PlanetMovementCoroutine()
    {
            canMove = false;
            planetMovement();
            yield return new WaitForSeconds(0.5f);
            canMove = true;
    }

    private void planetMovement()
    {

        foreach (var planets in AllPlanets)
        {

            // Random movement direction
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            // Calculate new position
            Vector3 newPosition = planets.transform.position + randomDirection * 5f * Time.deltaTime;

            // Apply new position
            planets.transform.position = Vector3.Lerp(planets.transform.position, newPosition, SmoothMovement * Time.deltaTime);
        }
        if (SelectedPlanet!=null&&SelectedPlanet.name.Contains("Planet"))
        {
            player.transform.position = Vector3.Lerp(player.transform.position, SelectedPlanet.transform.position, SmoothMovement * Time.deltaTime);
        }
    }

    public void InstantiateAircrafts()
    {
        Debug.Log(InstantiatedAirCrafts.Count + "InstantiateAircraftCount");
        if( InstantiatedAirCrafts.Count < 5)
        {
            int Index;
            //Instantiate(Object original, Vector3 position, Quaternion rotation);
            //Pick 1/3 warcraft for variety aircraft enemy purpose
            Index = Random.Range(0, warCrafts.Count);
            int randomInt = Random.Range(0, instantiatedPoints.Count);
            Vector3 InstantiateTransform = new Vector3(instantiatedPoints[randomInt].transform.position.x,planeBottom.transform.position.y, instantiatedPoints[randomInt].transform.position.z);
            GameObject instantiedObject = Instantiate(warCrafts[Index], InstantiateTransform, Quaternion.Euler(0, 0, 0));
            instantiedObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            warShipConfiguration warCraftConfig = instantiedObject.GetComponent<warShipConfiguration>();
            warCraftConfig.warShipHealth *= 10;
            InstantiatedAirCrafts.Add(instantiedObject);
         

        }
        else
        {
            StartCoroutine(PlanetControl.startAnimation());
            StartCoroutine(showTextButton()); 
        }

    }

    private IEnumerator showTextButton()
    {
        yield return new WaitForSeconds(1f);
        ExitButton.SetActive(true);
        ExitText.text = @"Congratulations! Save jewel to inventory and exit the scene!";
    }
    public void GoToCentreScene()
    {
        playerData.MRSceneCheck = true;
        GameManager.Instance.UnloadChosenScene();

        GameManager.Instance.XRManager.SetActive(false);
        GameManager.Instance.LoadChosenScene("FinalSceneDesign");
    }

}
