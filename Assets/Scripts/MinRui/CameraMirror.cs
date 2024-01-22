using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMirror : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private List<GameObject> Cameras = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var camera in Cameras)
        {
            camera.SetActive(false);
            camera.GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var camera in Cameras)
        {
            if (Vector3.Distance(Player.transform.position, camera.transform.position) < 15f)
            {

                camera.SetActive(true);
                camera.GetComponent<Camera>().enabled = true;
            }
            else
            {
                camera.SetActive(false);
                camera.GetComponent<Camera>().enabled = false;
            }
        }


    }
}
