using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midDoorCollider : MonoBehaviour
{
    public GameObject TurrentEntryGO;

    private bool JolleenMDC;
    private bool AsunaMDC; 

    // Start is called before the first frame update
    void Start()
    {
        JolleenMDC = false;
        AsunaMDC = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (JolleenMDC == TurrentEntryGO.GetComponent<TurrentEntry>().ToggleJolleen.isOn && AsunaMDC == TurrentEntryGO.GetComponent<TurrentEntry>().ToggleAsuna.isOn)
        {
            TurrentEntryGO.GetComponent<TurrentEntry>().JolleenToCell();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("midDoorCollider : " + other.gameObject.name);
        if (other.gameObject.name.Contains("Jolleen"))
        {
            JolleenMDC = true; 
        }
        if (other.gameObject.name.Contains("TurrentTargetPlayer"))
        {
            AsunaMDC = true;
        }

    }
}
