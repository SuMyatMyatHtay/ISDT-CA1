using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class CheckClamps : MonoBehaviour
{
    #region Variables 
    [SerializeField] private Toggle _toggle;
    [SerializeField] private GameObject correctSocketName;
    private GameObject _selectedClamp;
    #endregion

    protected void OnTriggerEnter(Collider col)
    {
        _selectedClamp = col.gameObject; 
    }

    #region On Methods 
    public void isCorrectClamp()
    {
        Debug.Log("Socket_" + _selectedClamp.name);
        Debug.Log(correctSocketName.name); 
        if (("Socket_" + _selectedClamp.name) == correctSocketName.name)
        {
            Debug.Log("Correct Match"); 
            _toggle.isOn = true;
        }
        else
        {
            Debug.Log("Mismatch"); 
            _toggle.isOn = false;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
