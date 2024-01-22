using UnityEngine;
using System;
using TMPro;

//---------------------------------------------------------------------------------
// Author		: SU MYAT MYAT HTAY 
// Date  		: 2023-02-12
// Description	: Scriptable object for different Magazine type. 
//---------------------------------------------------------------------------------

[CreateAssetMenu(fileName = "New MagData", menuName = "Weapons/Magazine Data", order = 51)]
public class MagDataSO : ScriptableObject 
{
    #region Variables

    [SerializeField] private string _magType;
    [SerializeField] private GameObject magazine;
    
    public int MaxBulletCount { get => this._maxBulletCount; }
    [SerializeField] private int _maxBulletCount = 10;

    #endregion

}
