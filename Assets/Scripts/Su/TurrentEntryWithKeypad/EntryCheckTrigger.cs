using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryCheckTrigger : MonoBehaviour
{
    public GameObject TurrentEntryGO;
    // Start is called before the first frame update
    void Start()
    {
        TurrentEntryGO.GetComponent<TurrentEntry>().CanStartCheck = true;
        TurrentEntryGO.GetComponent<TurrentEntry>().DisableToggles(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("JolleenIsIn : " + col.gameObject.name); 
        if (col.gameObject.name.Contains("TurrentTargetPlayer"))
        {
            TurrentEntryGO.GetComponent<TurrentEntry>().AsunaIsIn = true; 
            //TurrentEntryGO.GetComponent<TurrentEntry>().AsunaEntryCheck();
        }
        if (col.gameObject.name.Contains("Jolleen"))
        {
            TurrentEntryGO.GetComponent<TurrentEntry>().JolleenIsIn = true;
            //TurrentEntryGO.GetComponent<TurrentEntry>().AsunaEntryCheck();
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Contains("TurrentTargetPlayer"))
        {
            TurrentEntryGO.GetComponent<TurrentEntry>().AsunaIsIn = false;
            //TurrentEntryGO.GetComponent<TurrentEntry>().AsunaEntryExit();
        }
        if (col.gameObject.name.Contains("Jolleen"))
        {
            TurrentEntryGO.GetComponent<TurrentEntry>().JolleenIsIn = false;
            //TurrentEntryGO.GetComponent<TurrentEntry>().AsunaEntryCheck();
        }
    }
}
