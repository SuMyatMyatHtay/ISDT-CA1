using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject SelectedGun;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (SelectedGun != null)
        {
            Debug.Log(SelectedGun);
        }
    }


}
