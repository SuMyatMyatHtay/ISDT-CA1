using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoxMelting : MonoBehaviour
{
    [Header("Canon-Reference")]
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private float _velocity = 10;

    public GameObject BeforeChargeGO;
    public GameObject AfterChargeGO;

    [SerializeField] private XRSocketInteractor socket;
    IXRSelectInteractable objName;


    private float LastShootTime;
    [SerializeField] private float ShootDelay = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        BeforeChargeGO.SetActive(true);
        AfterChargeGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var ball = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
            ball.Init(_velocity);
        }
        */
    }

    public void FlushInserted()
    {
        Debug.Log("Flush is inserted");
        objName = socket.GetOldestInteractableSelected();
        if (objName != null)
        {
            Debug.Log("obj Name : " + objName);
            if (objName.ToString() == "frontLightPart (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)")
            {
                BeforeChargeGO.SetActive(false);
                AfterChargeGO.SetActive(true);
            }
            else
            {
                Debug.Log("Interacting with the wrong piece.");
            }
        }
    }
    public void FlushRemoved()
    {
        BeforeChargeGO.SetActive(true);
        AfterChargeGO.SetActive(false);
    }
    public void CheckToMelt()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            if (AfterChargeGO.activeSelf == true)
            {
                Debug.Log("Check To Melt function is runnning");
                var ball = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
                ball.Init(_velocity);
                Destroy(ball.gameObject, 5f);
                LastShootTime = Time.time;
                //CanonPressSpace(); 
            }
        }

        

    }

    public void CanonPressSpace()
    {
        var ball = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
        ball.Init(_velocity);
    }
}
