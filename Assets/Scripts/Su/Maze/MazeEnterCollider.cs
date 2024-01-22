using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeEnterCollider : MonoBehaviour
{
    public GameObject Stair; 
    public GameObject StairTwo;
    public GameObject MazeFloor;
    private MeshCollider meshCollider;

    [Header("Show Me The Path")]
    public GameObject XROrigin; 
    public GameObject LittleHelper; 
    [SerializeField] private GameObject playerPosition;
    NavMeshAgent enemyNavMeshAgent;

    /*
    [Header("Items relating to Mini Crystal")]
    public GameObject InventoryManagerGO;
    public Item newitem;
    public Item prevItem; 
    */

    // Start is called before the first frame update
    void Start()
    {
        meshCollider = MazeFloor.GetComponent<MeshCollider>();
        enemyNavMeshAgent = LittleHelper.GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (meshCollider != null)
        {
            meshCollider.isTrigger = true; 
        }
        Stair.SetActive(true);
        StairTwo.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " : maze");
        if (other.gameObject.name.Contains("XR Origin"))
        {
            if (meshCollider != null)
            {
                meshCollider.isTrigger = false;
                Stair.SetActive(false);
                StairTwo.SetActive(false);
                /*
                foreach (var item in InventoryManagerGO.GetComponent<InventoryManager>().Items)
                {
                    if(item.id == 4)
                    {
                        InventoryManager.Instance.Add(newitem);
                        InventoryManager.Instance.Remove(prevItem); 
                    }
                }
                */
                //ShowMeThePath(); 
            }
        }
    }

    public void ShowMeThePath()
    {
        LittleHelper.transform.position = XROrigin.transform.position;
        enemyNavMeshAgent.Warp(XROrigin.transform.position);
        enemyNavMeshAgent.SetDestination(playerPosition.transform.position);
    }

}
