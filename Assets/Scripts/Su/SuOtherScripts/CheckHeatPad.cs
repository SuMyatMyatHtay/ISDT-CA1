using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckHeatPad : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody crystalRigidbody;

    private GameObject _selectedClamp;

    public GameObject JolleenReliveGO;
    // Start is called before the first frame update
    public GameObject RubyCrystal;

    public bool MazeCrystalKeep; 

    protected void OnTriggerEnter(Collider col)
    {
        _selectedClamp = col.gameObject;
    }

    void Start()
    {
        grabInteractable = RubyCrystal.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            // Disable the XRGrabInteractable component
            grabInteractable.enabled = false;
        }

        crystalRigidbody = RubyCrystal.GetComponent<Rigidbody>();
        if (crystalRigidbody != null)
        {
            // Set the Rigidbody to be kinematic
            crystalRigidbody.isKinematic = true;
            crystalRigidbody.useGravity = false; 
        }

        MazeCrystalKeep = false; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region On Methods 
    public void isCorrectHeatPad()
    {
        if (_selectedClamp.name == "HeatPad")
        {
            Debug.Log("Correct Match");
            JolleenReliveGO.GetComponent<JollenRelive>().FadeOutGlassLikePanel(); 
        }
        else
        {
            Debug.Log("Mismatch");
        }
    }
    #endregion

    public void yesCorrectHeatPad()
    {
        Debug.Log("yesCorrectHeatPad");
        if (grabInteractable != null)
        {
            // Disable the XRGrabInteractable component
            grabInteractable.enabled = true;
        }

        if (crystalRigidbody != null)
        {
            // Set the Rigidbody to be kinematic
            crystalRigidbody.isKinematic = false;
            crystalRigidbody.useGravity = true;
        }

    }

    public void yesMazeCrystalKeep()
    {
        MazeCrystalKeep = true; 
    }
}
