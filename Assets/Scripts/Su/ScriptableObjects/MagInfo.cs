using UnityEngine;
using System.Collections;
using TMPro;
using System;

//---------------------------------------------------------------------------------
// Author		: SU MYAT MYAT HTAY 
// Date  		: 2023-02-12
// Description	: Magazine Information 
//---------------------------------------------------------------------------------
public class MagInfo : MonoBehaviour 
{
	#region Variables
	// Public Variables: camelCase
	public int magBulletCount;

    [SerializeField] private TMP_Text bulletDisplay;
    [SerializeField] private MagDataSO magData;

    #endregion

    private void Start() {
        
        UpdateBulletsDisplay();
    }

    private void OnEnable() {
        magBulletCount = magData.MaxBulletCount;
    }
    public void UpdateBulletsDisplay() {
        gameObject.GetComponent<ItemPickup>().item.value = magBulletCount; 
        bulletDisplay.text = String.Format("{0:D2}", magBulletCount);
    }
}
