using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

//Player aircraft movement which includes when handle/throttle is moved
public class shipMotion : MonoBehaviour
{
    /*
    Written By: Min Rui
    Description: Player aircraft movement
    Include:
    - when handle/throttle is moved
    - when aircraft reached destination
    - whether aircraft goes out of bounds
    */

    [Header("Handle Settings")]
    private bool isHandleGrabbed = false;
    public GameObject handle;
    private Transform originalHandleParent;
    private float rotationSpeed = 2f;
    private float rotationAmt = 1f;

    float r;
    [Header("Throttle Settings")]
    private bool isThrottleGrabbed = false;
    public GameObject throttle;
    private Transform originalThrottleParent;
    private Vector3 direction = new Vector3(0, 0, -1);
    //Throttle flames
    [SerializeField] private List<GameObject> airCraftFlames = new List<GameObject>();

    [Header("Ship Settings")]
    public float movementSpeed = 1f;
    public float straightLineDistance = 1000f;
    public GameObject ship;
    public shipEffects shipEffects;
    [SerializeField] private ShipUI ShipUI;
    private bool autoPilot = false;
    [SerializeField] private shipCollider shipCollider;
    [SerializeField] private PostProcessVolume PostProcessing;
    [SerializeField] private CanvasGroup overlayBlackCanvas;
    [SerializeField] private bool reachedDestination = false;


    [Header("Ship Points")]
    [SerializeField] private GameObject pointTopY;
    [SerializeField] private GameObject pointBottomY;
    [SerializeField] private GameObject pointRightX;
    [SerializeField] private GameObject pointLeftX;
    [SerializeField] private GameObject LimitZ;
    [SerializeField] private GameObject destination;

    [Header("Space island")]
    [SerializeField] private ParticleSystem fogParticles;
    [SerializeField] private GameObject EnterDoorPoint;

    [Header("XR")]
    [SerializeField] private GameObject externalXR;
    [SerializeField] private GameObject internalXR;
    [SerializeField] private GameObject XROrigin;
    [SerializeField] private MRAnimations MRAnimations;

    private bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        handle.GetComponent<XRGrabInteractable>().enabled = false;
        throttle.GetComponent<XRGrabInteractable>().enabled = false;
        originalThrottleParent = throttle.transform.parent;
        originalHandleParent = handle.transform.parent;
        throttle.transform.localRotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(WaitStart());
    }

    private IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(15f);
        MRAnimations.FadeInCam();
        handle.GetComponent<XRGrabInteractable>().enabled = true;
        throttle.GetComponent<XRGrabInteractable>().enabled = true;
        throttle.transform.localRotation = Quaternion.Euler(32, 0, 0);
        hasStarted =true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == true)
        {
            if (shipCollider.shipHealth > 0)
            {
                if (reachedDestination == false)
                {
                    checkReachDestination();
                    if (autoPilot == false)
                    {
                        moveByThrottle();
                        checkOutOfBound();
                        moveByHandle();
                    }
                }
                else
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        Vector3 newPosition = new Vector3(player.transform.position.x, 3f , player.transform.position.z);
                        player.transform.position = Vector3.Lerp(newPosition, player.transform.position, 3f * Time.deltaTime);
                        if (Vector3.Distance(EnterDoorPoint.transform.position, player.transform.position) < 1.5f)
                        {
                            MRAnimations._fadeScreen = overlayBlackCanvas;
                            MRAnimations.FadeOutCam();
                        }
                    }
                    return;
                }
            }
            else
            {
                if (ship != null)
                {
                    //add rigidbody for ship to spin
                    ship.AddComponent<Rigidbody>();
                    //edit post processing
                    if (PostProcessing.profile.TryGetSettings(out Vignette vignette))
                    {
                        vignette.intensity.value = 1;
                    }
                }
                ShipUI.isDead = true;
                ShipUI.atDanger = false;
                //destroy player ship if ship is destroyed
                Destroy(ship, 8f);
                StartCoroutine(LoadDeathScene());
            }
            //Increase vignette based on ship health
            if (shipCollider.shipHealth <= 50 && shipCollider.shipHealth > 0)
            {
                ShipUI.atDanger = true;
                if (PostProcessing.profile.TryGetSettings(out Vignette vignette))
                {
                    vignette.intensity.value = (50f - shipCollider.shipHealth) / 50f;
                }
            }
        }
    }

    private void moveByHandle()
    {
        float XAngle = 0f;
        float ZAngle = 0f;

        //change in Z direction of handle
        //handle.transform.eulerAngles.z --> Left (Pos) Right (Neg)
        if (handle.transform.eulerAngles.z > 180)
        {
            //change in z rotation
            ZAngle = -(360 - handle.transform.eulerAngles.z) / 2;
            controlHandleZ(ZAngle, false);
            // Debug.Log("Negative rotate" + ZAngle);

        }
        else
        {
            ZAngle = (handle.transform.eulerAngles.z / 2);

            controlHandleZ(ZAngle, true);

            //  Debug.Log("Positive rotate" + ZAngle);
        }

        //change in x rotation of handle
        //positive (Down) Ship.X >0
        //negative (Up) 
        if (handle.transform.eulerAngles.x > 315)
        {
            XAngle = handle.transform.eulerAngles.x - 315;
            controlHandleX(XAngle / 2, false);
        }
        //negative (Up)  Ship.X <0
        else
        {
            XAngle = -(315 - handle.transform.eulerAngles.x);
            controlHandleX(XAngle / 2, true);
        }
        // Debug.Log("Z rotation transform" + handle.transform.rotation.eulerAngles.z);
        if (handle.transform.eulerAngles.y - 180 > 2) 
        {
            directionalArrowsReset();
        }

    }

    //x rotation of handle
    private void controlHandleZ(float Angle, bool positive)
    {
        //rotate by z-axis
        ship.transform.rotation = Quaternion.Euler(ship.transform.rotation.eulerAngles.x, ship.transform.rotation.eulerAngles.y, Angle);
        //Move by z-axis

        if (Angle < 0)
        {
            Angle = -Angle;
        }

        Vector3 vectorDir = positive ? Vector3.right : -Vector3.right;
        Vector3 targetPosition = ship.transform.position + vectorDir * straightLineDistance * Mathf.Lerp(0, Angle * 2f, Time.deltaTime);
        ship.transform.position = Vector3.Lerp(ship.transform.position, targetPosition, Time.deltaTime * Mathf.Lerp(0, Angle * 2f, Time.deltaTime));
    }

    //z rotation of handle
    private void controlHandleX(float Angle, bool positive)
    {
        //rotate by x-axis
        ship.transform.rotation = Quaternion.Euler(Angle, ship.transform.rotation.eulerAngles.y, ship.transform.rotation.eulerAngles.z);
        //rotate by x-axis
        if (Angle < 0)
        {
            Angle = -Angle;
        }

        Vector3 vectorDir = positive ? Vector3.up : -Vector3.up;
        Vector3 targetPosition = ship.transform.position + vectorDir * straightLineDistance * Mathf.Lerp(0, Angle * 2f, Time.deltaTime);
        ship.transform.position = Vector3.Lerp(ship.transform.position, targetPosition, Time.deltaTime * Mathf.Lerp(0, Angle * 2f, Time.deltaTime));
    }


    private void moveByThrottle()
    {


        if (throttle.transform.localRotation.eulerAngles.x > 90)
        {
            float newAngle = 360 - throttle.transform.localRotation.eulerAngles.x;

            movementSpeed = newAngle / 5;

            direction = new Vector3(0, 0, 1);
            //  Debug.Log("ThrottleReverse" +newAngle);
            foreach (var flame in airCraftFlames)
            {
                shipEffects.aircraftEngineFlamesfloat(flame, newAngle);
            }
        }
        else if (throttle.transform.localRotation.eulerAngles.x != 0)
        {
            foreach (var flame in airCraftFlames)
            {
                shipEffects.aircraftEngineFlamesfloat(flame, throttle.transform.localRotation.eulerAngles.x);
            }
            //  Debug.Log("ThrottleForward" + throttle.transform.localRotation.eulerAngles.x);
            movementSpeed = throttle.transform.localRotation.eulerAngles.x / 5;
            direction = new Vector3(0, 0, -1);
        }
        else
        {
            movementSpeed = 1f;
        }
        // Debug.Log(movementSpeed);
        // Debug.Log(throttle.transform.localRotation.eulerAngles.x+ " rotation");
        Vector3 targetPosition = ship.transform.position + direction * straightLineDistance * Mathf.Lerp(0, movementSpeed, Time.deltaTime);

        ship.transform.position = Vector3.Lerp(ship.transform.position, targetPosition, Time.deltaTime * movementSpeed);


    }

    //Check if player's ship is out of bound
    public void checkOutOfBound()
    {

        //if ship x axis exceeds bounded x axis, it will be relocated in a few minutes within bound
        if (!(ship.transform.position.x > pointRightX.transform.position.x && ship.transform.position.x < pointLeftX.transform.position.x))
        {
            ShipUI.outOfBound = true;
           StartCoroutine( relocate());
            //Debug.Log("Out of bound");
        }
        //if ship y axis exceeds bounded y axis, it will be relocated in a few minutes within bound
        if (!(ship.transform.position.y>pointBottomY.transform.position.y && ship.transform.position.y< pointTopY.transform.position.y))
        {
            ShipUI.outOfBound = true;
            StartCoroutine(relocate());
            //Debug.Log("Out of bound");
        }
        if (ship.transform.position.z < LimitZ.transform.position.z)
        {
            ShipUI.outOfBound = true;
            StartCoroutine(relocate());
            //Debug.Log("Out of bound");
        }
        if (ship.transform.localRotation.y == 0)
        {
            Quaternion rotateTarget = Quaternion.Euler(4.35f, 180, 0);
            ship.transform.localRotation = Quaternion.Slerp(ship.transform.localRotation, rotateTarget, Time.deltaTime * 1f);
        }
    }
   

    //check if ship is within destinational point for autopilot to land at right spot
    private void checkReachDestination()
    {
        float distanceToDestination =  ship.transform.position.z-pointLeftX.transform.position.z;
        //just some ui updating to inform player how far it is from destination
        ShipUI.showArriving(Mathf.Round(distanceToDestination));

        if (distanceToDestination < 100 && distanceToDestination>=0)
        {
            
                var fogParticlesEmission = fogParticles.emission;
                fogParticlesEmission.rateOverTime = (50 - distanceToDestination) / 50;
            
        }
        if (ship.transform.position.z < pointLeftX.transform.position.z)
        {     
            ShipUI.atDestinationPoints = true;
            //disable XR Grab Interactable
            handle.GetComponent<XRGrabInteractable>().enabled = false;
            throttle.GetComponent<XRGrabInteractable>().enabled = false;
            //Reset throttle, handle and ship rotation
            throttle.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, destination.transform.position, Time.deltaTime * 8f);
            Quaternion rotateTarget = Quaternion.Euler(4.35f, 180, 0);
            ship.transform.localRotation = Quaternion.Slerp(ship.transform.localRotation, rotateTarget,Time.deltaTime*1f);
            autoPilot = true;
            ShipUI.showArriving(0f);
            directionalArrowsReset();
        }
        if(ship.transform.position== destination.transform.position)
        {
            foreach (var flame in airCraftFlames)
            {
                flame.SetActive(false);
            }
                reachedDestination = true;
            StartCoroutine(GetOutOfShip());
        }
    }

    private IEnumerator relocate()
    {
        yield return new WaitForSeconds(5f);
        if (ship.transform.position.z < pointLeftX.transform.position.z)
        {
            ship.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, pointLeftX.transform.position.z);
        }
            else {
                    ship.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, ship.transform.position.z - 10);
                }
        directionalArrowsReset();
        ship.transform.localRotation = Quaternion.Euler(4.35f, 180, 0);
    }

    private IEnumerator GetOutOfShip()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            ShipUI.boardText.text = "Arrived!\nYou will inside in " + countdown + "s";
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        MRAnimations._fadeScreen = overlayBlackCanvas;
        MRAnimations.fadeInAndOutSequence();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.UnloadChosenScene();
        GameManager.Instance.LoadChosenScene("SpaceStationMR");
        GameManager.Instance.XRManager.SetActive(false);

    }

    //Load Death Scene
    private IEnumerator LoadDeathScene()
    {
        yield return new WaitForSeconds(8f);
        Debug.Log("Load death");
        //SceneManager.LoadScene("DeadScene");
    }

    //when handle is grabbed, parent of it remains
    public void handleGrabMotion()
    {
        handle.transform.parent = originalHandleParent;
        isHandleGrabbed = true;
        Debug.Log("Handle Grabbed");
    }

    public void ungrabbedHandle()
    {
        isHandleGrabbed = false;
        Debug.Log("Handle Ungrabbed");
    }

    //when throttle is grabbed, parent of it remains
    public void throttleGrabMotion()
    {
        throttle.transform.parent = originalThrottleParent;
        isThrottleGrabbed = true;
        Debug.Log("Throttle Grabbed");
    }

    public void throttleUngrabMotion()
    {
        isThrottleGrabbed = false;
        Debug.Log("Throttle Ungrabbed");
    }

    public void directionalArrowsLeft()
    {
        
        Quaternion rot = Quaternion.Slerp(handle.transform.rotation, Quaternion.Euler(handle.transform.rotation.eulerAngles.x, handle.transform.rotation.eulerAngles.y, handle.transform.rotation.eulerAngles.z+rotationAmt),rotationSpeed* Time.deltaTime);
        handle.transform.rotation = rot;
    }

    public void directionalArrowsRight()
    {
        Quaternion rot = Quaternion.Slerp(handle.transform.rotation, Quaternion.Euler(handle.transform.rotation.eulerAngles.x, handle.transform.rotation.eulerAngles.y, handle.transform.rotation.eulerAngles.z-rotationAmt), rotationSpeed * Time.deltaTime);
        handle.transform.rotation = rot;
    }

    public void directionalArrowsUp()
    {
        Quaternion rot = Quaternion.Slerp(handle.transform.rotation, Quaternion.Euler(handle.transform.rotation.eulerAngles.x-rotationAmt, handle.transform.rotation.eulerAngles.y, handle.transform.rotation.eulerAngles.z), rotationSpeed * Time.deltaTime);
        handle.transform.rotation = rot;
    }

    public void directionalArrowsDown()
    {
        Quaternion rot = Quaternion.Slerp(handle.transform.rotation, Quaternion.Euler(handle.transform.rotation.eulerAngles.x + rotationAmt, handle.transform.rotation.eulerAngles.y, handle.transform.rotation.eulerAngles.z), rotationSpeed * Time.deltaTime);
        handle.transform.rotation = rot;
        
    }
    public void directionalArrowsReset()
    {
    Debug.Log("Before Reset: " + handle.transform.rotation.eulerAngles);
        handle.transform.localRotation = Quaternion.Euler(0,0,0);
        Debug.Log("After Reset: " + handle.transform.rotation.eulerAngles);
    }

    
}
