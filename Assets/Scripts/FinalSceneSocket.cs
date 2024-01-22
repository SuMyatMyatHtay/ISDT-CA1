using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FinalSceneSocket : MonoBehaviour
{
    public GameObject SuScene;
    public GameObject MRScene;
    public PlayerData PlayerData;

    private GameObject enteredGameObject;
    private GameObject exitGameObject;

    public XRGrabInteractable grabInteractableSu;
    public XRGrabInteractable grabInteractableMR;
    public FinalSceneScript FinalSceneScript;

    private int CrystalNumCheck;

    private void Start()
    {
        CrystalNumCheck = 0; 
    }

    private void OnTriggerEnter(Collider other)
    {
        enteredGameObject = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        exitGameObject = other.gameObject;
    }

    public void SelectedItem()
    {
        if (enteredGameObject.name == "Celestium" && gameObject.name == "CrystalsRedSocket")
        {
            SuScene.SetActive(false);
            enteredGameObject.GetComponent<XRGrabInteractable>().enabled = false;
            FinalSceneScript.CrystalCheck++;
        }
        if (enteredGameObject.name == "Etherium" && gameObject.name == "CrystalsBlueSocket")
        {
            MRScene.SetActive(false);
            enteredGameObject.GetComponent<XRGrabInteractable>().enabled = false;
            FinalSceneScript.CrystalCheck++; 
        }
    }

    public void Unselected()
    {
        /*
        if (exitGameObject.name == "Celestium" && gameObject.name == "CrystalsRedSocket")
        {
            PlayerData.CrystalsSuDone = null;
            SuScene.SetActive(true);
        }
        if (exitGameObject.name == "Etherium" && gameObject.name == "CrystalsBlueSocket")
        {
            PlayerData.CrystalsMRDone = null;
            MRScene.SetActive(true);

        }
        */
    }


}
