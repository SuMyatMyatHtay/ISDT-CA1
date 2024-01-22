using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class PlanetControl : MonoBehaviour
{
    public InstantiatePlanet InstantiatePlanet;
    [SerializeField] private Transform ToJewel;
    [SerializeField] private List<GameObject> ListOfObjToAnimate = new List<GameObject>();
    [SerializeField] private List<GameObject> warCrafts = new List<GameObject>();
    [SerializeField] private List<GameObject> instantiatedPoints = new List<GameObject>();


    private void Update()
    {
        if (ToJewel != null)
        {
            if (Vector3.Distance(InstantiatePlanet.player.transform.position, ToJewel.position) < 1f)
            {
                InstantiatePlanet.player.transform.parent = null;
            }
        }
    }
        

    public void movePlayer2Planets()
    {
        InstantiatePlanet.previousPlanetTransform = gameObject.transform;
        int randomNumber = Random.Range(1, 10);
        if (randomNumber == 5)
        {
            gameObject.AddComponent<Rigidbody>();
            InstantiatePlanet.player.AddComponent<Rigidbody>();
            InstantiatePlanet.FallenPlanet = gameObject;

        }
        else
        {
                InstantiatePlanet.SelectedPlanet = gameObject;
        }

    }
    public void moveMachineToDestination()
    {
        StartCoroutine(moveMachine());

    }

    private IEnumerator moveMachine()
    {

        yield return new WaitForSeconds(1f);
        InstantiatePlanet.player.transform.parent = gameObject.transform;
        NavMeshAgent gameObjectAgent = gameObject.GetComponent<NavMeshAgent>();
        gameObjectAgent.SetDestination(ToJewel.transform.position);
        InstantiatePlanet.reachDestination = true;
    }




    public IEnumerator startAnimation()
    {
        yield return new WaitForSeconds(1f);
        foreach (var item in ListOfObjToAnimate)
        {
            item.GetComponent<Animator>().SetTrigger("ShowJewel");
        }

    }


    public void hideJewel()
    {
        foreach (var item in ListOfObjToAnimate)
        {
            item.GetComponent<Animator>().SetTrigger("HideJewel");
        }
    }

 
}

